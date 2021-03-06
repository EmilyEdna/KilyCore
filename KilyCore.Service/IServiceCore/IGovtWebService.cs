﻿using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;

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

#endregion << 版 本 注 释 >>

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 政府监管前台后台业务逻辑接口
    /// </summary>
    public interface IGovtWebService : IService
    {
        #region 获取全局集团菜单

        IList<ResponseGovtMenu> GetGovtMenu();

        #endregion 获取全局集团菜单

        #region 获取所有商家和企业

        Object GetAllMerchant(String Key);

        #endregion 获取所有商家和企业

        #region 登录

        ResponseGovtInfo GovtLogin(RequestGovtInfo Param);

        String EditPwd(RequestGovtInfo Param);

        #endregion 登录

        #region 企业监管

        PagedResult<ResponseEnterprise> GetCompanyPage(PageParamList<RequestEnterprise> pageParam);

        PagedResult<ResponseMerchant> GetMerchantPage(PageParamList<RequestMerchant> pageParam);

        ResponseEnterprise GetCompanyDetail(Guid Id);

        ResponseMerchant GetRepastDetail(Guid Id);

        object GetAllComWithKeyWord(string KeyWord);

        object GetAllMerWithKeyWord(string KeyWord);

        object GetAllCom(string Area, int ComType);

        object GetAllMer(string Area, int ComType);

        object GetAllVideo(Guid Id, int Type);

        #endregion 企业监管

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

        List<ResponseGovtInfo> GetAllGovt();

        #endregion 部门信息

        #region 管辖区域

        String GetCityName(Guid Id);

        IList<ResponseGovtDistribut> GetDistributArea();

        IList<ResponseArea> GetArea(Guid Id);

        IList<ResponseTown> GetTown(Guid Id);

        #endregion 管辖区域

        #region 产品监管

        PagedResult<ResponseEnterpriseGoods> GetWorkPage(PageParamList<RequestEnterpriseGoods> pageParam);

        Object GetWorkDetail(Guid Id);

        PagedResult<ResponseEnterpriseGoods> GetEdiblePage(PageParamList<RequestEnterpriseGoods> pageParam);

        Object GetEdibleDetail(Guid Id);

        object GetGoodsPage(Guid CompanyId);

        #endregion 产品监管

        #region 台账管理

        Object GetTickPrint(Dictionary<String, String> pairs);

        #endregion 台账管理

        #region 餐饮监管

        PagedResult<ResponseCookBanquet> GetBanquetPage(PageParamList<RequestCookBanquet> pageParam);

        ResponseCookBanquet GetBanquetDetail(Guid Id);

        String SiteImg(Guid Id, String Param);

        String EditCookBanquet(Guid Id, String Param);

        #endregion 餐饮监管

        #region 风险预警

        PagedResult<ResponseGovtRisk> GetWaringRiskPage(PageParamList<RequestGovtRisk> pageParam);

        String EditWaringRisk(RequestGovtRisk Param);

        String ReportWaringRisk(Guid Id);

        String RemoveWaringRisk(Guid Id);

        List<int> GetRiskCount();

        String GetCity(Guid Id);

        Object GetCardPage(PageParamList<RequestGovtRiskCompany> pageParam);
        Object GetWarnList(PageParamList<RequestGovtRiskCompany> pageParam);

        String ReportCardWaring(Guid Id, String Key);

        ResponseGovtRisk GetWaringDetail(Guid Id);

        #endregion 风险预警

        #region 执法检查

        #region 网上执法

        PagedResult<ResponseGovtNetPatrol> GetNetPatrolPage(PageParamList<RequestGovtNetPatrol> pageParam);

        String EditPatrol(RequestGovtNetPatrol Param);

        String RemovePatrol(Guid Id);

        ResponseGovtNetPatrol GetNetPatrolDetail(Guid Id);

        String EditNetPatrol(RequestGovtMsg Param);

        List<ResponseSystemMessage> GetMsgList();

        List<ResponseGovtNetPatrolLog> GetNetPatrolLogs(Guid Id);

        #endregion 网上执法

        #region 执法类目

        PagedResult<ResponseGovtPatrolCategory> GetCategoryPage(PageParamList<RequestGovtPatrolCategory> pageParam);

        String EditCategory(RequestGovtPatrolCategory Param);

        ResponseGovtPatrolCategory GetCategoryDetail(Guid Id);

        String RemoveCategory(Guid Id);

        PagedResult<ResponseGovtPatrolCategoryAttach> GetCategoryAttachPage(PageParamList<RequestGovtPatrolCategoryAttach> pageParam);

        String EditCategoryAttach(RequestGovtPatrolCategoryAttach Param);

        ResponseGovtPatrolCategoryAttach GetCategoryAttachDetail(Guid Id);

        String RemoveCategoryAttach(Guid Id);

        #endregion 执法类目

        #region 移动执法

        PagedResult<ResponseGovtMovePatrol> GetMovePatralPage(PageParamList<RequestGovtMovePatrol> pageParam);

        String RemoveMovePatral(Guid Id);

        String EditMovePatrol(RequestGovtMovePatrol Param);

        ResponseGovtMovePatrol GetGovtMovePatrolDetail(Guid Id);

        #endregion 移动执法

        #region 企业自查模板

        PagedResult<ResponseGovtTemplateChild> GetTemplateChild(PageParamList<RequestGovtTemplateChild> pageParam);

        ResponseGovtTemplateChild GetTemplateChildDetail(Guid Id);

        IList<ResponseGovtTemplate> GetTemplateContentList(String CompanyType, String TypePath);

        PagedResult<ResponseGovtTemplate> GetTemplatePage(PageParamList<RequestGovtTemplate> pageParam);

        String EditGovtTemplate(RequestGovtTemplate Param);

        String RemoveTemplate(Guid Id);

        ResponseGovtTemplate GetTemplateDetail(Guid Id);

        #endregion 企业自查模板

        #endregion 执法检查

        #region 应急培训

        #region 培训通知

        PagedResult<ResponseGovtTrainNotice> GetTrainNoticePage(PageParamList<RequestGovtTrainNotice> pageParam);

        String EditNotice(RequestGovtTrainNotice Param);

        String RemoveNotice(Guid Id);

        ResponseGovtTrainNotice GetTrainNoticeDetail(Guid Id);

        String ReportTrainNotice(Guid Id);

        #endregion 培训通知

        #region 培训报道

        PagedResult<ResponseGovtTrainReport> GetTrainReportPage(PageParamList<RequestGovtTrainReport> pageParam);

        String EditTrainReport(RequestGovtTrainReport Param);

        String RemoveReport(Guid Id);

        ResponseGovtTrainReport GetTrainReportDetail(Guid Id);

        String ReportTrainReport(Guid Id);

        #endregion 培训报道

        #endregion 应急培训

        #region 投诉信息

        PagedResult<ResponseGovtComplain> GetComplainPage(PageParamList<RequestGovtComplain> pageParam);

        String RemoveComplain(Guid Id);

        String EditComplain(RequestGovtComplain Param);

        String ReportComplain(Guid Id);

        String ReportComplainInfo(Guid Id, String Param);

        ResponseGovtComplain GetComplainDetail(Guid Id);

        #endregion 投诉信息

        #region 数据统计

        #region 新旧大屏

        IList<DataPie> GetProductRank();

        IList<DataPie> GetPersonBank();

        ResponseGovtMap GetAllCityMerchantCount();

        #endregion 新旧大屏

        #region 新大屏

        Object GetNewStayInTodayCount();

        IList<DataPie> GetNewStayInAllCompanyCount();

        String GetTodayNow();

        IList<DataBar> GetNewWeekRiskAndComplainCount();

        IList<DataLine> GetNewNetCheckCount();

        Object GetNewVedioToday(Guid? Id);

        #endregion 新大屏

        #region 旧大屏

        Object GetLawRank();

        Object GetCountNum();

        IList<ResponseGovtRanking> GetAreaRank();

        #endregion 旧大屏

        #region 首页数据

        Object GetComplainDataRatio();

        Object GetComDataRatio();

        Object GetComplainLine();

        #endregion 首页数据

        #endregion 数据统计

        #region 责任协议

        PagedResult<ResponseGovtAgree> GetAgreePage(PageParamList<RequestGovtAgree> pageParam);

        String EditAgree(RequestGovtAgree Param);

        ResponseGovtAgree GetAgreeDetail(Guid Id);

        String RemoveAgree(Guid Id);

        #endregion 责任协议

        #region 操作日志

        List<ResponseSystemLogInfo> GetLogInfos();

        PagedResult<ResponseSystemLogInfo> GetHandlerLogPage(PageParamList<RequestSystemLogInfo> pageParam);

        string EditHandlerLog(List<Guid> Keys, bool flag);

        #endregion 操作日志

        #region 手机APP

        Object GetAppTodayCount();

        #endregion 手机APP

        #region  综合
        object GetAreaShow(string name, string type);
        object GetAreaBill(string type, string name);
        object GetAllPro(string ProName);
        #endregion
    }
}