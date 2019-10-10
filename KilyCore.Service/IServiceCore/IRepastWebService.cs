using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：IRepastWebService
* 类 描 述 ：
* 命名空间 ：KilyCore.Service.IServiceCore
* 机器名称 ：DESKTOP-QPIVQ28
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/16 15:14:21
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 餐饮前台后台业务逻辑接口
    /// </summary>
    public interface IRepastWebService : IService
    {
        #region 获取全局集团菜单

        IList<ResponseRepastDictionary> GetDictionaryList(String Param);

        IList<ResponseRepastMenu> GetRepastMenu();

        IList<ResponseParentTree> GetRepastWebTree(String Key);

        #endregion 获取全局集团菜单

        #region 基础管理

        #region 商家资料

        PagedResult<ResponseMerchant> GetMerchantInfoPage(PageParamList<RequestMerchant> pageParam);

        ResponseMerchant GetMerchantDetail(Guid Id);

        IDictionary<Guid, String> GetChildAccount(Guid Id);

        String SaveMerchant(RequestMerchant Param);

        String SaveMerchantAccount(RequestMerchant Param);

        String SaveMerchantArea(RequestMerchant Param);

        ResponseStayContract SaveContract(RequestStayContract Param);

        PagedResult<ResponseStayContract> GetContractAudit(PageParamList<RequestStayContract> pageParam);

        #endregion 商家资料

        #region 商家认证

        String SaveMerchantIdent(RequestRepastIdent Param);

        PagedResult<ResponseRepastIdent> GetIndentPage(PageParamList<RequestRepastIdent> pageParam);

        #endregion 商家认证

        #region 权限角色

        String SaveRoleAuthor(RequestRoleAuthorWeb Param);

        PagedResult<ResponseRoleAuthorWeb> GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam);

        String DeleteRole(Guid Id);

        IList<ResponseRoleAuthorWeb> GetRoleAuthorList();

        #endregion 权限角色

        #region 人员管理

        PagedResult<ResponseMerchantUser> GetMerchantUserPage(PageParamList<RequestMerchantUser> pageParam);

        String SaveUser(RequestMerchantUser Param);

        ResponseMerchantUser GetUserDetail(Guid Id);

        String DeleteUser(Guid Id);

        IList<ResponseMerchantUser> GetMerchantList();

        #endregion 人员管理

        #region 集团账户

        PagedResult<ResponseMerchant> GetChildInfoPage(PageParamList<RequestMerchant> pageParam);

        String SaveChildInfo(RequestMerchant Param);

        ResponseMerchant GetChildInfo(Guid Id);

        #endregion 集团账户

        #region 餐饮字典

        PagedResult<ResponseRepastDictionary> GetDicPage(PageParamList<RequestRepastDictionary> pageParam);

        String DeleteDic(Guid Id);

        ResponseRepastDictionary GetDicDetail(Guid Id);

        String SaveDic(RequestRepastDictionary Param);

        #endregion 餐饮字典

        #region 升级续费

        PagedResult<ResponseRepastLevelUp> GetLvPage(PageParamList<RequestRepastLevelUp> pageParam);

        String SaveContinued(RequestRepastContinued Param);

        String SaveUpLevel(RequestRepastUpLevel Param);

        PagedResult<ResponseRepastContinued> GetContinuedPage(PageParamList<RequestRepastContinued> pageParam);

        PagedResult<ResponseRepastUpLevel> GetUpLevelPage(PageParamList<RequestRepastUpLevel> pageParam);

        String AuditContinuedAndLevel(Guid Id, bool Param);

        #endregion 升级续费

        #region 商家自查

        PagedResult<ResponseGovtTemplateChild> GetTemplateChild(PageParamList<RequestGovtTemplateChild> pageParam);

        String EditTemplateChild(RequestGovtTemplateChild Param);

        String DeleteTemplate(Guid Id);

        ResponseGovtTemplateChild GetTemplateChildDetail(Guid Id);

        #endregion 商家自查

        #region 委员

        PagedResult<ResponseRepastOrg> GetOrgPage(PageParamList<RequestRepastOrg> pageParam);

        ResponseRepastOrg GetOrgDetail(Guid Id);

        string EditOrg(RequestRepastOrg Param);

        string RemoveOrg(Guid Id);

        string GetOrgInfo(String Phone);

        #endregion 委员

        #endregion 基础管理

        #region 功能管理

        #region 供应商

        PagedResult<ResponseRepastSupplier> GetSupplierPage(PageParamList<RequestRepastSupplier> pageParam);

        String DeleteSupplier(Guid Id);

        String SaveSupplier(RequestRepastSupplier Param);

        IList<ResponseRepastSupplier> GetSupplierList();

        #endregion 供应商

        #region 进货台账

        PagedResult<ResponseRepastBuybill> GetBuybillPage(PageParamList<RequestRepastBuybill> pageParam);

        String RemoveBuybill(Guid Id);

        ResponseRepastBuybill GetBuybillDetail(Guid Id);

        String EditBuybill(RequestRepastBuybill Param);

        #endregion 进货台账

        #region 台账凭证

        PagedResult<ResponseBillTicket> GetMerchantTicketPage(PageParamList<RequestBillTicket> pageParam);

        ResponseBillTicket GetMerchantTicketDetail(Guid Id);

        String EditTheme(RequestBillTicket Param);

        String DeleteTicket(Guid Id);

        #endregion 台账凭证

        #region 销售台账

        PagedResult<ResponseRepastSellbill> GetSellbillPage(PageParamList<RequestRepastSellbill> pageParam);

        String RemoveSellbill(Guid Id);

        ResponseRepastSellbill GetSellbillDetail(Guid Id);

        String EditSellbill(RequestRepastSellbill Param);

        #endregion 销售台账

        #region 进销台账

        PagedResult<Object> GetSellBuyPage(PageParamList<RequestRepastSellbill> pageParam);

        #endregion 进销台账

        #region 实时监控

        PagedResult<ResponseRepastVideo> GetVideoPage(PageParamList<RequestRepastVideo> pageParam);

        String SaveVideo(RequestRepastVideo Param);

        String DeleteVideo(Guid Id);

        String ShowVideo(Guid Id, bool flag);

        #endregion 实时监控

        #endregion 功能管理

        #region 菜品管理

        PagedResult<ResponseRepastDish> GetDishPage(PageParamList<RequestRepastDish> pageParam);

        ResponseRepastDish GetDishDetail(Guid Id);

        String DeleteDish(Guid Id);

        String SaveDish(RequestRepastDish Param);

        PagedResult<ResponseFoodMenu> GetMerchantWeekPage(PageParamList<RequestFoodMenu> pageParam);

        String EditFoodMenu(RequestFoodMenu Param);

        String DeleteWeekMenu(Guid Id);

        #endregion 菜品管理

        #region 溯源追踪

        #region 原料溯源

        PagedResult<ResponseRepastStuff> GetStuffPage(PageParamList<RequestRepastStuff> pageParam);

        String RemoveStuff(Guid Id);

        String EditStuff(RequestRepastStuff Param);

        ResponseRepastStuff GetStuffDetail(Guid Id);

        #endregion 原料溯源

        #region 留样管理

        PagedResult<ResponseRepastSample> GetSamplePage(PageParamList<RequestRepastSample> pageParam);

        String EditSample(RequestRepastSample Param);

        String RemoveSample(Guid Id);

        #endregion 留样管理

        #region 抽样管理

        PagedResult<ResponseRepastDraw> GetDrawPage(PageParamList<RequestRepastDraw> pageParam);

        String EditDraw(RequestRepastDraw Param);

        String RemoveDraw(Guid Id);

        #endregion 抽样管理

        #region 废物处理

        PagedResult<ResponseRepastDuck> GetDuckPage(PageParamList<RequestRepastDuck> pageParam);

        String EditDuck(RequestRepastDuck Param);

        String RemoveDuck(Guid Id);

        #endregion 废物处理

        #region 消毒管理

        PagedResult<ResponseRepastDisinfect> GetDisinfectPage(PageParamList<RequestRepastDisinfect> pageParam);

        String EditDisinfect(RequestRepastDisinfect Param);

        String RemoveDisinfect(Guid Id);

        ResponseRepastDisinfect GetDisinfectDetail(Guid Id);

        #endregion 消毒管理

        #region 添加剂管理

        PagedResult<ResponseRepastAdditive> GetAdditivePage(PageParamList<RequestRepastAdditive> pageParam);

        String SaveAdditive(RequestRepastAdditive Param);

        String RemoveAdditive(Guid Id);

        ResponseRepastAdditive GetAdditiveDetail(Guid Id);

        #endregion 添加剂管理

        #endregion 溯源追踪

        #region 仓库管理

        #region 原料仓库-入库

        PagedResult<ResponseRepastInStorage> GetInStoragePage(PageParamList<RequestRepastInStorage> pageParam);

        String EditInStorage(RequestRepastInStorage Param);

        String RemoveInStorage(Guid Id);

        ResponseRepastInStorage GetInStorageDetail(Guid Id);

        IList<ResponseRepastInStorage> GetInStorageList(string Param);

        #endregion 原料仓库-入库

        #region 原料仓库-出库

        PagedResult<ResponseRepastOutStorage> GetOutStoragePage(PageParamList<RequestRepastOutStorage> pageParam);

        String EditOutStorage(RequestRepastOutStorage Param);

        String RemoveOutStorage(Guid Id);

        #endregion 原料仓库-出库

        #region 物品仓库-入库

        PagedResult<ResponseRepastArticleInStock> GetInStockPage(PageParamList<RequestRepastArticleInStock> pageParam);

        String RemoveInStock(Guid Id);

        String EditInStock(RequestRepastArticleInStock Param);

        ResponseRepastArticleInStock GetInStockDetail(Guid Id);

        #endregion 物品仓库-入库

        #region 物品仓库-出库

        PagedResult<ResponseRepastArticleOutStock> GetOutStockPage(PageParamList<RequestRepastArticleOutStock> pageParam);

        String RemoveOutStock(Guid Id);

        String EditOutStock(RequestRepastArticleOutStock Param);

        #endregion 物品仓库-出库

        #region 名称管理

        PagedResult<ResponseRepastTypeName> GetNamesPage(PageParamList<RequestRepastTypeName> pageParam);

        String EditNames(RequestRepastTypeName Param);

        String RemoveNames(Guid Id);

        IList<ResponseRepastTypeName> GetNamesList(int Key);

        #endregion 名称管理

        #region 库存报表

        PagedResult<ResponseRepastStockReport> GetStorageReportPage(PageParamList<RequestRepastStockReport> pageParam);

        PagedResult<ResponseRepastStockReport> GetStockReportPage(PageParamList<RequestRepastStockReport> pageParam);

        #endregion 库存报表

        #endregion 仓库管理

        #region 微信和支付宝调用

        String AliPay(int Key, int? Value);

        String WxPay(int Key, int? Value);

        String WxQueryPay(RequestContractTemp Param);

        #endregion 微信和支付宝调用

        #region 导出Excel

        IList<Object> ExportStuffInStockFile(String Param);

        IList<Object> ExportStuffOutStockFile(String Param);

        IList<Object> ExportGoodsInStockFile(String Param);

        IList<Object> ExportGoodsOutStockFile(String Param);

        #endregion 导出Excel

        #region 数据统计

        Object GetDataCount(Guid? Id);

        Object GetLineData();

        #endregion 数据统计

        #region 扫码信息

        PagedResult<ResponseRepastScanInfo> GetScanInfoPage(PageParamList<RequestRepastScanInfo> pageParam);

        String SaveScanInfo(RequestRepastScanInfo Param);

        String RemoveScan(Guid Id, bool? Param);

        ResponseRepastScanInfo GetScanInfoDetail(Guid Id);

        #endregion 扫码信息

        #region 列表信息

        Object GetDishList();

        Object GetStuffList();

        Object GetVideoList();

        Object GetUserList();

        Object GetDuckList();

        Object GetDrawList();

        Object GetSampleList();

        Object GetDisinfectList();

        Object GetAdditiveList();

        Object GetTicketList();

        Object GetWeekMenuList();

        #endregion 列表信息

        #region 手机端信息

        ResponseRepastScanInfos GetMobileScanInfo(Guid Id);

        #endregion 手机端信息

        #region 陪餐管理

        PagedResult<ReponseRepastUnitIns> GetUnitInsPage(PageParamList<RequestRepastUnitIns> pageParam);

        String DeleteUnitIns(Guid Id);

        String SaveUnitIns(RequestRepastUnitIns Param);

        ReponseRepastUnitIns GetUnitInsDetail(Guid Id);

        PagedResult<ReponseRepastUnitInsRecord> GetUnitInsRecordPage(PageParamList<RequestRepastUnitInsRecord> pageParam);

        String DeleteUnitInsRecord(Guid Id);

        String SaveUnitInsRecord(RequestRepastUnitInsRecord Param);

        ReponseRepastUnitInsRecord GetUnitInsRecordDetail(Guid Id);

        #endregion 陪餐管理

        #region APP接口

        PagedResult<ResponseGovtRisk> GetWaringRiskPage(PageParamList<RequestGovtRisk> pageParam);

        PagedResult<ResponseGovtComplain> GetComplainPage(PageParamList<RequestGovtComplain> pageParam);

        PagedResult<ResponseSystemMessage> GetNetPatrolPage(PageParamList<Object> pageParam);

        #endregion APP接口
    }
}