﻿using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Dining;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Dining;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Finance;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 财务业务逻辑接口
    /// </summary>
    public interface IFinanceService : IService
    {
        #region 加盟缴费
        PagedResult<ResponseAdminAttach> GetJoinPayPage(PageParamList<RequestAdminAttach> pageParam);
        String StartUse(Guid Id);
        String BlockUp(Guid Id);
        String Archive(RequestAdminAttach param);
        String SendEmail(RequestEMail Param);
        #endregion
        #region 企业认证
        PagedResult<ResponseEnterpriseIdent> IdentEnterprisePay(PageParamList<RequestEnterpriseIdent> pageParam);
        String AuditIndetEnterprisePay(Guid Key, bool Param);
        #endregion
        #region 餐饮认证
        PagedResult<ResponseDiningIdent> IdentFoodPay(PageParamList<RequestDiningIdent> pageParam);
        String AuditIndetFoodPay(Guid Key, bool Param);
        #endregion
        #region 缴费凭证
        ResponsePayment WatchCertificate(Guid Id, string Param);
        #endregion
        #region 餐饮合同
        PagedResult<ResponseContract> GetContractPage(PageParamList<RequestContract> pageParam);
        #endregion
        #region 餐饮缴费
        PagedResult<ResponseDiningPay> GetDiningPayPage(PageParamList<RequestDiningPay> pageParam);
        IList<ResponseAuthorRole> GetDiningRoles();
        String EditDiningRole(RequestDiningInfo Param);
        #endregion
        #region 物码缴费
        PagedResult<ResponseEnterpriseApply> GetTagAuditPage(PageParamList<RequestEnterpriseApply> pageParam);
        String AuditCode(RequestAudit Param);
        #endregion
    }
}
