using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.EntityFrameWork.ModelEnum;
using KilyCore.Extension.AttributeExtension;
using KilyCore.Extension.AutoMapperExtension;
using KilyCore.Extension.EmailExtension;
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
using KilyCore.EntityFrameWork.Model.Repast;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.ServiceCore
{
    public class FinanceService : Repository, IFinanceService
    {
        #region 加盟缴费-财务
        /// <summary>
        /// 加盟缴费分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseAdminAttach> GetJoinPayPage(PageParamList<RequestAdminAttach> pageParam)
        {
            IQueryable<SystemAdminAttach> AdminAttach = Kily.Set<SystemAdminAttach>();
            IQueryable<SystemAdmin> Admin = Kily.Set<SystemAdmin>();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.Account))
                Admin = Admin.Where(t => t.Account.Equals(pageParam.QueryParam.Account));
            var data = Admin.OrderBy(t => t.CreateTime).GroupJoin(AdminAttach, t => t.Id, x => x.AdminId, (t, x) => new ResponseAdminAttach()
            {
                AdminId = t.Id,
                CompanyName = t.CompanyName,
                TrueName = t.TrueName,
                AccountType = t.AccountType,
                AccountTypeName = AttrExtension.GetSingleDescription<AccountEnum, DescriptionAttribute>(t.AccountType),
                IdCard = t.IdCard,
                Phone = t.Phone,
                Email = t.Email,
                StartTime = x.FirstOrDefault().StartTime,
                EndTime = x.FirstOrDefault().EndTime,
                IsPay = x.FirstOrDefault().IsPay,
                Money = x.FirstOrDefault().Money,
                PayUser = x.FirstOrDefault().PayUser,
                IsDelete = t.IsDelete
            }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string StartUse(Guid Id)
        {
            SystemAdmin Admin = Kily.Set<SystemAdmin>().Where(t => t.Id == Id).First();
            Admin.IsDelete = false;
            if (UpdateField<SystemAdmin>(Admin, "IsDelete"))
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
            if (Delete<SystemAdmin>(t => t.Id == Id))
                return ServiceMessage.UPDATESUCCESS;
            else
                return ServiceMessage.UPDATEFAIL;
        }
        /// <summary>
        /// 加盟归档
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Archive(RequestAdminAttach param)
        {
            SystemAdminAttach AdminAttach = param.MapToEntity<SystemAdminAttach>();
            if (Insert<SystemAdminAttach>(AdminAttach))
                return ServiceMessage.INSERTSUCCESS;
            else
                return ServiceMessage.INSERTFAIL;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public string SendEmail(RequestEMail Param)
        {
            if (EmailExSend.SendEmail(Param.Receive, Param.Title, Param.Content))
                return ServiceMessage.HANDLESUCCESS;
            else
                return ServiceMessage.HANDLEFAIL;
        }
        #endregion

        #region 企业认证-财务
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseIdent> IdentEnterprisePay(PageParamList<RequestEnterpriseIdent> pageParam)
        {
            IQueryable<EnterpriseIdent> queryable = Kily.Set<EnterpriseIdent>().Where(t => t.IsDelete == false);
            queryable = queryable.Where(t => t.AuditType >= AuditEnum.AuditSuccess);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.CompanyName))
                queryable = queryable.Where(t => t.CompanyName.Contains(pageParam.QueryParam.CompanyName));
            var data = queryable.OrderByDescending(t => t.CreateTime)
                .Select(x => new ResponseEnterpriseIdent()
                {
                    Id = x.Id,
                    IdentNo = x.IdentNo,
                    CompanyName = x.CompanyName,
                    IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(x.IdentStar),
                    LinkPhone = x.LinkPhone,
                    AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(x.AuditType),
                    TableName = x.GetType().Name,
                    IdentStartTime = x.IdentStartTime,
                    IdentEndTime = x.IdentEndTime
                }).AsNoTracking().ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 是否通过终审
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditIndetEnterprisePay(Guid Key, bool Param)
        {
            EnterpriseIdent Ident = Kily.Set<EnterpriseIdent>().Where(t => t.Id == Key).FirstOrDefault();
            if (Param)
                Ident.AuditType = AuditEnum.FinanceSuccess;
            else
                Ident.AuditType = AuditEnum.FinanceFail; ;
            if (UpdateField<EnterpriseIdent>(Ident, "AuditType"))
                return ServiceMessage.HANDLESUCCESS;
            else
                return ServiceMessage.HANDLEFAIL;
        }
        #endregion

        #region 餐饮认证-财务
        /// <summary>
        /// 餐饮认证
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseRepastIdent> IdentFoodPay(PageParamList<RequestRepastIdent> pageParam)
        {
            IQueryable<RepastIdent> queryable = Kily.Set<RepastIdent>().Where(t => t.IsDelete == false);
            queryable = queryable.Where(t => t.AuditType >= AuditEnum.AuditSuccess);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseRepastIdent()
            {
                Id = t.Id,
                IdentNo = t.IdentNo,
                MerchantName = t.MerchantName,
                IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.IdentStar),
                LinkPhone = t.LinkPhone,
                AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(t.AuditType),
                TableName = t.GetType().Name,
                IdentStartTime = t.IdentStartTime,
                IdentEndTime = t.IdentEndTime
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 是否通过终审
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string AuditIndetFoodPay(Guid Key, bool Param)
        {
            RepastIdent Ident = Kily.Set<RepastIdent>().Where(t => t.Id == Key).FirstOrDefault();
            if (Param)
                Ident.AuditType = AuditEnum.FinanceSuccess;
            else
                Ident.AuditType = AuditEnum.FinanceFail; ;
            if (UpdateField<RepastIdent>(Ident, "AuditType"))
                return ServiceMessage.HANDLESUCCESS;
            else
                return ServiceMessage.HANDLEFAIL;
        }
        #endregion

        #region 缴费凭证-财务
        /// <summary>
        /// 查看缴费凭证
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public ResponsePayment WatchCertificate(Guid Id, string Param)
        {
            var data = Kily.Set<SystemPayment>().Where(t => t.IsDelete == false)
                .Where(t => t.TableId == Id).Where(t => t.TableName == Param)
                .Select(t => new ResponsePayment()
                {
                    Id = t.Id,
                    LinkPhone = t.LinkPhone,
                    PayCertificate = t.PayCertificate,
                    PaymentUser = t.PaymentUser,
                    PayTime = t.PayTime,
                    Paymoney = t.Paymoney,
                    Remark = t.Remark
                }).AsNoTracking().FirstOrDefault();
            return data;
        }
        #endregion

        #region 物码缴费-财务
        /// <summary>
        /// 物码缴费
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseEnterpriseApply> GetTagAuditPage(PageParamList<RequestEnterpriseApply> pageParam)
        {
            IQueryable<EnterpriseTagApply> queryable = Kily.Set<EnterpriseTagApply>().Where(t => t.IsDelete == false);
            IQueryable<EnterpriseInfo> queryables = Kily.Set<EnterpriseInfo>();
            if (!string.IsNullOrEmpty(pageParam.QueryParam.AreaTree))
                queryables = queryables.Where(t => t.TypePath.Contains(pageParam.QueryParam.AreaTree));
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
                TagApply.AuditType = Param.AuditType;
                TagApply.IsPay = true;
                List<string> Fields = new List<string>() { "AuditType", "IsPay" };
                if (UpdateField<EnterpriseTagApply>(TagApply, null, Fields))
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