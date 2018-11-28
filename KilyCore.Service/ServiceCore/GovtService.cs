using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Govt;
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
* 类 名 称 ：GovtService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.ServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/6 14:37:52
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.ServiceCore
{
    public class GovtService : Repository, IGovtService
    {
        #region 政府监管
        /// <summary>
        /// 获取父级菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseGovtMenu> AddGovtParentMenu()
        {
            var query = Kily.Set<GovtMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.ParentId == null).AsNoTracking().AsQueryable();
            var data = query.Select(t => new ResponseGovtMenu()
            {
                MenuId = t.MenuId,
                MenuName = t.MenuName
            }).ToList();
            return data;
        }
        /// <summary>
        /// 政府菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtMenu> GetGovtMenuPage(PageParamList<RequestGovtMenu> pageParam)
        {
            var query = Kily.Set<GovtMenu>().Where(t => t.IsDelete == false).AsNoTracking().AsQueryable();
            if (!String.IsNullOrEmpty(pageParam.QueryParam.MenuName))
                query = query.Where(t => t.MenuName.Contains(pageParam.QueryParam.MenuName));
            var data = query.OrderByDescending(t => t.CreateTime).Select(t => new ResponseGovtMenu()
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
        /// 获取政府菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseGovtMenu GetGovtMenuDetail(Guid Id)
        {
            var data = Kily.Set<GovtMenu>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseGovtMenu()
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
        /// 删除政府菜单
        /// </summary>
        /// <param name="Id"></param>
        public string RemoveGovtMenu(Guid Id)
        {
            if (Delete<GovtMenu>(ExpressionExtension.GetExpression<GovtMenu>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 新增政府菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EditGovtMenu(RequestGovtMenu Param)
        {
            GovtMenu tree = Param.MapToEntity<GovtMenu>();
            if (Param.Id != Guid.Empty)
            {
                //修改
                if (Update<GovtMenu, RequestGovtMenu>(tree, Param))
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
                if (Insert<GovtMenu>(tree))
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
        public IList<ResponseParentTree> GetGovtTree()
        {
            IQueryable<ResponseParentTree> queryable = Kily.Set<GovtMenu>().Where(t => t.IsDelete == false)
                 .Where(t => t.Level == MenuEnum.LevelOne)
                 .AsNoTracking().Select(t => new ResponseParentTree()
                 {
                     Id = t.Id,
                     Text = t.MenuName,
                     Color = "black",
                     BackClolor = "white",
                     SelectedIcon = "fa fa-refresh fa-spin",
                     Nodes = Kily.Set<GovtMenu>().Where(x => x.IsDelete == false)
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

        #region 角色权限
        /// <summary>
        /// 权限分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtRoleAuthor> GetAuthorPage(PageParamList<RequestGovtRoleAuthor> pageParam)
        {
            IQueryable<GovtRoleAuthor> queryable = Kily.Set<GovtRoleAuthor>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AuthorName))
                queryable = queryable.Where(t => t.AuthorName.Contains(pageParam.QueryParam.AuthorName));
            var data = queryable.AsNoTracking().Select(t => new ResponseGovtRoleAuthor()
            {
                Id = t.Id,
                AuthorName = t.AuthorName,
                AuthorMenuPath = t.AuthorMenuPath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveAuthor(Guid Id)
        {
            return Remove<GovtRoleAuthor>(t => t.Id == Id) ? ServiceMessage.REMOVESUCCESS : ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditAuthor(RequestGovtRoleAuthor Param)
        {
            GovtRoleAuthor author = Param.MapToEntity<GovtRoleAuthor>();
            return Insert(author) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 政府账号
        /// <summary>
        /// 账号分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseGovtInfo> GetInfoPage(PageParamList<RequestGovtInfo> pageParam)
        {
            IQueryable<GovtInfo> queryable = Kily.Set<GovtInfo>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.DepartName))
                queryable = queryable.Where(t => t.DepartName.Contains(pageParam.QueryParam.DepartName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseGovtInfo()
            {
                Id = t.Id,
                DepartName = t.DepartName,
                Account = t.Account,
                AccountTypeName = AttrExtension.GetSingleDescription<GovtAccountEnum, DescriptionAttribute>(t.AccountType),
                TrueName = t.TrueName,
                Phone = t.Phone,
                Email = t.Email,
                TypePath=t.TypePath
            }).ToList();
            data.ForEach(t => {
                t.TableName = Kily.Set<SystemProvince>()
                .Where(x => x.Id.ToString() == t.Province)
                .Select(x => x.Name).AsNoTracking().FirstOrDefault() + "," +
                Kily.Set<SystemCity>()
                .Where(x => x.Id.ToString() == t.City)
                .Select(x => x.Name).AsNoTracking().FirstOrDefault() + "," +
                  Kily.Set<SystemArea>()
                .Where(x => x.Id.ToString() == t.Area)
                .Select(x => x.Name).AsNoTracking().FirstOrDefault();
            });
            return data.ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
        }
        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditInfo(RequestGovtInfo Param)
        {
            Param.PassWord = "123456";
            GovtInfo Info = Param.MapToEntity<GovtInfo>();
            return Insert(Info) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
        }
        #endregion
    }
}
