﻿using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Finance;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 企业前台后也基础管理业务逻辑接口
    /// </summary>
    public interface IEnterpriseWebService : IService
    {
        #region 获取全局集团菜单
        IList<ResponseEnterpriseMenu> GetEnterpriseMenu();
        IList<ResponseParentTree> GetEnterpriseWebTree();
        #endregion
        #region 基础管理
        #region 企业信息
        ResponseEnterprise GetEnterpriseInfo(Guid Id);
        String EditEnterprise(RequestEnterprise param);
        #endregion
        #region 保存合同和缴费凭证
        String SaveContract(RequestStayContract Param);
        #endregion
        #region 人员管理
        PagedResult<ResponseEnterpriseUser> GetUserPage(PageParamList<RequestEnterpriseUser> pageParam);
        String EditUser(RequestEnterpriseUser Param);
        String RemoveUser(Guid Id);
        ResponseEnterpriseUser GetUserDetail(Guid Id);
        IList<ResponseRoleAuthorWeb> GetRoleAuthorList();
        #endregion
        #region 集团账户
        String EditRoleAuthor(RequestRoleAuthorWeb Param);
        PagedResult<ResponseRoleAuthorWeb> GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam);
        String RemoveRole(Guid Id);
        #endregion
        #region 企业认证
        String EditEnterpriseIdent(RequestEnterpriseIdent param);
        #endregion
        #endregion
        #region 成长档案
        #region 施养管理
        PagedResult<ResponseEnterprisePlanting> GetPlantingPage(PageParamList<RequestEnterprisePlanting> pageParam);
        String EditPlanting(RequestEnterprisePlanting Param);
        String RemovePlanting(Guid Id);
        #endregion
        #endregion
    }
}
