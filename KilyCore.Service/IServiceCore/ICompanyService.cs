using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Company;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Company;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 企业业务逻辑接口
    /// </summary>
    public interface ICompanyService :IService
    {
        #region 资料审核
        PagedResult<ResponseCompany> GetCompanyPage(PageParamList<RequestCompany> pageParam);
        ResponseCompany GetCompanyDetail(Guid Id);
        String AuditCompany(RequestAudit Param);
        #endregion
        #region 认证审核
        PagedResult<ResponseCompanyIdent> GetCompanyIdentPage(PageParamList<RequestCompanyIdent> pageParam);
        ResponseCompanyIdent GetCompanyIdentDetail(RequestCompanyIdent Param);
        String AuditIdent(RequestAudit Param);
        #endregion
    }
}
