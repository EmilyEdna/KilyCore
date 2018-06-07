using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
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
        #endregion
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
        #region 企业字典
        PagedResult<ResponseEnterpriseDictionary> GetDicPage(PageParamList<RequestEnterpriseDictionary> pageParam);
        String RemoveDic(Guid Id);
        ResponseEnterpriseDictionary GetDicDetail(Guid Id);
        String EditDic(RequestEnterpriseDictionary Param);
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
        String RemoveMaterial(Guid Id);
        #endregion
        #region 入库
        PagedResult<ResponseEnterpriseMaterialStock> GetStockPage(PageParamList<RequestEnterpriseMaterialStock> pageParam);
        ResponseEnterpriseMaterialStock GetStockDetail(Guid Id);
        String EditStock(RequestEnterpriseMaterialStock Param);
        String RemoveStock(Guid Id);
        #endregion
        #region 出库
        PagedResult<ResponseEnterpriseMaterialStockAttach> GetOutStockPage(PageParamList<RequestEnterpriseMaterialStockAttach> pageParam);
        String RemoveStockAttach(Guid Id);
        #endregion
        #endregion
    }
}