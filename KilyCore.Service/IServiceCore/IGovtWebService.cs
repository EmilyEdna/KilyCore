using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：IGovtWebService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/7 11:21:04
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 政府监管前台后台业务逻辑接口
    /// </summary>
    public interface IGovtWebService : IService
    {
        #region 获取全局集团菜单
        IList<ResponseGovtMenu> GetGovtMenu();
        #endregion

        #region 登录
        ResponseGovtInfo GovtLogin(RequestGovtInfo Param);
        String EditPwd(RequestGovtInfo Param);
        #endregion

        #region 企业监管
        PagedResult<ResponseEnterprise> GetCompanyPage(PageParamList<RequestEnterprise> pageParam);
        PagedResult<ResponseMerchant> GetMerchantPage(PageParamList<RequestMerchant> pageParam);
        #endregion

        #region 部门信息
        PagedResult<ResponseGovtInstitution> GetInsPage(PageParamList<RequestGovtInstitution> pageParam);
        String RemoveIns(Guid Id);
        String EditIns(RequestGovtInstitution Param);
        PagedResult<ResponseGovtInfo> GetGovtInfoPage(PageParamList<RequestGovtInfo> pageParam);
        String RemoveGovtInfo(Guid Id);
        ResponseGovtInfo GetGovtInfoDetail(Guid Id);
        String EditUser(RequestGovtInfo Param);
        #endregion
    }
}
