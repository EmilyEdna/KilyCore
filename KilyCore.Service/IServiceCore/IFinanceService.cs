using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Finance;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

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
        #region 认证缴费
        PagedResult<ResponseEnterpriseIdent> GetIdentPayPage(PageParamList<RequestEnterpriseIdent> pageParam);
        String AuditIndetPay(Guid Key, bool Param);
        #endregion
    }
}
