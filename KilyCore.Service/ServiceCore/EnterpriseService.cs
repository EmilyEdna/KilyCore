using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Enterprise;
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
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.ServiceCore
{
    public class EnterpriseService : Repository, IEnterpriseService
    {
        #region 集团菜单
        /// <summary>
        /// 父级菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseMenu> AddEnterpriseParentMenu()
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

        #region 权限菜单树
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        public IList<ResponseParentTree> GetEnterpriseTree(String key)
        {
            IQueryable<EnterpriseMenu> queryables = Kily.Set<EnterpriseMenu>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(key))
                queryables = queryables.Where(t => key.Contains(t.Id.ToString()));
            IQueryable<ResponseParentTree> queryable = Kily.Set<EnterpriseMenu>().Where(t => t.IsDelete == false)
                .Where(t => t.Level == MenuEnum.LevelOne)
                 .AsNoTracking().Select(t => new ResponseParentTree()
                 {
                     Id = t.Id,
                     Text = t.MenuName,
                     Color = "black",
                     BackClolor = "white",
                     State = string.IsNullOrEmpty(key) ? null : (queryables.Where(x => x.Id == t.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                     SelectedIcon = "fa fa-refresh fa-spin",
                     Nodes = Kily.Set<EnterpriseMenu>().Where(x => x.IsDelete == false)
                     .Where(x => x.Level != MenuEnum.LevelOne)
                     .Where(x => x.ParentId == t.MenuId).AsNoTracking()
                     .Select(x => new ResponseChildTree()
                     {
                         Id = x.Id,
                         Text = x.MenuName,
                         Color = "black",
                         State = string.IsNullOrEmpty(key) ? null : (queryables.Where(p => p.Id == x.Id).AsNoTracking().FirstOrDefault() != null ? new States { Checked = true } : null),
                         BackClolor = "white",
                         SelectedIcon = "fa fa-refresh fa-spin",
                     }).AsQueryable()
                 }).AsQueryable();
            var data = queryable.ToList();
            return data;
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
            IQueryable<EnterpriseRoleAuthor> queryable = Kily.Set<EnterpriseRoleAuthor>();
            IQueryable<EnterpriseInfo> queryables = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryables = queryables.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            var data = queryables.OrderBy(t => t.CreateTime).GroupJoin(queryable, t => t.EnterpriseRoleId, x => x.Id, (t, x) => new ResponseEnterpriseRoleAuthor()
            {
                Id = t.Id,
                EnterpriseRoleId = x.FirstOrDefault().Id,
                CompanyName = t.CompanyName,
                EnterpriseRoleName = x.FirstOrDefault().EnterpriseRoleName,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                AuthorMenuPath = x.FirstOrDefault().AuthorMenuPath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseRoleAuthor> WatchRolePage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            IQueryable<EnterpriseRoleAuthor> queryable = Kily.Set<EnterpriseRoleAuthor>();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.EnterpriseRoleName))
                queryable = queryable.Where(t => t.EnterpriseRoleName.Contains(pageParam.QueryParam.EnterpriseRoleName));
            var data = queryable.Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseRoleAuthor()
            {
                Id = t.Id,
                EnterpriseRoleName = t.EnterpriseRoleName,
                AuthorMenuPath = t.AuthorMenuPath
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
            if (Param.Id == Guid.Empty)
            {
                if (Kily.Set<EnterpriseRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.EnterpriseRoleName.Equals(RoleAuthor.EnterpriseRoleName)).AsNoTracking().FirstOrDefault() != null)
                    return "角色名称重复请重新添加!";
                return Insert<EnterpriseRoleAuthor>(RoleAuthor) ? ServiceMessage.INSERTSUCCESS : ServiceMessage.INSERTFAIL;
            }
            else
                return Update<EnterpriseRoleAuthor, RequestEnterpriseRoleAuthor>(RoleAuthor, Param) ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 角色分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseRoleAuthor> GetRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            IQueryable<EnterpriseRoleAuthor> queryable = Kily.Set<EnterpriseRoleAuthor>();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.EnterpriseRoleName))
                queryable = queryable.Where(t => t.EnterpriseRoleName.Contains(pageParam.QueryParam.EnterpriseRoleName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterpriseRoleAuthor()
            {
                Id = t.Id,
                EnterpriseRoleName = t.EnterpriseRoleName,
                AuthorMenuPath = t.AuthorMenuPath
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 删除集团角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RemoveEnterpriseRoleAuthor(Guid Id)
        {
            if (Remove<EnterpriseRoleAuthor>(t => t.Id == Id))
                return ServiceMessage.REMOVESUCCESS;
            else
                return ServiceMessage.REMOVEFAIL;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<ResponseEnterpriseRoleAuthor> GetRoleAuthorList()
        {
            return Kily.Set<EnterpriseRoleAuthor>().Select(t => new ResponseEnterpriseRoleAuthor()
            {
                Id = t.Id,
                EnterpriseRoleName = t.EnterpriseRoleName
            }).ToList();
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string DistributionRole(RequestEnterpriseRoleAuthor Param)
        {
            EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.Id).FirstOrDefault();
            if (Param.EnterpriseRoleName.Contains("基本"))
                info.Version = SystemVersionEnum.Normal;
            if (Param.EnterpriseRoleName.Contains("体验"))
                info.Version = SystemVersionEnum.Test;
            if (Param.EnterpriseRoleName.Contains("基础"))
                info.Version = SystemVersionEnum.Base;
            if (Param.EnterpriseRoleName.Contains("升级"))
                info.Version = SystemVersionEnum.Level;
            if (Param.EnterpriseRoleName.Contains("旗舰"))
                info.Version = SystemVersionEnum.Enterprise;
            info.EnterpriseRoleId = Param.EnterpriseRoleId;
            if (UpdateField<EnterpriseInfo>(info, "EnterpriseRoleId"))
                return ServiceMessage.HANDLESUCCESS;
            else
                return ServiceMessage.HANDLEFAIL;
        }
        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterpriseRoleAuthor GetEnterpriseRoleAuthorDetail(Guid Id)
        {
            return Kily.Set<EnterpriseRoleAuthor>().Where(t => t.Id == Id).Select(t => new ResponseEnterpriseRoleAuthor()
            {
                Id = t.Id,
                EnterpriseRoleName = t.EnterpriseRoleName,
                AuthorMenuPath = t.AuthorMenuPath
            }).AsNoTracking().FirstOrDefault();
        }
        #endregion

        #region 资料审核
        /// <summary>
        /// 企业分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterprise> GetCompanyPage(PageParamList<RequestEnterprise> pageParam)
        {
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).AsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
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
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseEnterprise()
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                CompanyAccount = t.CompanyAccount,
                CompanyPhone = t.CompanyPhone,
                VersionName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.Version),
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                TableName = t.GetType().Name,
                AuditType = t.AuditType
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 获取企业详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEnterprise GetCompanyDetail(Guid Id)
        {
            var data = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false)
                 .Where(t => t.Id == Id)
                 .AsNoTracking().Select(t => new ResponseEnterprise()
                 {
                     Id = t.Id,
                     CompanyName = t.CompanyName,
                     CardExpiredDate = t.CardExpiredDate,
                     SafeOffer = t.SafeOffer,
                     OfferLv = t.OfferLv,
                     CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                     CompanyAccount = t.CompanyAccount,
                     TypePath = t.TypePath,
                     CompanyAddress = t.CompanyAddress,
                     CommunityCode = t.CommunityCode,
                     SellerAddress = t.SellerAddress,
                     ProductionAddress = t.ProductionAddress,
                     CompanyPhone = t.CompanyPhone,
                     NetAddress = t.NetAddress,
                     Discription = t.Discription,
                     VideoAddress = t.VideoAddress,
                     Certification = t.Certification,
                     Honor = t.HonorCertification,
                     AuditDetails = Kily.Set<SystemAudit>().Where(x => x.IsDelete == false).
                     Where(x => x.TableId == t.Id).Select(x => new ResponseAudit()
                     {
                         Id = x.Id,
                         TableId = t.Id,
                         TabelName = t.GetType().Name,
                         CreateTime = x.CreateTime,
                         AuditSuggestion = x.AuditSuggestion,
                         AuditName = x.AuditName,
                         CreateUser = x.CreateUser
                     }).FirstOrDefault(),
                     AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType)
                 }).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 审核企业
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string AuditCompany(RequestAudit Param)
        {
            Param.AuditName = UserInfo().TrueName;
            SystemAudit Audit = Param.MapToEntity<SystemAudit>();
            if (Insert<SystemAudit>(Audit))
            {
                EnterpriseInfo Company = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                Company.AuditType = Param.AuditType;
                if (UpdateField<EnterpriseInfo>(Company, "AuditType"))
                    return ServiceMessage.HANDLESUCCESS;
                else
                    return ServiceMessage.HANDLEFAIL;
            }
            else
                return ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 认证审核
        /// <summary>
        /// 企业认证分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseIdent> GetCompanyIdentPage(PageParamList<RequestEnterpriseIdent> pageParam)
        {
            IQueryable<EnterpriseIdent> queryable = Kily.Set<EnterpriseIdent>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseInfo> queryables = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            if (pageParam.QueryParam.CompanyType.HasValue)
                queryable = queryable.Where(t => t.CompanyType == pageParam.QueryParam.CompanyType);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryables = queryables.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
            if (UserInfo().AccountType == AccountEnum.Province)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Area));
            if (UserInfo().AccountType == AccountEnum.Village)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Town));
            var data = queryable.OrderByDescending(t => t.CreateTime)
                .Join(queryables, x => x.CompanyId, y => y.Id, (x, y) => new ResponseEnterpriseIdent()
                {
                    Id = x.Id,
                    IdentNo = x.IdentNo,
                    CompanyName = x.CompanyName,
                    IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(x.IdentStar),
                    Representative = x.Representative,
                    LinkPhone = x.LinkPhone,
                    AuditType = x.AuditType,
                    AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(x.AuditType),
                    CompanyType = x.CompanyType,
                    CommunityCode = x.CommunityCode,
                    TableName = x.GetType().Name,
                    AuditInfo = Kily.Set<SystemAudit>()
                    .Where(t => t.IsDelete == false)
                    .Where(t => t.TableId == x.Id).ToList().MapToList<SystemAudit, ResponseAudit>()
                }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 企业认证详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public ResponseEnterpriseIdent GetCompanyIdentDetail(RequestEnterpriseIdent Param)
        {
            IQueryable<EnterpriseIdent> queryable = Kily.Set<EnterpriseIdent>().Where(t => t.Id == Param.Id);
            ResponseEnterpriseIdent data = null;
            //种植企业
            if (Param.CompanyType == CompanyEnum.Plant)
            {
                IQueryable<EnterprisePlantIdentAttach> Plant = Kily.Set<EnterprisePlantIdentAttach>();
                data = queryable.GroupJoin(Plant, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseEnterpriseIdent()
                {
                    Id = t.x.Id,
                    IdentStartTime = t.x.IdentStartTime,
                    IdentEndTime = t.x.IdentEndTime,
                    IdentNo = t.x.IdentNo,
                    CompanyName = t.x.CompanyName,
                    CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.x.CompanyType),
                    IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.x.IdentStar),
                    Representative = t.x.Representative,
                    LinkPhone = t.x.LinkPhone,
                    IdentYear = t.x.IdentYear,
                    CommunityCode = t.x.CommunityCode,
                    RepresentativeCard = t.x.RepresentativeCard,
                    SendPerson = t.x.SendPerson,
                    SendCard = t.x.SendCard,
                    Remark = t.x.Remark,
                    ImgCard = t.y.FirstOrDefault().ImgCard,
                    ImgApply = t.y.FirstOrDefault().ImgApply,
                    ImgResearch = t.y.FirstOrDefault().ImgResearch,
                    ImgAgreement = t.y.FirstOrDefault().ImgAgreement,
                    ImgOther = t.y.FirstOrDefault().ImgOther,
                    ImgQualified_X = t.y.FirstOrDefault().ImgQualified,
                    ImgWater_X = t.y.FirstOrDefault().ImgWater,
                    ImgSoil_X = t.y.FirstOrDefault().ImgSoil,
                    ImgMetal_X = t.y.FirstOrDefault().ImgMetal,
                    AuditInfo = o.MapToList<SystemAudit, ResponseAudit>()
                }).AsNoTracking().FirstOrDefault();
            }
            //养殖企业
            if (Param.CompanyType == CompanyEnum.Culture)
            {
                IQueryable<EnterpriseCultureIdentAttach> Plant = Kily.Set<EnterpriseCultureIdentAttach>();
                data = queryable.GroupJoin(Plant, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseEnterpriseIdent()
                {
                    Id = t.x.Id,
                    IdentStartTime = t.x.IdentStartTime,
                    IdentEndTime = t.x.IdentEndTime,
                    IdentNo = t.x.IdentNo,
                    CompanyName = t.x.CompanyName,
                    CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.x.CompanyType),
                    IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.x.IdentStar),
                    Representative = t.x.Representative,
                    LinkPhone = t.x.LinkPhone,
                    IdentYear = t.x.IdentYear,
                    CommunityCode = t.x.CommunityCode,
                    RepresentativeCard = t.x.RepresentativeCard,
                    SendPerson = t.x.SendPerson,
                    SendCard = t.x.SendCard,
                    Remark = t.x.Remark,
                    ImgCard = t.y.FirstOrDefault().ImgCard,
                    ImgApply = t.y.FirstOrDefault().ImgApply,
                    ImgResearch = t.y.FirstOrDefault().ImgResearch,
                    ImgAgreement = t.y.FirstOrDefault().ImgAgreement,
                    ImgOther = t.y.FirstOrDefault().ImgOther,
                    ImgQualified_X = t.y.FirstOrDefault().ImgQualified,
                    ImgWater_X = t.y.FirstOrDefault().ImgWater,
                    ImgSoil_X = t.y.FirstOrDefault().ImgSoil,
                    ImgMetal_X = t.y.FirstOrDefault().ImgMetal,
                    AuditInfo = o.MapToList<SystemAudit, ResponseAudit>()
                }).AsNoTracking().FirstOrDefault();
            }
            //生产企业
            if (Param.CompanyType == CompanyEnum.Production)
            {
                IQueryable<EnterpriseProductionIdentAttach> Production = Kily.Set<EnterpriseProductionIdentAttach>();
                data = queryable.GroupJoin(Production, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseEnterpriseIdent()
                {
                    Id = t.x.Id,
                    IdentStartTime = t.x.IdentStartTime,
                    IdentEndTime = t.x.IdentEndTime,
                    IdentNo = t.x.IdentNo,
                    CompanyName = t.x.CompanyName,
                    CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.x.CompanyType),
                    IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.x.IdentStar),
                    Representative = t.x.Representative,
                    LinkPhone = t.x.LinkPhone,
                    IdentYear = t.x.IdentYear,
                    CommunityCode = t.x.CommunityCode,
                    RepresentativeCard = t.x.RepresentativeCard,
                    SendPerson = t.x.SendPerson,
                    SendCard = t.x.SendCard,
                    Remark = t.x.Remark,
                    ImgCard = t.y.FirstOrDefault().ImgCard,
                    ImgApply = t.y.FirstOrDefault().ImgApply,
                    ImgResearch = t.y.FirstOrDefault().ImgResearch,
                    ImgAgreement = t.y.FirstOrDefault().ImgAgreement,
                    ImgOther = t.y.FirstOrDefault().ImgOther,
                    ImgProduction_Y = t.y.FirstOrDefault().ImgProduction,
                    ImgDrugs_Y_M = t.y.FirstOrDefault().ImgDrugs,
                    ImgHygiene_Y_M = t.y.FirstOrDefault().ImgHygiene,
                    AuditInfo = o.MapToList<SystemAudit, ResponseAudit>()
                }).AsNoTracking().FirstOrDefault();
            }
            //流通企业
            if (Param.CompanyType == CompanyEnum.Circulation)
            {
                IQueryable<EnterpriseCirculationIdentAttach> Circulation = Kily.Set<EnterpriseCirculationIdentAttach>();
                data = queryable.GroupJoin(Circulation, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseEnterpriseIdent()
                {
                    Id = t.x.Id,
                    IdentStartTime = t.x.IdentStartTime,
                    IdentEndTime = t.x.IdentEndTime,
                    IdentNo = t.x.IdentNo,
                    CompanyName = t.x.CompanyName,
                    CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.x.CompanyType),
                    IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.x.IdentStar),
                    Representative = t.x.Representative,
                    LinkPhone = t.x.LinkPhone,
                    IdentYear = t.x.IdentYear,
                    CommunityCode = t.x.CommunityCode,
                    RepresentativeCard = t.x.RepresentativeCard,
                    SendPerson = t.x.SendPerson,
                    SendCard = t.x.SendCard,
                    Remark = t.x.Remark,
                    ImgCard = t.y.FirstOrDefault().ImgCard,
                    ImgApply = t.y.FirstOrDefault().ImgApply,
                    ImgResearch = t.y.FirstOrDefault().ImgResearch,
                    ImgAgreement = t.y.FirstOrDefault().ImgAgreement,
                    ImgOther = t.y.FirstOrDefault().ImgOther,
                    ImgCirculation_M = t.y.FirstOrDefault().ImgCirculation,
                    ImgImportExport_M = t.y.FirstOrDefault().ImgImportExport,
                    ImgDrugs_Y_M = t.y.FirstOrDefault().ImgDrugs,
                    ImgHygiene_Y_M = t.y.FirstOrDefault().ImgHygiene,
                    AuditInfo = o.MapToList<SystemAudit, ResponseAudit>()
                }).AsNoTracking().FirstOrDefault();
            }
            //其他企业
            if (Param.CompanyType == CompanyEnum.Other)
            {
                IQueryable<EnterpriseOtherIdentAttach> Other = Kily.Set<EnterpriseOtherIdentAttach>();
                data = queryable.GroupJoin(Other, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseEnterpriseIdent()
                {
                    Id = t.x.Id,
                    IdentNo = t.x.IdentNo,
                    IdentStartTime = t.x.IdentStartTime,
                    IdentEndTime = t.x.IdentEndTime,
                    CompanyName = t.x.CompanyName,
                    CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.x.CompanyType),
                    IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.x.IdentStar),
                    Representative = t.x.Representative,
                    LinkPhone = t.x.LinkPhone,
                    IdentYear = t.x.IdentYear,
                    CommunityCode = t.x.CommunityCode,
                    RepresentativeCard = t.x.RepresentativeCard,
                    SendPerson = t.x.SendPerson,
                    SendCard = t.x.SendCard,
                    Remark = t.x.Remark,
                    ImgCard = t.y.FirstOrDefault().ImgCard,
                    ImgApply = t.y.FirstOrDefault().ImgApply,
                    ImgResearch = t.y.FirstOrDefault().ImgResearch,
                    ImgAgreement = t.y.FirstOrDefault().ImgAgreement,
                    ImgOther = t.y.FirstOrDefault().ImgOther,
                    AuditInfo = o.MapToList<SystemAudit, ResponseAudit>()
                }).AsNoTracking().FirstOrDefault();
            }
            return data;
        }
        /// <summary>
        /// 审核认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditIdent(RequestAudit Param)
        {
            Param.AuditName = UserInfo().TrueName;
            SystemAudit Audit = Param.MapToEntity<SystemAudit>();
            if (Insert<SystemAudit>(Audit))
            {
                EnterpriseIdent Ident = Kily.Set<EnterpriseIdent>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                Ident.AuditType = Param.AuditType;
                if (UpdateField<EnterpriseIdent>(Ident, "AuditType"))
                    return ServiceMessage.HANDLESUCCESS;
                else
                    return ServiceMessage.HANDLEFAIL;
            }
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 认证缴费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditPayment(RequestPayment Param)
        {
            SystemPayment payment = Param.MapToEntity<SystemPayment>();
            if (Insert<SystemPayment>(payment))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        #endregion

        #region 登录注册
        /// <summary>
        /// 集团客服注册账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string RegistCompanyAccount(RequestEnterprise Param)
        {
            Param.AuditType = AuditEnum.WaitAduit;
            EnterpriseRoleAuthor Author = Kily.Set<EnterpriseRoleAuthor>().Where(t => t.IsDelete == false).Where(t => t.EnterpriseRoleName.Contains("基本")).OrderBy(t => t.CreateTime).FirstOrDefault();
            Param.EnterpriseRoleId = Author.Id;
            EnterpriseInfo Info = Param.MapToEntity<EnterpriseInfo>();
            if (Insert<EnterpriseInfo>(Info))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 企业登录
        /// </summary>
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        public Object EnterpriseLogin(RequestValidate LoginValidate)
        {
            #region 公司账号登录
            IQueryable<EnterpriseInfo> queryable = Kily.Set<EnterpriseInfo>()
                .Where(t => t.CompanyAccount.Equals(LoginValidate.Account) || t.CompanyPhone.Equals(LoginValidate.Account))
                .Where(t => t.PassWord.Equals(LoginValidate.PassWord))
                .Where(t => t.IsDelete == false);
            ResponseEnterprise Info = queryable.Select(t => new ResponseEnterprise()
            {
                Id = t.Id,
                CompanyId = t.CompanyId,
                CompanyAccount = t.CompanyAccount,
                CommunityCode = t.CommunityCode,
                CompanyAddress = t.CompanyAddress,
                CompanyName = t.CompanyName,
                CompanyPhone = t.CompanyPhone,
                CompanyType = t.CompanyType,
                Version = t.Version,
                VersionName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.Version),
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                AuditType = t.AuditType,
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                EnterpriseRoleId = t.EnterpriseRoleId,
                TypePath = t.TypePath,
                LngAndLat = t.LngAndLat,
                Certification = t.Certification,
                Honor = t.HonorCertification,
                Discription = t.Discription,
                CodeStar=t.CodeStar,
                NetAddress = t.NetAddress,
                ProductionAddress = t.ProductionAddress,
                SellerAddress = t.SellerAddress,
                TableName = typeof(ResponseEnterprise).Name
            }).FirstOrDefault();
            #endregion
            #region 公司子账号登录
            IQueryable<EnterpriseUser> queryables = Kily.Set<EnterpriseUser>()
                .Where(t => t.Account.Equals(LoginValidate.Account) || t.Phone.Equals(LoginValidate.Account))
                .Where(t => t.PassWord.Equals(LoginValidate.PassWord))
                .Where(t => t.IsDelete == false);
            ResponseEnterpriseUser User = queryables.Select(t => new ResponseEnterpriseUser()
            {
                Id = t.CompanyId,
                CompanyId = t.Id,
                IdCard = t.IdCard,
                CompanyName = t.CompanyName,
                CompanyType = t.CompanyType,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                Version = t.Version,
                VersionName = AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.Version),
                Account = t.Account,
                Phone = t.Phone,
                CodeStar = t.CodeStar,
                LngAndLat = t.Account,
                TypePath = t.TypePath,
                RoleAuthorType = t.RoleAuthorType,
                TrueName = t.TrueName,
                TableName = typeof(ResponseEnterpriseUser).Name
            }).FirstOrDefault();
            #endregion
            if (Info != null)
                return Info;
            else if (User != null)
                return User;
            else return null;
        }
        #endregion

        #region 标签管理
        /// <summary>
        /// 二维码审核的标签分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseApply> GetTagAuditPage(PageParamList<RequestEnterpriseApply> pageParam)
        {
            IQueryable<EnterpriseTagApply> queryable = Kily.Set<EnterpriseTagApply>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseInfo> queryables = Kily.Set<EnterpriseInfo>();
            if (UserInfo().AccountType == AccountEnum.Province)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Province));
            if (UserInfo().AccountType == AccountEnum.City)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().City));
            if (UserInfo().AccountType == AccountEnum.Area)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Area));
            if (UserInfo().AccountType == AccountEnum.Village)
                queryables = queryables.Where(t => t.TypePath.Contains(UserInfo().Town));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.BatchNo))
                queryable = queryable.Where(t => t.BatchNo.Contains(pageParam.QueryParam.BatchNo));
            var data = queryable.Join(queryables, t => t.CompanyId, y => y.Id, (t, y) => new ResponseEnterpriseApply()
            {
                Id = t.Id,
                BatchNo = t.BatchNo,
                TagTypeName = AttrExtension.GetSingleDescription<TagEnum, DescriptionAttribute>(t.TagType),
                ApplyNum = t.ApplyNum,
                ApplyMoney = t.ApplyMoney,
                Payment = t.Payment,
                IsPay = t.IsPay,
                PaytTicket = t.PaytTicket,
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                TableName = t.GetType().Name
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 审核标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditCode(RequestAudit Param)
        {
            Param.AuditName = UserInfo().TrueName;
            SystemAudit Audit = Param.MapToEntity<SystemAudit>();
            if (Insert<SystemAudit>(Audit))
            {
                EnterpriseTagApply TagApply = Kily.Set<EnterpriseTagApply>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                EnterpriseInfo info = Kily.Set<EnterpriseInfo>().Where(t => t.Id == TagApply.CompanyId).FirstOrDefault();
                info.TagCodeNum = Convert.ToInt64(TagApply.ApplyNum);
                UpdateField<EnterpriseInfo>(info, "TagCodeNum");
                TagApply.AuditType = Param.AuditType;
                if (UpdateField<EnterpriseTagApply>(TagApply, "AuditType"))
                    return ServiceMessage.HANDLESUCCESS;
                else
                    return ServiceMessage.HANDLEFAIL;
            }
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 获取审核详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IList<ResponseAudit> GetTagAuditDetail(RequestAudit Param)
        {
            var data = Kily.Set<SystemAudit>().Where(t => t.TableName.Contains(Param.TableName))
                .Where(t => t.TableId == Param.TableId).Select(t => new ResponseAudit()
                {
                    AuditName = t.AuditName,
                    CreateTime = t.CreateTime,
                    AuditSuggestion = t.AuditSuggestion,
                    AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType)
                }).AsNoTracking().ToList();
            return data;
        }
        #endregion

        #region  产品审核
        /// <summary>
        /// 产品审核列表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseGoodsStock> GetWaitAuditGoodPage(PageParamList<RequestEnterpriseGoodsStock> pageParam)
        {
            IQueryable<EnterpriseInfo> info = Kily.Set<EnterpriseInfo>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoods> goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseGoodsStock> stocks = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.TempPath))
                info = info.Where(t => t.TypePath.Contains(pageParam.QueryParam.TempPath));
            if (!string.IsNullOrEmpty(pageParam.QueryParam.GoodsName))
                goods = goods.Where(t => t.ProductName.Contains(pageParam.QueryParam.GoodsName));
            var data = goods.Join(info, x => x.CompanyId, y => y.Id, (x, y) => new { x }).Join(stocks, t => t.x.Id, p => p.GoodsId, (t, p) => new ResponseEnterpriseGoodsStock()
            {
                Id = p.Id,
                GoodsId = t.x.Id,
                GoodsName = t.x.ProductName,
                Spec = t.x.Spec,
                Unit = t.x.Unit,
                ExpiredDate = t.x.ExpiredDate,
                AuditType = t.x.AuditType,
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.x.AuditType),
                ImgUrl = p.ImgUrl,
                Remark = p.Remark
            }).Distinct().AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 审核产品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string AuditGoodSuccess(Guid Id)
        {
            EnterpriseGoods goods = Kily.Set<EnterpriseGoods>().Where(t => t.Id == Id).FirstOrDefault();
            goods.AuditType = AuditEnum.AuditSuccess;
            return UpdateField(goods, "AuditType") ? ServiceMessage.UPDATESUCCESS : ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 产品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Object GetAuditProductDetail(Guid Id)
        {
            IQueryable<EnterpriseGoodsStockAttach> GoodsStockAttach = Kily.Set<EnterpriseGoodsStockAttach>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoodsStock> GoodStock = Kily.Set<EnterpriseGoodsStock>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseGoods> Goods = Kily.Set<EnterpriseGoods>().Where(t => t.IsDelete == false).OrderByDescending(t => t.CreateTime);
            IQueryable<EnterpriseProductionBatch> ProductionBatch = Kily.Set<EnterpriseProductionBatch>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseStockType> StockType = Kily.Set<EnterpriseStockType>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseMaterialStockAttach> MaterialStockAttach = Kily.Set<EnterpriseMaterialStockAttach>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseMaterialStock> MaterialStock = Kily.Set<EnterpriseMaterialStock>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseMaterial> Material = Kily.Set<EnterpriseMaterial>().Where(t => t.IsDelete == false).AsNoTracking();
            IQueryable<EnterpriseProductSeries> ProductSeries = Kily.Set<EnterpriseProductSeries>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseCheckMaterial> CheckMaterial = Kily.Set<EnterpriseCheckMaterial>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseCheckGoods> CheckGoods = Kily.Set<EnterpriseCheckGoods>().Where(t => t.IsDelete == false);
            //产品的查询
            var GoodsData = GoodsStockAttach.Join(GoodStock, a => a.StockId, b => b.Id, (a, b) => new { a, b })
                 .Join(ProductionBatch, c => c.b.BatchId, d => d.Id, (c, d) => new { c, d })
                 .Join(Goods, e => e.c.b.GoodsId, f => f.Id, (e, f) => new { e, f })
                 .GroupJoin(CheckGoods, g => g.e.c.b.CheckGoodsId, h => h.Id, (g, h) => new { g, h })
                 .Join(ProductSeries, i => i.g.f.ProductSeriesId, j => j.Id, (i, j) => new { i, j })
                 .Join(StockType, k => k.i.g.e.c.b.StockTypeId, l => l.Id, (k, l) => new { k, l }).Where(t => t.k.i.g.f.Id == Id)
                 .AsNoTracking();
            //原材料的查询
            var MaterialsData = MaterialStockAttach.Join(MaterialStock, a => a.MaterialStockId, b => b.Id, (a, b) => new { a, b })
                 .Join(Material, c => c.b.BatchNo, d => d.BatchNo, (c, d) => new { c, d })
                 .GroupJoin(CheckMaterial, e => e.c.b.CheckMaterialId, f => f.Id, (e, f) => new { e, f }).AsNoTracking();
            var GoodData = GoodsData.Select(t => new
            {
                GoodsName = t.k.i.g.f.ProductName,
                ExpiredDay = t.k.i.g.f.ExpiredDate,
                t.k.i.g.f.Spec,
                t.k.i.g.f.ProductType,
                t.k.j.Standard,
                t.k.i.g.e.d.DeviceName,
                OutNo = t.k.i.g.e.c.a.GoodsBatchNo,
                t.k.i.g.e.c.a.OutStockTime,
                t.k.i.g.e.c.a.OutStockUser,
                t.k.i.g.e.c.b.Manager,
                t.k.i.g.e.c.b.ProductTime,
                InNo = t.k.i.g.e.c.b.GoodsBatchNo,
                t.k.i.g.e.d.MaterialId,
                t.k.i.h.FirstOrDefault().CheckUint,
                t.k.i.h.FirstOrDefault().CheckUser,
                t.k.i.h.FirstOrDefault().CheckResult,
                t.k.i.h.FirstOrDefault().CheckReport,
                t.l.StockName,
                t.l.SaveType,
                t.l.SaveH2,
                t.l.SaveTemp
            }).FirstOrDefault();
            var MaterialData = MaterialsData.Where(t => GoodData.MaterialId.Contains(t.e.d.Id.ToString())).Select(t => new
            {
                t.f.FirstOrDefault().CheckUint,
                t.f.FirstOrDefault().CheckUser,
                t.f.FirstOrDefault().CheckResult,
                t.f.FirstOrDefault().CheckReport,
                t.e.d.Address,
                t.e.d.ExpiredDay,
                t.e.d.MaterName,
                t.e.d.MaterType,
                t.e.d.Supplier,
                t.e.c.b.ProductTime
            }).FirstOrDefault();
            return new
            {
                GoodData.StockName,
                GoodData.SaveH2,
                GoodData.SaveType,
                GoodData.SaveTemp,
                GoodData.GoodsName,
                GoodData.ExpiredDay,
                GoodData.Spec,
                GoodData.ProductType,
                GoodData.Standard,
                GoodData.DeviceName,
                GoodData.OutNo,
                GoodData.OutStockTime,
                GoodData.OutStockUser,
                GoodData.Manager,
                GoodProductTime = GoodData.ProductTime,
                GoodData.InNo,
                GoodCheckUint = GoodData.CheckUint,
                GoodCheckUser = GoodData.CheckUser,
                GoodCheckResult = GoodData.CheckResult,
                GoodCheckReport = GoodData.CheckReport,
                MaterialCheckUint = MaterialData.CheckUint,
                MaterialCheckUser = MaterialData.CheckUser,
                MaterialCheckResult = MaterialData.CheckResult,
                MaterialCheckReport = MaterialData.CheckReport,
                MaterialAddress = MaterialData.Address,
                MaterialExpiredDay = MaterialData.ExpiredDay,
                MaterialData.MaterName,
                MaterialData.MaterType,
                MaterialData.Supplier,
                MaterialProductTime = MaterialData.ProductTime
            };
        }
        #endregion
    }
}