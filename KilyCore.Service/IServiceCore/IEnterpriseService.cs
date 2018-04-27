using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
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

        #region 登录注册
        String RegistCompanyAccount(RequestEnterprise Param);
        ResponseEnterprise EnterpriseLogin(RequestValidate LoginValidate);
        #endregion

        #region 集团企业后台系统
        #region 获取全局集团菜单
        IList<ResponseEnterpriseMenu> GetEnterpriseMenu();
        #endregion
        #region 企业信息
        ResponseEnterprise GetEnterpriseInfo(Guid Id);
        String EditEnterprise(RequestEnterprise param);
        #endregion
        #region 保存合同和缴费凭证
        String SaveContract(RequestStayContract Param);
        #endregion
        #endregion
    }
}
