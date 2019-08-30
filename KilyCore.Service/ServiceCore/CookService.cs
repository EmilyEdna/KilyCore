using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Cook;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：CookService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.ServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/24 14:26:01
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.ServiceCore
{
    public class CookService : Repository, ICookService
    {
        #region 厨师菜单
        /// <summary>
        /// 父级菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseCookMenu> AddCookParentMenu()
        {
            var query = Kily.Set<CookMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.ParentId == null).AsNoTracking().AsQueryable();
            var data = query.Select(t => new ResponseCookMenu()
            {
                MenuId = t.MenuId,
                MenuName = t.MenuName
            }).ToList();
            return data;
        }
        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseCookMenu GetCookMenuDetail(Guid Id)
        {
            var data = Kily.Set<CookMenu>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseCookMenu()
            {
                Id = t.Id,
                ParentId = t.ParentId,
                MenuId = t.MenuId,
                MenuName = t.MenuName,
                MenuIcon = t.MenuIcon,
                MenuAddress = t.MenuAddress,
                HasChildrenNode = t.HasChildrenNode
            }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 厨师菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookMenu> GetCookMenuPage(PageParamList<RequestCookMenu> pageParam)
        {
            var query = Kily.Set<CookMenu>().Where(t => t.IsDelete == false).AsNoTracking().AsQueryable();
            if (!String.IsNullOrEmpty(pageParam.QueryParam.MenuName))
                query = query.Where(t => t.MenuName.Contains(pageParam.QueryParam.MenuName));
            var data = query.OrderByDescending(t => t.CreateTime).Select(t => new ResponseCookMenu()
            {
                Id = t.Id,
                MenuId = t.MenuId,
                ParentId = t.ParentId,
                MenuAddress = t.MenuAddress,
                MenuName = t.MenuName,
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        public string RemoveCookMenu(Guid Id)
        {
            if (Delete<CookMenu>(ExpressionExtension.GetExpression<CookMenu>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EditCookMenu(RequestCookMenu Param)
        {
            CookMenu tree = Param.MapToEntity<CookMenu>();
            if (Param.Id != Guid.Empty)
            {
                //修改
                if (Update<CookMenu, RequestCookMenu>(tree, Param))
                    return ServiceMessage.UPDATESUCCESS;
                else
                    return ServiceMessage.UPDATEFAIL;
            }
            else
            {
                //新增
                if (tree.HasChildrenNode) //true就是一级菜单
                {
                    tree.ParentId = null;
                    tree.Level = MenuEnum.LevelOne;
                    tree.MenuAddress = null;
                    tree.MenuId = Guid.NewGuid();
                }
                else
                {
                    tree.Level = MenuEnum.LevelTwo;
                    tree.MenuId = Guid.NewGuid();
                }
                if (Insert<CookMenu>(tree))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        #endregion

        #region 权限菜单树
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        public IList<ResponseParentTree> GetCookTree()
        {
            IQueryable<ResponseParentTree> queryable = Kily.Set<CookMenu>().Where(t => t.IsDelete == false)
                 .Where(t => t.Level == MenuEnum.LevelOne)
                 .AsNoTracking().Select(t => new ResponseParentTree()
                 {
                     Id = t.Id,
                     Text = t.MenuName,
                     Color = "black",
                     BackClolor = "white",
                     SelectedIcon = "fa fa-refresh fa-spin",
                     Nodes = Kily.Set<CookMenu>().Where(x => x.IsDelete == false)
                     .Where(x => x.Level != MenuEnum.LevelOne)
                     .Where(x => x.ParentId == t.MenuId).AsNoTracking()
                     .Select(x => new ResponseChildTree()
                     {
                         Id = x.Id,
                         Text = x.MenuName,
                         Color = "black",
                         BackClolor = "white",
                         SelectedIcon = "fa fa-refresh fa-spin",
                     }).AsQueryable()
                 }).AsQueryable();
            var data = queryable.ToList();
            return data;
        }
        #endregion

        #region 厨师角色
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookRole> GetCookAuthorPage(PageParamList<RequestCookRole> pageParam)
        {
            IQueryable<CookRoleAuthor> queryable = Kily.Set<CookRoleAuthor>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AuthorName))
                queryable = queryable.Where(t => t.AuthorName.Contains(pageParam.QueryParam.AuthorName));
            var data = queryable.Select(t => new ResponseCookRole()
            {
                Id = t.Id,
                AuthorName = t.AuthorName,
                AuthorMenuPath = t.AuthorMenuPath
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EditRole(RequestCookRole Param)
        {
            CookRoleAuthor Author = Param.MapToEntity<CookRoleAuthor>();
            if (Kily.Set<CookRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.AuthorName.Equals(Author.AuthorName)).AsNoTracking().FirstOrDefault() != null)
            {
                return "角色名称重复请重新添加!";
            }
            else
            {
                if (Insert<CookRoleAuthor>(Author))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveAuthorRole(Guid Id)
        {
            return Remove<CookRoleAuthor>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        #endregion

        #region 厨师信息
        /// <summary>
        /// 厨师信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookInfo> GetCookInfoPage(PageParamList<RequestCookInfo> pageParam)
        {
         
            IQueryable<CookInfo> queryable = Kily.Set<CookInfo>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryable = queryable.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Area));
            if(UserInfo().AccountType==AccountEnum.Village)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Town));
            var data = queryable.Select(t => new ResponseCookInfo()
            {
                Id = t.Id,
                TrueName = t.TrueName,
                IdCardNo = t.IdCardNo,
                AuditTypeName = AttrExtension.GetSingleDescription<DescriptionAttribute>(t.AuditType),
                TableName = t.GetType().Name
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 厨师详情
        /// </summary>
        /// <returns></returns>
        public ResponseCookInfo GetCookInfoDetail(Guid Id)
        {
            var data = Kily.Set<CookInfo>().Where(t => t.Id == Id).Join(Kily.Set<CookVip>(), t => t.CookId, x => x.Id, (t, x) => new ResponseCookInfo()
            {
                Id = t.Id,
                CookId = t.CookId,
                Phone = x.Phone,
                TrueName = t.TrueName,
                Sexlab = t.Sexlab,
                Nation = t.Nation,
                Birthday = t.Birthday,
                IdCardNo = t.IdCardNo,
                TypePath = t.TypePath,
                Address = t.Address,
                CardOffice = t.CardOffice,
                ExpiredDay = t.ExpiredDay,
                IdCardPhoto = t.IdCardPhoto,
                BookInCard = t.BookInCard,
                TrainCard = t.TrainCard
            }).AsNoTracking().FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 审核厨师信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditCookInfo(RequestAudit Param)
        {
            CookInfo info = Kily.Set<CookInfo>().Where(t => t.Id == Param.TableId).FirstOrDefault();
            info.AuditType = Param.AuditType;
            UpdateField(info, "AuditType");
            SystemAudit audit = Param.MapToEntity<SystemAudit>();
            return Insert(audit) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 服务管理
        /// <summary>
        /// 厨师开通的服务分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCookInfo> GetCookServicePage(PageParamList<RequestCookInfo> pageParam)
        {
            IQueryable<CookInfo> queryable = Kily.Set<CookInfo>().OrderByDescending(t => t.CreateTime);
            IQueryable<CookVip> queryables = Kily.Set<CookVip>().OrderByDescending(t => t.CreateTime);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryable = queryable.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
            if (UserInfo().AccountType == AccountEnum.Province)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Area));
            if (UserInfo().AccountType == AccountEnum.Village)
                queryable = queryable.Where(t => t.TypePath.Contains(UserInfo().Town));
            var data = queryable.Join(queryables, t => t.CookId, x => x.Id, (t, x) => new ResponseCookInfo()
            {
                Id = x.Id,
                Phone = x.Phone,
                IsVip = x.IsVip,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                TrueName = t.TrueName,
                IdCardNo = t.IdCardNo,
                IsEnable = x.IsDelete
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string StartUse(Guid Id)
        {
            CookVip vip = Kily.Set<CookVip>().Where(t => t.Id == Id).First();
            vip.IsDelete = false;
            if (UpdateField<CookVip>(vip, "IsDelete"))
                return ServiceMessage.UPDATESUCCESS;
            else
                return ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string BlockUp(Guid Id)
        {
            if (Delete<CookVip>(t => t.Id == Id))
                return ServiceMessage.UPDATESUCCESS;
            else
                return ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 确认缴费并分配角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string CheckPayment(Guid Id)
        {
            var exp = ExpressionExtension.GetExpression<CookRoleAuthor>("AuthorName", "基本", ExpressionEnum.NotLike);
            CookRoleAuthor author = Kily.Set<CookRoleAuthor>().Where(exp).Where(t => t.IsDelete == false).AsNoTracking().FirstOrDefault();
            CookVip vip = Kily.Set<CookVip>().Where(t => t.Id == Id).FirstOrDefault();
            vip.RoleId = author.Id;
            return UpdateField(vip, "RoleId") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        #endregion
    }
}
