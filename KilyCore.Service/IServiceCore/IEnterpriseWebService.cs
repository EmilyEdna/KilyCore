using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.Function;
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
    /// 企业前台后也基础管理业务逻辑接口
    /// </summary>
    public interface IEnterpriseWebService : IService
    {
        #region 下拉关联列表
        IList<ResponseEnterpriseSeller> GetSellerList(int Type);
        IList<ResponseEnterpriseDictionary> GetDictionaryList();
        IList<ResponseEnterpriseSeller> GetSellerInEnterprise(String Param);
        #endregion
        #region 获取全局集团菜单
        IList<ResponseEnterpriseMenu> GetEnterpriseMenu();
        IList<ResponseParentTree> GetEnterpriseWebTree();
        #endregion
        #region 基础管理
        #region 企业信息
        PagedResult<ResponseEnterprise> GetInfoPage(PageParamList<RequestEnterprise> pageParam);
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
        IList<ResponseEnterpriseUser> GetUserList();
        #endregion
        #region 权限角色
        String EditRoleAuthor(RequestRoleAuthorWeb Param);
        PagedResult<ResponseRoleAuthorWeb> GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam);
        String RemoveRole(Guid Id);
        #endregion
        #region 集团账户
        PagedResult<ResponseEnterprise> GetChildInfo(PageParamList<RequestEnterprise> pageParam);
        String EditChildInfo(RequestEnterprise Param);
        #endregion
        #region 企业认证
        String EditEnterpriseIdent(RequestEnterpriseIdent param);
        #endregion
        #region 企业字典
        PagedResult<ResponseEnterpriseDictionary> GetDicPage(PageParamList<RequestEnterpriseDictionary> pageParam);
        String RemoveDic(Guid Id);
        ResponseEnterpriseDictionary GetDicDetail(Guid Id);
        String EditDic(RequestEnterpriseDictionary Param);
        #endregion
        #region 升级续费
        PagedResult<ResponseEnterpriseLevelUp> GetLvPage(PageParamList<RequestEnterpriseLevelUp> pageParam);
        String EditContinued(RequestEnterpriseContinued Param);
        String EditUpLevel(RequestEnterpriseUpLevel Param);
        PagedResult<ResponseEnterpriseContinued> GetContinuedPage(PageParamList<RequestEnterpriseContinued> pageParam);
        PagedResult<ResponseEnterpriseUpLevel> GetUpLevelPage(PageParamList<RequestEnterpriseUpLevel> pageParam);
        String AuditContinuedAndLevel(Guid Id, bool Param);
        #endregion
        #region 内部文件
        PagedResult<ResponseEnterpriseInsideFile> GetFilePage(PageParamList<RequestEnterpriseInsideFile> pageParam);
        ResponseEnterpriseInsideFile GetFileDetail(Guid Id);
        String RemoveFile(Guid Id);
        String EditFile(RequestEnterpriseInsideFile Param);
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
        #endregion
        #region 成长流程
        PagedResult<ResponseEnterpriseAgeUp> GetAgeUpPage(PageParamList<RequestEnterpriseAgeUp> pageParam);
        String EditAgeUp(RequestEnterpriseAgeUp Param);
        String RemoveAgeUp(Guid Id);
        #endregion
        #endregion
        #region 物码管理
        PagedResult<ResponseEnterpriseTag> GetTagPage(PageParamList<RequestEnterpriseTag> pageParam);
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
        PagedResult<ResponseEnterpriseScanCode> GetScanCodePage(PageParamList<RequestEnterpriseGoods> pageParam);
        #endregion
        #region 厂商管理
        PagedResult<ResponseEnterpriseSeller> GetSellerPage(PageParamList<RequestEnterpriseSeller> pageParam);
        String RemoveSeller(Guid Id);
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
        String EditDevice(RequestEnterpriseDevice Param);
        String EidtDeviceClean(RequestEnterpriseDeviceClean Param);
        String EidtDeviceFix(RequestEnterpriseDeviceFix Param);
        String RemoveDevice(Guid Id);
        String RemoveDeviceClean(Guid Id);
        String RemoveDeviceFix(Guid Id);
        #endregion
        #region 指标把控
        PagedResult<ResponseEnterpriseTarget> GetTargetPage(PageParamList<RequestEnterpriseTarget> pageParam);
        String RemoveTarget(Guid Id);
        String EditTarget(RequestEnterpriseTarget Param);
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
        String EditFac(RequestEnterpriseFacilities Param);
        String RemoveFac(Guid Id);
        PagedResult<ResponseEnterpriseFacilitiesAttach> GetFacAttachPage(PageParamList<RequestEnterpriseFacilitiesAttach> pageParam);
        String EditFacAttach(RequestEnterpriseFacilitiesAttach Param);
        String RemoveFacAttach(Guid Id);
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
        String BindTarget(RequestEnterpriseTagAttach Param);
        PagedResult<ResponseEnterpriseGoodsStockAttach> GetGoodsStockAttachPage(PageParamList<RequestEnterpriseGoodsStockAttach> pageParam);
        String EditStockAttach(RequestEnterpriseGoodsStockAttach Param);
        String RemoveGoodsStockAttach(Guid Id);
        long GetCodeSerialNo(Guid Id);
        IList<ResponseEnterpriseGoodsStockAttach> GetStockOutNoList();
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
        PagedResult<ResponseEnterpriseInferiorExprired> GetInferiorExpriredPage(PageParamList<RequestEnterpriseInferiorExprired> pageParam);
        ResponseEnterpriseInferiorExprired GetInferiorExpriredDetail(Guid Id);
        String RemoveInferiorExprired(Guid Id);
        String EditInferiorExprired(RequestEnterpriseInferiorExprired Param);
        #endregion
        #region 召回处理
        PagedResult<ResponseEnterpriseRecover> GetRecoverPage(PageParamList<RequestEnterpriseRecover> pageParam);
        ResponseEnterpriseRecover GetRecoverDetail(Guid Id);
        String EditRecover(RequestEnterpriseRecover Param);
        String RemoveRecover(Guid Id);
        #endregion
        #endregion
        #region 物流管理
        #region 打包管理
        PagedResult<ResponseEnterpriseGoodsPackage> GetPackagePage(PageParamList<RequestEnterpriseGoodsPackage> pageParam);
        String EditGoodsPackage(RequestEnterpriseGoodsPackage Param);
        ResponseEnterpriseGoodsPackage GetGoodsPackageDetail(Guid Id);
        String RemoveGoodsPackge(Guid Id);
        long GetPackageCode(String StockOutNo);
        #endregion
        #region 发货收货
        PagedResult<ResponseEnterpriseLogistics> GetLogisticsPage(PageParamList<RequestEnterpriseLogistics> pageParam);
        String EditLogistics(RequestEnterpriseLogistics Param);
        String RemoveLogistics(Guid Id);
        String CheckLogistics(Guid Id);
        #endregion
        #region 进货管理
        PagedResult<ResponseEnterpriseBuyer> GetBuyerPage(PageParamList<RequestEnterpriseBuyer> pageParam);
        String EditBuyer(RequestEnterpriseBuyer Param);
        String RemoveBuyer(Guid Id);
        #endregion
        #endregion
        #region 微信和支付宝调用
        String AliPay(int Key, int? Value);
        String WxPay(int Key, int? Value);
        #endregion
    }
}