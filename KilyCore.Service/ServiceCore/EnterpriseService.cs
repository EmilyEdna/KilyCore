using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Company;
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
using System.Text;

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

        #region 集团角色
        /// <summary>
        /// 集团角色分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseRoleAuthor> GetCompanyRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            IQueryable<EnterpriseRoleAuthor> queryable = Kily.Set<EnterpriseRoleAuthor>();
            IQueryable<CompanyInfo> queryables = Kily.Set<CompanyInfo>().Where(t => t.IsDelete == false);
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
            if (Insert<EnterpriseRoleAuthor>(RoleAuthor))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
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
            CompanyInfo info = Kily.Set<CompanyInfo>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.Id).FirstOrDefault();
            info.EnterpriseRoleId = Param.EnterpriseRoleId;
            if (UpdateField<CompanyInfo>(info, "EnterpriseRoleId"))
                return ServiceMessage.HANDLESUCCESS;
            else
                return ServiceMessage.HANDLEFAIL;
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
            IQueryable<CompanyInfo> queryable = Kily.Set<CompanyInfo>().Where(t => t.IsDelete == false).AsQueryable().AsNoTracking();
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
                VersionName=AttrExtension.GetSingleDescription<SystemVersionEnum, DescriptionAttribute>(t.Version),
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
            var data = Kily.Set<CompanyInfo>().Where(t => t.IsDelete == false)
                 .Where(t => t.Id == Id)
                 .AsNoTracking().Select(t => new ResponseEnterprise()
                 {
                     Id = t.Id,
                     CompanyName = t.CompanyName,
                     CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                     CompanyAccount = t.CompanyAccount,
                     TypePath = t.TypePath,
                     CompanyAddress = t.CompanyAddress,
                     CommunityCode = t.CommunityCode,
                     SellerAddress = t.SellerAddress,
                     ProductionAddress = t.ProductionAddress,
                     CompanyPhone = t.CompanyPhone,
                     NetAddress = t.NetAddress,
                     VideoAddress = t.VideoAddress,
                     Discription = t.Discription,
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
                CompanyInfo Company = Kily.Set<CompanyInfo>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                Company.AuditType = Param.AuditType;
                if (UpdateField<CompanyInfo>(Company, "AuditType"))
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
            IQueryable<CompanyIdent> queryable = Kily.Set<CompanyIdent>().Where(t => t.IsDelete == false);
            IQueryable<CompanyInfo> queryables = Kily.Set<CompanyInfo>().Where(t => t.IsDelete == false);
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
            IQueryable<CompanyIdent> queryable = Kily.Set<CompanyIdent>().Where(t => t.Id == Param.Id);
            ResponseEnterpriseIdent data = null;
            //种植企业
            if (Param.CompanyType == CompanyEnum.Plant)
            {
                IQueryable<CompanyPlantIdentAttach> Plant = Kily.Set<CompanyPlantIdentAttach>();
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
                IQueryable<CompanyCultureIdentAttach> Plant = Kily.Set<CompanyCultureIdentAttach>();
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
                IQueryable<CompanyProductionIdentAttach> Production = Kily.Set<CompanyProductionIdentAttach>();
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
                IQueryable<CompanyCirculationIdentAttach> Circulation = Kily.Set<CompanyCirculationIdentAttach>();
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
                IQueryable<CompanyOtherIdentAttach> Other = Kily.Set<CompanyOtherIdentAttach>();
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
                CompanyIdent Ident = Kily.Set<CompanyIdent>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.TableId).FirstOrDefault();
                Ident.AuditType = Param.AuditType;
                if (UpdateField<CompanyIdent>(Ident, "AuditType"))
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
            CompanyInfo Info = Param.MapToEntity<CompanyInfo>();
            if (Insert<CompanyInfo>(Info))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 企业登录
        /// </summary>
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        public ResponseEnterprise EnterpriseLogin(RequestValidate LoginValidate)
        {
            IQueryable<CompanyInfo> queryable = Kily.Set<CompanyInfo>()
                .Where(t => t.CompanyAccount.Equals(LoginValidate.Account))
                .Where(t => t.PassWord.Equals(LoginValidate.PassWord))
                .Where(t => t.IsDelete == false);
            ResponseEnterprise Info = queryable.Select(t => new ResponseEnterprise()
            {
                Id = t.Id,
                CompanyAccount = t.CompanyAccount,
                CommunityCode = t.CommunityCode,
                CompanyAddress = t.CompanyAddress,
                CompanyName = t.CompanyName,
                CompanyPhone = t.CompanyPhone,
                CompanyType = t.CompanyType,
                CompanyTypeName = AttrExtension.GetSingleDescription<CompanyEnum, DescriptionAttribute>(t.CompanyType),
                AuditType = t.AuditType,
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                EnterpriseRoleId = t.EnterpriseRoleId,
                TypePath = t.TypePath,
                Certification = t.Certification,
                Honor = t.HonorCertification,
                Discription = t.Discription,
                NetAddress = t.NetAddress,
                ProductionAddress = t.ProductionAddress,
                SellerAddress = t.SellerAddress,
                VideoAddress = t.VideoAddress
            }).FirstOrDefault();
            return Info ?? null;
        }
        #endregion

        #region 集团企业后台系统
        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public IList<ResponseEnterpriseMenu> GetEnterpriseMenu()
        {
            IQueryable<EnterpriseMenu> queryable = Kily.Set<EnterpriseMenu>().Where(t => t.Level == MenuEnum.LevelOne)
                   .Where(t => t.IsDelete == false).AsNoTracking().AsQueryable().OrderBy(t => t.CreateTime);
            EnterpriseRoleAuthor Author = Kily.Set<EnterpriseRoleAuthor>().Where(t => t.IsDelete == false)
                .Where(t => t.Id == CompanyInfo().EnterpriseRoleId).AsNoTracking().FirstOrDefault();
            queryable = queryable.Where(t => Author.AuthorMenuPath.Contains(t.Id.ToString())).AsNoTracking();
            var data = queryable.Select(t => new ResponseEnterpriseMenu()
            {
                Id = t.Id,
                MenuId = t.MenuId,
                ParentId = t.ParentId,
                MenuAddress = t.MenuAddress,
                MenuName = t.MenuName,
                HasChildrenNode = t.HasChildrenNode,
                MenuIcon = t.MenuIcon,
                MenuChildren = Kily.Set<EnterpriseMenu>()
                    .Where(x => x.ParentId == t.MenuId)
                    .Where(x => x.Level != MenuEnum.LevelOne)
                    .Where(x => x.IsDelete == false)
                    .Where(x => Author.AuthorMenuPath.Contains(x.Id.ToString()))
                    .Select(x => new ResponseEnterpriseMenu()
                    {
                        Id = x.Id,
                        MenuId = x.MenuId,
                        ParentId = x.ParentId,
                        MenuAddress = x.MenuAddress,
                        MenuName = x.MenuName,
                        HasChildrenNode = x.HasChildrenNode,
                        MenuIcon = x.MenuIcon
                    }).ToList()
            }).ToList();
            return data;
        }
        #endregion

        #endregion
    }
}
