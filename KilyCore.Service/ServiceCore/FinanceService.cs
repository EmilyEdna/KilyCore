﻿using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.DataEntity.ResponseMapper.Dining;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Finance;
using KilyCore.EntityFrameWork.Model.Company;
using KilyCore.EntityFrameWork.Model.Dining;
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
            IQueryable<CompanyIdent> queryable = Kily.Set<CompanyIdent>().Where(t => t.IsDelete == false);
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
                    Representative = x.Representative,
                    LinkPhone = x.LinkPhone,
                    AuditTypeName = AttrExtension.GetSingleDescription<AuditEnum, DescriptionAttribute>(x.AuditType),
                    CommunityCode = x.CommunityCode,
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
            CompanyIdent Ident = Kily.Set<CompanyIdent>().Where(t => t.Id == Key).FirstOrDefault();
            if (Param)
                Ident.AuditType = AuditEnum.FinanceSuccess;
            else
                Ident.AuditType = AuditEnum.FinanceFail; ;
            if (UpdateField<CompanyIdent>(Ident, "AuditType"))
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
                IdentNo = t.IdentNo,
                MerchantName = t.MerchantName,
                IdentStarName = AttrExtension.GetSingleDescription<IdentEnum, DescriptionAttribute>(t.IdentStar),
                Representative = t.Representative,
                CommunityCode = t.CommunityCode,
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
    }
}