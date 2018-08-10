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
    /// 餐饮系统业务逻辑接口
    /// </summary>
    public interface IRepastWebService : IService
    {
        #region 获取全局集团菜单
        IList<ResponseRepastDictionary> GetDictionaryList();
        IList<ResponseRepastMenu> GetRepastMenu();
        IList<ResponseParentTree> GetRepastWebTree();
        #endregion
        #region 基础管理
        #region 商家资料
        PagedResult<ResponseMerchant> GetMerchantInfoPage(PageParamList<RequestMerchant> pageParam);
        ResponseMerchant GetMerchantDetail(Guid Id);
        String EditMerchant(RequestMerchant Param);
        String SaveContract(RequestStayContract Param);
        #endregion
        #region 商家认证
        String EditMerchantIdent(RequestRepastIdent Param);
        #endregion
        #region 权限角色
        String EditRoleAuthor(RequestRoleAuthorWeb Param);
        PagedResult<ResponseRoleAuthorWeb> GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam);
        String RemoveRole(Guid Id);
        IList<ResponseRoleAuthorWeb> GetRoleAuthorList();
        #endregion
        #region 人员管理
        PagedResult<ResponseMerchantUser> GetMerchantUserPage(PageParamList<RequestMerchantUser> pageParam);
        String EditUser(RequestMerchantUser Param);
        ResponseMerchantUser GetUserDetail(Guid Id);
        String RemoveUser(Guid Id);
        IList<ResponseMerchantUser> GetMerchantList();
        #endregion
        #region 集团账户
        PagedResult<ResponseMerchant> GetChildInfoPage(PageParamList<RequestMerchant> pageParam);
        String EditChildInfo(RequestMerchant Param);
        #endregion
        #region 餐饮字典
        PagedResult<ResponseRepastDictionary> GetDicPage(PageParamList<RequestRepastDictionary> pageParam);
        String RemoveDic(Guid Id);
        ResponseRepastDictionary GetDicDetail(Guid Id);
        String EditDic(RequestRepastDictionary Param);
        #endregion
        #region 升级续费
        PagedResult<ResponseRepastLevelUp> GetLvPage(PageParamList<RequestRepastLevelUp> pageParam);
        String EditContinued(RequestRepastContinued Param);
        String EditUpLevel(RequestRepastUpLevel Param);
        PagedResult<ResponseRepastContinued> GetContinuedPage(PageParamList<RequestRepastContinued> pageParam);
        PagedResult<ResponseRepastUpLevel> GetUpLevelPage(PageParamList<RequestRepastUpLevel> pageParam);
        String AuditContinuedAndLevel(Guid Id, bool Param);
        #endregion
        #endregion
        #region 功能管理
        #region 供应商
        PagedResult<ResponseRepastSupplier> GetSupplierPage(PageParamList<RequestRepastSupplier> pageParam);
        String RemoveSupplier(Guid Id);
        String EditSupplier(RequestRepastSupplier Param);
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
        #endregion
        #region 菜品管理
        PagedResult<ResponseRepastDish> GetDishPage(PageParamList<RequestRepastDish> pageParam);
        ResponseRepastDish GetDishDetail(Guid Id);
        String RemoveDish(Guid Id);
        String EditDish(RequestRepastDish Param);
        #endregion
        #region 溯源追踪
        #region 原料溯源
        PagedResult<ResponseRepastStuff> GetStuffPage(PageParamList<RequestRepastStuff> pageParam);
        String RemoveStuff(Guid Id);
        String EditStuff(RequestRepastStuff Param);
        ResponseRepastStuff GetStuffDetail(Guid Id);
        #endregion
        #endregion
        #region 仓库管理
        #region 原料仓库-入库
        PagedResult<ResponseRepastInStorage> GetInStoragePage(PageParamList<RequestRepastInStorage> pageParam);
        String EditInStorage(RequestRepastInStorage Param);
        String RemoveInStorage(Guid Id);
        ResponseRepastInStorage GetInStorageDetail(Guid Id);
        IList<ResponseRepastInStorage> GetInStorageList();
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
    }
}
