using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using KilyCore.DataEntity.RequestMapper.Company;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Company;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Company;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Repositories.BaseRepository;
using KilyCore.Service.ConstMessage;
using KilyCore.Service.IServiceCore;
using KilyCore.Service.QueryExtend;
using Microsoft.EntityFrameworkCore;

namespace KilyCore.Service.ServiceCore
{
    public class CompanyService : Repository, ICompanyService
    {
        #region 资料审核
        /// <summary>
        /// 企业分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseCompany> GetCompanyPage(PageParamList<RequestCompany> pageParam)
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
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseCompany()
            {
                Id = t.Id,
                CompanyName = t.CompanyName,
                CompanyAccount = t.CompanyAccount,
                CompanyPhone = t.CompanyPhone,
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
        public ResponseCompany GetCompanyDetail(Guid Id)
        {
            var data = Kily.Set<CompanyInfo>().Where(t => t.IsDelete == false)
                 .Where(t => t.Id == Id)
                 .AsNoTracking().Select(t => new ResponseCompany()
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
        public PagedResult<ResponseCompanyIdent> GetCompanyIdentPage(PageParamList<RequestCompanyIdent> pageParam)
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
                .Join(queryables, x => x.CompanyId, y => y.Id, (x, y) => new ResponseCompanyIdent()
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
        public ResponseCompanyIdent GetCompanyIdentDetail(RequestCompanyIdent Param)
        {
            IQueryable<CompanyIdent> queryable = Kily.Set<CompanyIdent>().Where(t => t.Id == Param.Id);
            ResponseCompanyIdent data = null;
            //种养企业
            if (Param.CompanyType == CompanyEnum.Plant)
            {
                IQueryable<CompanyPlantIdentAttach> Plant = Kily.Set<CompanyPlantIdentAttach>();
                data = queryable.GroupJoin(Plant, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseCompanyIdent()
                {
                    Id = t.x.Id,
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
                data = queryable.GroupJoin(Production, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseCompanyIdent()
                {
                    Id = t.x.Id,
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
            //餐饮企业
            if (Param.CompanyType == CompanyEnum.Dining)
            {
                IQueryable<CompanyDiningIdentAttach> Dining = Kily.Set<CompanyDiningIdentAttach>();
                data = queryable.GroupJoin(Dining, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseCompanyIdent()
                {
                    Id = t.x.Id,
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
                    ImgMaterialOrder_F = t.y.FirstOrDefault().ImgMaterialOrder,
                    ImgDisinfection_F = t.y.FirstOrDefault().ImgDisinfection,
                    ImgMaterialSave_F = t.y.FirstOrDefault().ImgMaterialSave,
                    ImgAbandoned_F = t.y.FirstOrDefault().ImgAbandoned,
                    ImgSample_F = t.y.FirstOrDefault().ImgSample,
                    ImgWorkingPerson_F = t.y.FirstOrDefault().ImgWorkingPerson,
                    AuditInfo = o.MapToList<SystemAudit, ResponseAudit>()
                }).AsNoTracking().FirstOrDefault();
            }
            //流通企业
            if (Param.CompanyType == CompanyEnum.Circulation)
            {
                IQueryable<CompanyCirculationIdentAttach> Circulation = Kily.Set<CompanyCirculationIdentAttach>();
                data = queryable.GroupJoin(Circulation, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseCompanyIdent()
                {
                    Id = t.x.Id,
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
                data = queryable.GroupJoin(Other, x => x.Id, y => y.IdentId, (x, y) => new { x, y }).GroupJoin(Kily.Set<SystemAudit>(), t => t.x.Id, o => o.TableId, (t, o) => new ResponseCompanyIdent()
                {
                    Id = t.x.Id,
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
        #endregion
    }
}
