using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Company;
using KilyCore.EntityFrameWork.Model.Enterprise;
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

namespace KilyCore.Service.ServiceCore
{
    public class EnterpriseService : Repository, IEnterpriseService
    {
        #region 集团菜单
        /// <summary>
        /// 获取父级菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseMenu> GetEnterpriseParentMenu()
        {
            var query = Kily.Set<EnterpriseMenu>().Where(t => t.Level == MenuEnum.LevelOne).Where(t => t.ParentId == null).AsNoTracking().AsQueryable();
            var data = query.Select(t => new ResponseEnterpriseMenu()
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
        public ResponseEnterpriseMenu GetEnterpriseMenuDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseMenu>().Where(t => t.Id == Id).AsNoTracking().Select(t => new ResponseEnterpriseMenu()
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
        /// 企业菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseMenu> GetEnterpriseMenuPage(PageParamList<RequestEnterpriseMenu> pageParam)
        {
            var query = Kily.Set<EnterpriseMenu>().Where(t => t.IsDelete == false).AsNoTracking().AsQueryable();
            if (!String.IsNullOrEmpty(pageParam.QueryParam.MenuName))
                query = query.Where(t => t.MenuName.Contains(pageParam.QueryParam.MenuName));
            var data = query.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseMenu()
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
        public string RemoveEnterpriseMenu(Guid Id)
        {
            if (Delete<EnterpriseMenu>(ExpressionExtension.GetExpression<EnterpriseMenu>("Id", Id, ExpressionEnum.Equals)))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EditEnterpriseMenu(RequestEnterpriseMenu Param)
        {
            EnterpriseMenu tree = Param.MapToEntity<EnterpriseMenu>();
            if (Param.Id != Guid.Empty)
            {
                //修改
                if (Update<EnterpriseMenu, RequestEnterpriseMenu>(tree, Param))
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
                if (Insert<EnterpriseMenu>(tree))
                    return ServiceMessage.INSERTSUCCESS;
                else
                    return ServiceMessage.INSERTFAIL;
            }
        }
        #endregion

        #region 集团角色
        /// <summary>
        /// 集团角色分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseRoleAuthor> GetCompanyRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            IQueryable<EnterpriseRoleAuthor> queryable = Kily.Set<EnterpriseRoleAuthor>().Where(t => t.IsDelete == false);
            IQueryable<CompanyInfo> queryables = Kily.Set<CompanyInfo>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryables = queryables.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            var data = queryables.OrderBy(t => t.CreateTime).GroupJoin(queryable, t => t.Id, x => x.CompanyId, (t, x) => new ResponseEnterpriseRoleAuthor()
            {
                Id = x.FirstOrDefault().Id,
                CompanyId=t.Id,
                CompanyName = t.CompanyName,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                AuthorMenuPath = x.FirstOrDefault().AuthorMenuPath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 编辑集团角色菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditEnterpriseRoleAuthor(RequestEnterpriseRoleAuthor Param)
        {
            EnterpriseRoleAuthor RoleAuthor = Param.MapToEntity<EnterpriseRoleAuthor>();
            if (Param.Id != null)
            {
                return "";
            }
            else
            {
                if (Insert<EnterpriseRoleAuthor>(RoleAuthor))
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
        public IList<ResponseParentTree> GetEnterpriseTree()
        {
            IQueryable<ResponseParentTree> queryable = Kily.Set<EnterpriseMenu>().Where(t => t.IsDelete == false)
                 .Where(t => t.Level == MenuEnum.LevelOne)
                 .AsNoTracking().Select(t => new ResponseParentTree()
                 {
                     Id = t.Id,
                     Text = t.MenuName,
                     Color = "black",
                     BackClolor = "white",
                     SelectedIcon = "fa fa-refresh fa-spin",
                     Nodes = Kily.Set<EnterpriseMenu>().Where(x => x.IsDelete == false)
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
    }
}
