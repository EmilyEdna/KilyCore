using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
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
        ResponseEnterprise GetCompanyDetail(Guid Id);
        ResponseMerchant GetRepastDetail(Guid Id);
        #endregion

        #region 部门信息
        PagedResult<ResponseGovtInstitution> GetInsPage(PageParamList<RequestGovtInstitution> pageParam);
        IList<ResponseGovtInstitution> GetInsList();
        String RemoveIns(Guid Id);
        ResponseGovtInstitution GetInsDetail(Guid Id);
        String EditIns(RequestGovtInstitution Param);
        PagedResult<ResponseGovtInfo> GetGovtInfoPage(PageParamList<RequestGovtInfo> pageParam);
        String RemoveGovtInfo(Guid Id);
        ResponseGovtInfo GetGovtInfoDetail(Guid Id);
        String EditUser(RequestGovtInfo Param);
        #endregion

        #region 管辖区域
        IList<ResponseGovtDistribut> GetDistributArea();
        IList<ResponseArea> GetArea(Guid Id);
        IList<ResponseTown> GetTown(Guid Id);
        #endregion

        #region 产品监管
        PagedResult<ResponseEnterpriseGoods> GetWorkPage(PageParamList<RequestEnterpriseGoods> pageParam);
        Object GetWorkDetail(Guid Id);
        PagedResult<ResponseEnterpriseGoods> GetEdiblePage(PageParamList<RequestEnterpriseGoods> pageParam);
        Object GetEdibleDetail(Guid Id);
        #endregion

        #region 餐饮监管
        PagedResult<ResponseCookBanquet> GetBanquetPage(PageParamList<RequestCookBanquet> pageParam);
        ResponseCookBanquet GetBanquetDetail(Guid Id);
        String EditCookBanquet(Guid Id);
        #endregion

        #region 风险预警
        PagedResult<ResponseGovtRisk> GetWaringRiskPage(PageParamList<RequestGovtRisk> pageParam);
        String EditWaringRisk(RequestGovtRisk Param);
        String ReportWaringRisk(Guid Id);
        String RemoveWaringRisk(Guid Id);
        int GetRiskCount();
        String GetCity(Guid Id);
        Object GetCardPage(PageParamList<RequestGovtRiskCompany> pageParam);
        String ReportCardWaring(Guid Id);
        #endregion
    }
}
