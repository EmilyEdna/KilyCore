﻿using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
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

        IList<ResponseParentTree> GetEnterpriseTree(String key);

        #endregion 集团菜单

        #region 集团角色

        PagedResult<ResponseEnterpriseRoleAuthor> GetCompanyRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam);

        PagedResult<ResponseEnterpriseRoleAuthor> WatchRolePage(PageParamList<RequestEnterpriseRoleAuthor> pageParam);

        String EditEnterpriseRoleAuthor(RequestEnterpriseRoleAuthor Param);

        PagedResult<ResponseEnterpriseRoleAuthor> GetRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam);

        String RemoveEnterpriseRoleAuthor(Guid Id);

        IList<ResponseEnterpriseRoleAuthor> GetRoleAuthorList();

        String DistributionRole(RequestEnterpriseRoleAuthor Param);

        ResponseEnterpriseRoleAuthor GetEnterpriseRoleAuthorDetail(Guid Id);

        #endregion 集团角色

        #region 资料审核

        PagedResult<ResponseEnterprise> GetCompanyPage(PageParamList<RequestEnterprise> pageParam);

        ResponseEnterprise GetCompanyDetail(Guid Id);

        String AuditCompany(RequestAudit Param);

        #endregion 资料审核

        #region 认证审核

        PagedResult<ResponseEnterpriseIdent> GetCompanyIdentPage(PageParamList<RequestEnterpriseIdent> pageParam);

        ResponseEnterpriseIdent GetCompanyIdentDetail(RequestEnterpriseIdent Param);

        String AuditIdent(RequestAudit Param);

        String AuditPayment(RequestPayment param);

        #endregion 认证审核

        #region 登录注册

        String RegistCompanyAccount(RequestEnterprise Param);

        Object EnterpriseLogin(RequestValidate LoginValidate);

        #endregion 登录注册

        #region 标签管理

        PagedResult<ResponseEnterpriseApply> GetTagAuditPage(PageParamList<RequestEnterpriseApply> pageParam);

        String AuditCode(RequestAudit Param);

        IList<ResponseAudit> GetTagAuditDetail(RequestAudit Param);

        #endregion 标签管理

        #region 产品审核

        PagedResult<ResponseEnterpriseGoodsStock> GetWaitAuditGoodPage(PageParamList<RequestEnterpriseGoodsStock> pageParam);

        String AuditGoodSuccess(Guid Id);

        Object GetAuditProductDetail(Guid Id);

        #endregion 产品审核
    }
}