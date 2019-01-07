using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Function;
using KilyCore.DataEntity.ResponseMapper.Govt;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.Service.QueryExtend;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.IServiceCore
{
    /// <summary>
    /// 企业前台后台业务逻辑接口
    /// </summary>
    public interface IEnterpriseWebService : IService
    {
        #region 下拉关联列表
        IList<ResponseEnterpriseSeller> GetSellerList(int Type);
        IList<ResponseEnterpriseDictionary> GetDictionaryList(String Param);
        IList<ResponseEnterpriseSeller> GetSellerInEnterprise(String Param);
        #endregion
        #region 获取全局集团菜单
        IList<ResponseEnterpriseMenu> GetEnterpriseMenu();
        IList<ResponseParentTree> GetEnterpriseWebTree(String Key);
        #endregion
        #region 基础管理
        #region 企业信息
        PagedResult<ResponseEnterprise> GetInfoPage(PageParamList<RequestEnterprise> pageParam);
        ResponseEnterprise GetEnterpriseInfo(Guid Id);
        IDictionary<Guid, String> GetChildAccount(Guid Id);
        String SaveEnterprise(RequestEnterprise param);
        String SaveCompanyAccount(RequestEnterprise Param);
        String SaveCompanyArea(RequestEnterprise Param);
        #endregion
        #region 保存合同和缴费凭证
        ResponseStayContract SaveContract(RequestStayContract Param);
        #endregion
        #region 人员管理
        PagedResult<ResponseEnterpriseUser> GetUserPage(PageParamList<RequestEnterpriseUser> pageParam);
        String SaveUser(RequestEnterpriseUser Param);
        String DeleteUser(Guid Id);
        ResponseEnterpriseUser GetUserDetail(Guid Id);
        IList<ResponseRoleAuthorWeb> GetRoleAuthorList();
        IList<ResponseEnterpriseUser> GetUserList();
        #endregion
        #region 权限角色
        String SaveRoleAuthor(RequestRoleAuthorWeb Param);
        PagedResult<ResponseRoleAuthorWeb> GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam);
        String DeleteRole(Guid Id);
        #endregion
        #region 集团账户
        PagedResult<ResponseEnterprise> GetChildInfo(PageParamList<RequestEnterprise> pageParam);
        String SaveChildInfo(RequestEnterprise Param);
        #endregion
        #region 企业认证
        String SaveEnterpriseIdent(RequestEnterpriseIdent param);
        PagedResult<ResponseEnterpriseIdent> GetIndentPage(PageParamList<RequestEnterpriseIdent> pageParam);
        #endregion
        #region 企业字典
        PagedResult<ResponseEnterpriseDictionary> GetDicPage(PageParamList<RequestEnterpriseDictionary> pageParam);
        String DeleteDic(Guid Id);
        ResponseEnterpriseDictionary GetDicDetail(Guid Id);
        String SaveDic(RequestEnterpriseDictionary Param);
        #endregion
        #region 升级续费
        PagedResult<ResponseEnterpriseLevelUp> GetLvPage(PageParamList<RequestEnterpriseLevelUp> pageParam);
        String SaveContinued(RequestEnterpriseContinued Param);
        String SaveUpLevel(RequestEnterpriseUpLevel Param);
        PagedResult<ResponseEnterpriseContinued> GetContinuedPage(PageParamList<RequestEnterpriseContinued> pageParam);
        PagedResult<ResponseEnterpriseUpLevel> GetUpLevelPage(PageParamList<RequestEnterpriseUpLevel> pageParam);
        String AuditContinuedAndLevel(Guid Id, bool Param);
        #endregion
        #region 内部文件
        PagedResult<ResponseEnterpriseInsideFile> GetFilePage(PageParamList<RequestEnterpriseInsideFile> pageParam);
        ResponseEnterpriseInsideFile GetFileDetail(Guid Id);
        String DeleteFile(Guid Id);
        String SaveFile(RequestEnterpriseInsideFile Param);
        #endregion
        #region 视频监控
        PagedResult<ResponseEnterpriseVedio> GetVedioPage(PageParamList<RequestEnterpriseVedio> pageParam);
        String EditVedio(RequestEnterpriseVedio Param);
        String DeleteVedio(Guid Id);
        String ShowVedio(Guid Id, bool flag);
        #endregion
        #region 企业自查
         PagedResult<ResponseGovtTemplateChild> GetTemplateChild(PageParamList<RequestGovtTemplateChild> pageParam);
         String EditTemplateChild(RequestGovtTemplateChild Param);
         String DeleteTemplate(Guid Id);
        #endregion
        #endregion
        #region 成长档案
        #region 育苗信息
        PagedResult<ResponseEnterpriseGrowInfo> GetGrowInfoPage(PageParamList<RequestEnterpriseGrowInfo> pageParam);
        ResponseEnterpriseGrowInfo GetGrowDetail(Guid Id);
        String EditGrow(RequestEnterpriseGrowInfo Param);
        String RemoveGrow(Guid Id);
        IList<ResponseEnterpriseGrowInfo> GetGrowList();
        #endregion
        #region 施养管理
        PagedResult<ResponseEnterprisePlanting> GetPlantingPage(PageParamList<RequestEnterprisePlanting> pageParam);
        String EditPlanting(RequestEnterprisePlanting Param);
        String RemovePlanting(Guid Id);
        #endregion
        #region 农药疫情
        PagedResult<ResponseEnterpriseDrug> GetDrugPage(PageParamList<RequestEnterpriseDrug> pageParam);
        String EditDrug(RequestEnterpriseDrug Param);
        String RemoveDrug(Guid Id);
        #endregion
        #region 环境检测
        PagedResult<ResponseEnterpriseEnvironment> GetEnvPage(PageParamList<RequestEnterpriseEnvironment> pageParam);
        PagedResult<ResponseEnterpriseEnvironmentAttach> GetEnvAttachPage(PageParamList<RequestEnterpriseEnvironmentAttach> pageParam);
        String EditEnv(RequestEnterpriseEnvironment Param);
        String EditEnvAttach(RequestEnterpriseEnvironmentAttach Param);
        String RemoveEnv(Guid Id);
        String RemoveEnvAttach(Guid Id);
        #endregion
        #region 成长日记
        PagedResult<ResponseEnterpriseNote> GetNotePage(PageParamList<RequestEnterpriseNote> pageParam);
        String EditNote(RequestEnterpriseNote Param);
        String RemoveNote(Guid Id);
        ResponseEnterpriseNote GetNoteDetail(Guid Id);
        IList<ResponseEnterpriseNote> GetNoteList();
        #endregion
        #region 成长流程
        PagedResult<ResponseEnterpriseAgeUp> GetAgeUpPage(PageParamList<RequestEnterpriseAgeUp> pageParam);
        String EditAgeUp(RequestEnterpriseAgeUp Param);
        String RemoveAgeUp(Guid Id);
        #endregion
        #endregion
        #region 物码管理
        PagedResult<ResponseEnterpriseTag> GetTagPage(PageParamList<RequestEnterpriseTag> pageParam);
        ResponseEnterpriseTag GetTagDetailWeb(String key);
        String UpdateEmptyTag(Guid Id);
        String CreateTag(RequestEnterpriseTag Param);
        String RemoveTag(Guid Id);
        String RemoveApplyTag(Guid Id);
        PagedResult<ResponseEnterpriseApply> GetTagApplyPage(PageParamList<RequestEnterpriseApply> pageParam);
        String ApplyEdit(RequestEnterpriseApply Param);
        ResponseEnterpriseApply GetPaymentDetail(Guid Id);
        PagedResult<ResponseVeinTag> GetVeinTargetPage(PageParamList<RequestVeinTag> pageParam);
        String AcceptVeinTarget(Guid Id);
        String RemoveVeinTarget(Guid Id);
        Object GetTagList(int type);
        Object GetCodeNo(int Type, string BatchNo);
        PagedResult<ResponseEnterpriseScanCode> GetScanCodePage(PageParamList<RequestEnterpriseGoods> pageParam);
        PagedResult<ResponseEnterpriseTagAttach> GetTagAttachPage(PageParamList<RequestEnterpriseTagAttach> pageParam);
        String DeleteBindTagInfo(Guid Id);
        PagedResult<ResponseEnterpriseBoxing> GetBoxPage(PageParamList<RequestEnterpriseBoxing> pageParam);
        #endregion
        #region 厂商管理
        PagedResult<ResponseEnterpriseSeller> GetSellerPage(PageParamList<RequestEnterpriseSeller> pageParam);
        String DeleteSeller(Guid Id);
        String EditSeller(RequestEnterpriseSeller Param);
        ResponseEnterpriseSeller GetSellerDetail(Guid Id);
        #endregion
        #region 原料管理
        #region 原料
        PagedResult<ResponseEnterpriseMaterial> GetMaterialPage(PageParamList<RequestEnterpriseMaterial> pageParam);
        String EditMaterial(RequestEnterpriseMaterial Param);
        IList<ResponseEnterpriseMaterial> GetMaterialList();
        String RemoveMaterial(Guid Id);
        #endregion
        #region 入库
        PagedResult<ResponseEnterpriseMaterialStock> GetStockPage(PageParamList<RequestEnterpriseMaterialStock> pageParam);
        String EditStock(RequestEnterpriseMaterialStock Param);
        String RemoveStock(Guid Id);
        String UpdateMaterStockCheck(RequestEnterpriseMaterialStock Param);
        #endregion
        #region 出库
        PagedResult<ResponseEnterpriseMaterialStockAttach> GetOutStockPage(PageParamList<RequestEnterpriseMaterialStockAttach> pageParam);
        String RemoveStockAttach(Guid Id);
        String EditOutStock(RequestEnterpriseMaterialStockAttach Param);
        IList<ResponseEnterpriseMaterial> GetOutStockMaterialList();
        #endregion
        #endregion
        #region 生产管理
        #region 设备管理
        PagedResult<ResponseEnterpriseDevice> GetDevicePage(PageParamList<RequestEnterpriseDevice> pageParam);
        PagedResult<ResponseEnterpriseDeviceClean> GetDeviceCleanPage(PageParamList<RequestEnterpriseDeviceClean> pageParam);
        PagedResult<ResponseEnterpriseDeviceFix> GetDeviceFixPage(PageParamList<RequestEnterpriseDeviceFix> pageParam);
        IList<ResponseEnterpriseDevice> GetDeviceList();
        String SaveDevice(RequestEnterpriseDevice Param);
        String SaveDeviceClean(RequestEnterpriseDeviceClean Param);
        String SaveDeviceFix(RequestEnterpriseDeviceFix Param);
        String DeleteDevice(Guid Id);
        String DeleteDeviceClean(Guid Id);
        String DeleteDeviceFix(Guid Id);
        #endregion
        #region 指标把控
        PagedResult<ResponseEnterpriseTarget> GetTargetPage(PageParamList<RequestEnterpriseTarget> pageParam);
        String DeleteTarget(Guid Id);
        String SaveTarget(RequestEnterpriseTarget Param);
        IList<ResponseEnterpriseTarget> GetTargetList();
        #endregion
        #region 产品系列
        PagedResult<ResponseEnterpriseProductSeries> GetSeriesPage(PageParamList<RequestEnterpriseProductSeries> pageParam);
        String RemoveSeries(Guid Id);
        String EditSeries(RequestEnterpriseProductSeries Param);
        IList<ResponseEnterpriseProductSeries> GetSeriesList();
        #endregion
        #region 生产批次
        PagedResult<ResponseEnterpriseProductionBatch> GetProBatchPage(PageParamList<RequestEnterpriseProductionBatch> pageParam);
        IList<ResponseEnterpriseProductionBatch> GetProBatchList();
        String RemoveProBatch(Guid Id);
        String EditProBatch(RequestEnterpriseProductionBatch Param);
        IList<ResponseEnterpriseProductionBatchAttach> GetProBatchAttachList(Guid Id);
        String EditProBatchAttach(RequestEnterpriseProductionBatchAttach Param);
        #endregion
        #region 设施管理
        IList<ResponseEnterpriseFacilities> GetFacList();
        PagedResult<ResponseEnterpriseFacilities> GetFacPage(PageParamList<RequestEnterpriseFacilities> pageParam);
        String SaveFac(RequestEnterpriseFacilities Param);
        String DeleteFac(Guid Id);
        PagedResult<ResponseEnterpriseFacilitiesAttach> GetFacAttachPage(PageParamList<RequestEnterpriseFacilitiesAttach> pageParam);
        String SaveFacAttach(RequestEnterpriseFacilitiesAttach Param);
        String DeleteFacAttach(Guid Id);
        #endregion
        #endregion
        #region 产品管理
        #region 产品列表
        PagedResult<ResponseEnterpriseGoods> GetGoodsPage(PageParamList<RequestEnterpriseGoods> pageParam);
        String RemoveGoods(Guid Id);
        String EditGoods(RequestEnterpriseGoods Param);
        ResponseEnterpriseGoods GetGoodsDetail(Guid Id);
        IList<ResponseEnterpriseGoods> GetGoodsList();
        #endregion
        #region 产品仓库
        String EditExplanation(RequestEnterpriseGoodsStock Param);
        String AuditGoods(RequestEnterpriseGoodsStock Param);
        PagedResult<ResponseEnterpriseGoodsStock> GetGoodsStockPage(PageParamList<RequestEnterpriseGoodsStock> pageParam);
        String RemoveGoodsStock(Guid Id);
        String EditGoodsStock(RequestEnterpriseGoodsStock Param);
        String EditBoxing(RequestEnterpriseBoxing Param);
        String BindTarget(RequestEnterpriseTagAttach Param);
        PagedResult<ResponseEnterpriseGoodsStockAttach> GetGoodsStockAttachPage(PageParamList<RequestEnterpriseGoodsStockAttach> pageParam);
        String EditStockAttach(RequestEnterpriseGoodsStockAttach Param);
        String RemoveGoodsStockAttach(Guid Id);
        IList<ResponseEnterpriseGoodsStockAttach> GetStockOutNoList();
        String UpdateStockCheck(RequestEnterpriseGoodsStock Param);
        #endregion
        #region 仓库类型
        PagedResult<ResponseEnterpriseStockType> GetStockTypePage(PageParamList<RequestEnterpriseStockType> pageParam);
        IList<ResponseEnterpriseStockType> GetStockTypeList(String Param);
        String RemoveStockType(Guid Id);
        String EditStockType(RequestEnterpriseStockType Param);
        #endregion
        #endregion
        #region 品质管理
        #region 原料产品质检
        PagedResult<ResponseEnterpriseCheckMaterial> GetCheckMaterialPage(PageParamList<RequestEnterpriseCheckMaterial> pageParam);
        String EditCheckMaterial(RequestEnterpriseCheckMaterial Param);
        String RemoveCheckMaterial(Guid Id);
        PagedResult<ResponseEnterpriseCheckGoods> GetCheckGoodsPage(PageParamList<RequestEnterpriseCheckGoods> pageParam);
        String EditCheckGoods(RequestEnterpriseCheckGoods Param);
        String RemoveCheckGoods(Guid Id);
        IList<ResponseEnterpriseCheckMaterial> GetCheckMaterial();
        IList<ResponseEnterpriseCheckGoods> GetCheckGoodsList();
        #endregion
        #region 过期不合格处理
        Object GetExpiredPage(PageParamList<Object> pageParam);
        PagedResult<ResponseEnterpriseInferiorExprired> GetInferiorExpriredPage(PageParamList<RequestEnterpriseInferiorExprired> pageParam);
        ResponseEnterpriseInferiorExprired GetInferiorExpriredDetail(Guid Id);
        String RemoveInferiorExprired(Guid Id);
        String EditInferiorExprired(RequestEnterpriseInferiorExprired Param);
        #endregion
        #region 召回处理
        PagedResult<ResponseEnterpriseRecover> GetRecoverPage(PageParamList<RequestEnterpriseRecover> pageParam);
        ResponseEnterpriseRecover GetRecoverDetail(Guid Id);
        String SaveRecover(RequestEnterpriseRecover Param);
        String RemoveRecover(Guid Id);
        #endregion
        #endregion
        #region 物流管理
        #region 打包管理
        PagedResult<ResponseEnterpriseGoodsPackage> GetPackagePage(PageParamList<RequestEnterpriseGoodsPackage> pageParam);
        String EditGoodsPackage(RequestEnterpriseGoodsPackage Param);
        ResponseEnterpriseGoodsPackage GetGoodsPackageDetail(Guid Id);
        String RemoveGoodsPackge(Guid Id);
        IList<ResponseEnterpriseGoodsPackage> GetPackagesList();
        #endregion
        #region 发货收货
        PagedResult<ResponseEnterpriseLogistics> GetLogisticsPage(PageParamList<RequestEnterpriseLogistics> pageParam);
        IList<ResponseEnterpriseLogistics> GetReceipt();
        String EditLogistics(RequestEnterpriseLogistics Param);
        String RemoveLogistics(Guid Id);
        #endregion
        #region 进货管理
        PagedResult<ResponseEnterpriseBuyer> GetBuyerPage(PageParamList<RequestEnterpriseBuyer> pageParam);
        String EditBuyer(RequestEnterpriseBuyer Param);
        String RemoveBuyer(Guid Id);
        IList<ResponseEnterpriseBuyer> GetBuyerList();
        #endregion
        #endregion
        #region 微信和支付宝调用
        String AliPay(int Key, int? Value);
        String WxPay(int Key, int? Value);
        String WxQueryPay(RequestContractTemp Param);
        #endregion
        #region 导出Excel
        IList<Object> ExportMaterialInStockFile(String Param);
        IList<Object> ExportMaterialOutStockFile(String Param);
        IList<Object> ExportProInStockFile(String Param);
        IList<Object> ExportProOutStockFile(String Param);
        #endregion
        #region 数据统计
        Object GetDataCount(Guid? Id);
        ResponseDataCount GetPieCount(Guid? Id);
        ResponseDataCount GetPieCountBatch(Guid? Id);
        #endregion
        #region 手机扫描页面
        ResponseEnterprise GetScanCompanyFirst(Guid Id);
        ResponseEnterpriseScanCodeContent GetScanCodeInfo(Guid? Id, String Code);
        String EditScanInfo(RequestEnterpriseScanCodeInfo Param);
        ResponseEnterpriseBoxing GetScanBoxInfo(Guid? Id, String Code);
        RequestEnterpriseLogistics GetScanSendInfo(Guid Id);
        String CheckLogistics(RequestEnterpriseLogistics Param);
        ResponseEnterpriseGoodsPackage GetScanPackageInfo(Guid Id);
        #endregion
    }
}