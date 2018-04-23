using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 集团业务逻辑接口
    /// </summary>
    public interface IEnterpriseService : IService
    {
        #region 集团菜单
        PagedResult<ResponseEnterpriseMenu> GetEnterpriseMenuPage(PageParamList<RequestEnterpriseMenu> pageParam);
        String RemoveEnterpriseMenu(Guid Id);
        IList<ResponseEnterpriseMenu> AddEnterpriseParentMenu();
        ResponseEnterpriseMenu GetEnterpriseMenuDetail(Guid Id);
        String EditEnterpriseMenu(RequestEnterpriseMenu Param);
        IList<ResponseParentTree> GetEnterpriseTree();
        #endregion

        #region 集团角色
        PagedResult<ResponseEnterpriseRoleAuthor> GetCompanyRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam);
        PagedResult<ResponseEnterpriseRoleAuthor> WatchRolePage(PageParamList<RequestEnterpriseRoleAuthor> pageParam);
        String EditEnterpriseRoleAuthor(RequestEnterpriseRoleAuthor Param);
        PagedResult<ResponseEnterpriseRoleAuthor> GetRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam);
        String RemoveEnterpriseRoleAuthor(Guid Id);
        IList<ResponseEnterpriseRoleAuthor> GetRoleAuthorList();
        String DistributionRole(RequestEnterpriseRoleAuthor Param);
        #endregion

        #region 资料审核
        PagedResult<ResponseEnterprise> GetCompanyPage(PageParamList<RequestEnterprise> pageParam);
        ResponseEnterprise GetCompanyDetail(Guid Id);
        String AuditCompany(RequestAudit Param);
        #endregion

        #region 认证审核
        PagedResult<ResponseEnterpriseIdent> GetCompanyIdentPage(PageParamList<RequestEnterpriseIdent> pageParam);
        ResponseEnterpriseIdent GetCompanyIdentDetail(RequestEnterpriseIdent Param);
        String AuditIdent(RequestAudit Param);
        String AuditPayment(RequestPayment param);
        #endregion

        #region 登陆注册
        String RegistCompanyAccount(RequestEnterpriseInfo Param);
        ResponseEnterpriseInfo EnterpriseLogin(RequestValidate LoginValidate);
        #endregion
    }
}
