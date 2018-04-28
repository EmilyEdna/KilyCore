using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.DataEntity.ResponseMapper.Dining;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Finance;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork.Model.Dining;
using KilyCore.EntityFrameWork.Model.Enterprise;
using KilyCore.EntityFrameWork.Model.Finance;
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
                Id = x.FirstOrDefault().Id,
                AdminId = t.Id,
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
        public PagedResult<ResponseDiningIdent> IdentFoodPay(PageParamList<RequestDiningIdent> pageParam)
        {
            IQueryable<DiningIdent> queryable = Kily.Set<DiningIdent>().Where(t => t.IsDelete == false);
            queryable = queryable.Where(t => t.AuditType >= AuditEnum.AuditSuccess);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseDiningIdent()
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
            DiningIdent Ident = Kily.Set<DiningIdent>().Where(t => t.Id == Key).FirstOrDefault();
            if (Param)
                Ident.AuditType = AuditEnum.FinanceSuccess;
            else
                Ident.AuditType = AuditEnum.FinanceFail; ;
            if (UpdateField<DiningIdent>(Ident, "AuditType"))
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

        #region 餐饮合同-财务
        /// <summary>
        /// 合同分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseContract> GetContractPage(PageParamList<RequestContract> pageParam)
        {
            IQueryable<DiningContract> queryable = Kily.Set<DiningContract>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            var data = queryable.OrderByDescending(t => t.CreateTime).AsNoTracking()
                .Select(t => new ResponseContract()
                {
                    Id = t.Id,
                    MerchantName = t.MerchantName,
                    PayType = t.PayType,
                    Contract = t.Contract
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        #endregion

        #region 餐饮缴费-财务
        /// <summary>
        /// 餐饮缴费列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseDiningPay> GetDiningPayPage(PageParamList<RequestDiningPay> pageParam)
        {
            IQueryable<DiningPayment> queryable = Kily.Set<DiningPayment>().Where(t => t.IsDelete == false);
            if (pageParam.QueryParam.PayType != 0)
                queryable = queryable.Where(t => t.PayType == pageParam.QueryParam.PayType);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.MerchantName))
                queryable = queryable.Where(t => t.MerchantName.Contains(pageParam.QueryParam.MerchantName));
            var data = queryable.OrderByDescending(t => t.CreateTime).Select(t => new ResponseDiningPay()
            {
                Id = t.Id,
                MerchantName = t.MerchantName,
                PayType = t.PayType,
                MerchantId = t.MerchantId,
                PayTime = t.PayTime,
                EnableYear = t.EnableYear,
                EnableYearEndTime = t.EnableYearEndTime,
                Paymoney = t.Paymoney,
                OrderMoneySum = t.OrderMoneySum,
                PayUser = t.PayUser,
                LinkPhone = t.LinkPhone,
                PayCertificate = t.PayCertificate
            }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns></returns>
        public IList<ResponseAuthorRole> GetDiningRoles()
        {
            var data = Kily.Set<DiningRoleAuthor>().Where(t => t.IsDelete == false).AsNoTracking().Select(t => new ResponseAuthorRole()
            {
                Id = t.Id,
                AuthorName = t.AuthorName,
                AuthorMenuPath = t.AuthorMenuPath,
                AuthorMenuName = String.Join(',', Kily.Set<DiningMenu>()
                    .Where(x => x.IsDelete == false)
                    .Where(x => t.AuthorMenuPath.Contains(x.Id.ToString()))
                    .Select(x => x.MenuName).AsNoTracking().ToArray())
            }).ToList();
            return data;
        }
        /// <summary>
        /// 更新餐饮商家是否开启点餐
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string EditDiningRole(RequestDiningInfo Param)
        {
            DiningInfo info = Kily.Set<DiningInfo>().Where(t => t.IsDelete == false).Where(t => t.Id == Param.Id).FirstOrDefault();
            info.DingRoleId = Param.DingRoleId;
            if (UpdateField<DiningInfo>(info, "DingRoleId"))
                return ServiceMessage.UPDATESUCCESS;
            else
                return ServiceMessage.UPDATEFAIL;
        }
        #endregion

        #region 入住合同-财务
        /// <summary>
        /// 入住合同
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public PagedResult<ResponseStayContract> GetStayContractPage(PageParamList<RequestStayContract> pageParam)
        {
            IQueryable<SystemStayContract> queryable = Kily.Set<SystemStayContract>().Where(t => t.IsDelete == false);
            if (!string.IsNullOrEmpty(pageParam.QueryParam.StayCompanyName))
                queryable = queryable.Where(t => t.StayCompanyName.Contains(pageParam.QueryParam.StayCompanyName));
            var data = queryable.OrderByDescending(t => t.CreateTime).AsNoTracking()
                .Select(t => new ResponseStayContract()
                {
                    Id = t.Id,
                    StayCompanyId = t.StayCompanyId,
                    StayCompanyName = t.StayCompanyName,
                    StayCompanyContract = t.StayCompanyContract,
                    PayContract=t.PayContract
                }).ToPagedResult(pageParam.pageNumber, pageParam.pageSize);
            return data;
        }
        #endregion
    }
}

