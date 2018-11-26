using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Repast;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Repast;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;

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
#endregion
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
        IList<ResponseParentTree> GetRepastWebTree();
        #endregion
        #region 基础管理
        #region 商家资料
        PagedResult<ResponseMerchant> GetMerchantInfoPage(PageParamList<RequestMerchant> pageParam);
        ResponseMerchant GetMerchantDetail(Guid Id);
        IDictionary<Guid, String> GetChildAccount(Guid Id);
        String SaveMerchant(RequestMerchant Param);
        String SaveMerchantAccount(RequestMerchant Param);
        String SaveMerchantArea(RequestMerchant Param);
        String SaveContract(RequestStayContract Param);
        PagedResult<ResponseAudit> GetContractAudit(PageParamList<RequestAudit> pageParam);
        #endregion
        #region 商家认证
        String SaveMerchantIdent(RequestRepastIdent Param);
        PagedResult<ResponseRepastIdent> GetIndentPage(PageParamList<RequestRepastIdent> pageParam);
        #endregion
        #region 权限角色
        String SaveRoleAuthor(RequestRoleAuthorWeb Param);
        PagedResult<ResponseRoleAuthorWeb> GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam);
        String DeleteRole(Guid Id);
        IList<ResponseRoleAuthorWeb> GetRoleAuthorList();
        #endregion
        #region 人员管理
        PagedResult<ResponseMerchantUser> GetMerchantUserPage(PageParamList<RequestMerchantUser> pageParam);
        String SaveUser(RequestMerchantUser Param);
        ResponseMerchantUser GetUserDetail(Guid Id);
        String DeleteUser(Guid Id);
        IList<ResponseMerchantUser> GetMerchantList();
        #endregion
        #region 集团账户
        PagedResult<ResponseMerchant> GetChildInfoPage(PageParamList<RequestMerchant> pageParam);
        String SaveChildInfo(RequestMerchant Param);
        #endregion
        #region 餐饮字典
        PagedResult<ResponseRepastDictionary> GetDicPage(PageParamList<RequestRepastDictionary> pageParam);
        String DeleteDic(Guid Id);
        ResponseRepastDictionary GetDicDetail(Guid Id);
        String SaveDic(RequestRepastDictionary Param);
        #endregion
        #region 升级续费
        PagedResult<ResponseRepastLevelUp> GetLvPage(PageParamList<RequestRepastLevelUp> pageParam);
        String SaveContinued(RequestRepastContinued Param);
        String SaveUpLevel(RequestRepastUpLevel Param);
        PagedResult<ResponseRepastContinued> GetContinuedPage(PageParamList<RequestRepastContinued> pageParam);
        PagedResult<ResponseRepastUpLevel> GetUpLevelPage(PageParamList<RequestRepastUpLevel> pageParam);
        String AuditContinuedAndLevel(Guid Id, bool Param);
        #endregion
        #endregion
        #region 功能管理
        #region 供应商
        PagedResult<ResponseRepastSupplier> GetSupplierPage(PageParamList<RequestRepastSupplier> pageParam);
        String DeleteSupplier(Guid Id);
        String SaveSupplier(RequestRepastSupplier Param);
        IList<ResponseRepastSupplier> GetSupplierList();
        #endregion
        #region 进货台账
        PagedResult<ResponseRepastBuybill> GetBuybillPage(PageParamList<RequestRepastBuybill> pageParam);
        String RemoveBuybill(Guid Id);
        ResponseRepastBuybill GetBuybillDetail(Guid Id);
        String EditBuybill(RequestRepastBuybill Param);
        #endregion
        #region 销售台账
        PagedResult<ResponseRepastSellbill> GetSellbillPage(PageParamList<RequestRepastSellbill> pageParam);
        String RemoveSellbill(Guid Id);
        ResponseRepastSellbill GetSellbillDetail(Guid Id);
        String EditSellbill(RequestRepastSellbill Param);
        #endregion
        #region 实时监控
        PagedResult<ResponseRepastVideo> GetVideoPage(PageParamList<RequestRepastVideo> pageParam);
        String SaveVideo(RequestRepastVideo Param);
        String DeleteVideo(Guid Id);
        String ShowVideo(Guid Id);
        #endregion
        #endregion
        #region 菜品管理
        PagedResult<ResponseRepastDish> GetDishPage(PageParamList<RequestRepastDish> pageParam);
        ResponseRepastDish GetDishDetail(Guid Id);
        String DeleteDish(Guid Id);
        String SaveDish(RequestRepastDish Param);
        #endregion
        #region 溯源追踪
        #region 原料溯源
        PagedResult<ResponseRepastStuff> GetStuffPage(PageParamList<RequestRepastStuff> pageParam);
        String RemoveStuff(Guid Id);
        String EditStuff(RequestRepastStuff Param);
        ResponseRepastStuff GetStuffDetail(Guid Id);
        #endregion
        #region 留样管理
        PagedResult<ResponseRepastSample> GetSamplePage(PageParamList<RequestRepastSample> pageParam);
        String EditSample(RequestRepastSample Param);
        String RemoveSample(Guid Id);
        #endregion
        #region 抽样管理
        PagedResult<ResponseRepastDraw> GetDrawPage(PageParamList<RequestRepastDraw> pageParam);
        String EditDraw(RequestRepastDraw Param);
        String RemoveDraw(Guid Id);
        #endregion
        #region 废物处理
        PagedResult<ResponseRepastDuck> GetDuckPage(PageParamList<RequestRepastDuck> pageParam);
        String EditDuck(RequestRepastDuck Param);
        String RemoveDuck(Guid Id);
        #endregion
        #region 消毒管理
        PagedResult<ResponseRepastDisinfect> GetDisinfectPage(PageParamList<RequestRepastDisinfect> pageParam);
        String EditDisinfect(RequestRepastDisinfect Param);
        String RemoveDisinfect(Guid Id);
        ResponseRepastDisinfect GetDisinfectDetail(Guid Id);
        #endregion
        #region 添加剂管理
        PagedResult<ResponseRepastAdditive> GetAdditivePage(PageParamList<RequestRepastAdditive> pageParam);
        String EditAdditive(RequestRepastAdditive Param);
        String RemoveAdditive(Guid Id);
        ResponseRepastAdditive GetAdditiveDetail(Guid Id);
        #endregion
        #endregion
        #region 仓库管理
        #region 原料仓库-入库
        PagedResult<ResponseRepastInStorage> GetInStoragePage(PageParamList<RequestRepastInStorage> pageParam);
        String EditInStorage(RequestRepastInStorage Param);
        String RemoveInStorage(Guid Id);
        ResponseRepastInStorage GetInStorageDetail(Guid Id);
        IList<ResponseRepastInStorage> GetInStorageList(string Param);
        #endregion
        #region 原料仓库-出库
        PagedResult<ResponseRepastOutStorage> GetOutStoragePage(PageParamList<RequestRepastOutStorage> pageParam);
        String EditOutStorage(RequestRepastOutStorage Param);
        String RemoveOutStorage(Guid Id);
        #endregion
        #region 物品仓库-入库
        PagedResult<ResponseRepastArticleInStock> GetInStockPage(PageParamList<RequestRepastArticleInStock> pageParam);
        String RemoveInStock(Guid Id);
        String EditInStock(RequestRepastArticleInStock Param);
        ResponseRepastArticleInStock GetInStockDetail(Guid Id);
        #endregion
        #region 物品仓库-出库
        PagedResult<ResponseRepastArticleOutStock> GetOutStockPage(PageParamList<RequestRepastArticleOutStock> pageParam);
        String RemoveOutStock(Guid Id);
        String EditOutStock(RequestRepastArticleOutStock Param);
        #endregion
        #region 名称管理
        PagedResult<ResponseRepastTypeName> GetNamesPage(PageParamList<RequestRepastTypeName> pageParam);
        String EditNames(RequestRepastTypeName Param);
        String RemoveNames(Guid Id);
        IList<ResponseRepastTypeName> GetNamesList(int Key);
        #endregion
        #region 库存报表
        PagedResult<ResponseRepastStockReport> GetStorageReportPage(PageParamList<RequestRepastStockReport> pageParam);
        PagedResult<ResponseRepastStockReport> GetStockReportPage(PageParamList<RequestRepastStockReport> pageParam);
        #endregion
        #endregion
        #region 微信和支付宝调用
        String AliPay(int Key, int? Value);
        String WxPay(int Key, int? Value);
        #endregion
        #region 导出Excel
        IList<Object> ExportStuffInStockFile(String Param);
         IList<Object> ExportStuffOutStockFile(String Param);
         IList<Object> ExportGoodsInStockFile(String Param);
         IList<Object> ExportGoodsOutStockFile(String Param);
        #endregion
        #region 数据统计
        Object GetDataCount(Guid? Id);
        #endregion
        #region 扫码信息
        PagedResult<ResponseRepastScanInfo> GetScanInfoPage(PageParamList<RequestRepastScanInfo> pageParam);
        String EditScanInfo(RequestRepastScanInfo Param);
        String RemoveScan(Guid Id, bool? Param);
        ResponseRepastScanInfo GetScanInfoDetail(Guid Id);
        #endregion
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
        #endregion
        #region 手机端信息
        Object GetMobileScanInfo(Guid Id);
        #endregion
    }
}
