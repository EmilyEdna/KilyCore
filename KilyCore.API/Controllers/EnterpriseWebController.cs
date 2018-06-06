using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class EnterpriseWebController : BaseController
    {
        #region 下拉关联列表
        /// <summary>
        /// 下拉厂商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetSellerList")]
        public ObjectResultEx GetSellerList(SimlpeParam<int> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetSellerList(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 下拉字典类型
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDictionaryList")]
        public ObjectResultEx GetDictionaryList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDictionaryList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 下拉字典值
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetDictionaryList")]
        public ObjectResultEx GetDictionaryList(SimlpeParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDictionaryList(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 获取全局集团菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEnterpriseMenu")]
        public ObjectResultEx GetEnterpriseMenu()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnterpriseMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取权限菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEnterpriseWebTree")]
        public ObjectResultEx GetEnterpriseWebTree()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnterpriseWebTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 基础管理
        #region  企业信息
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetEnterpriseInfo")]
        public ObjectResultEx GetEnterpriseInfo(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnterpriseInfo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑企业
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditEnterprise")]
        public ObjectResultEx EditEnterprise(RequestEnterprise Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditEnterprise(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 保存合同和缴费凭证
        /// <summary>
        /// 保存合同和缴费凭证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveContract")]
        public ObjectResultEx SaveContract(RequestStayContract Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveContract(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 人员管理
        /// <summary>
        /// 获取人员管理分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetUserPage")]
        public ObjectResultEx GetUserPage(PageParamList<RequestEnterpriseUser> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetUserPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditUser")]
        public ObjectResultEx EditUser(RequestEnterpriseUser Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditUser(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetUserDetail")]
        public ObjectResultEx GetUserDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetUserDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除子账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveUser")]
        public ObjectResultEx RemoveUser(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveUser(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 集团账户权限列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorList")]
        public ObjectResultEx GetRoleAuthorList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetRoleAuthorList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 集团账户
        /// <summary>
        /// 新增账户
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditRoleAuthor")]
        public ObjectResultEx EditRoleAuthor(RequestRoleAuthorWeb Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditRoleAuthor(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 账户分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorPage")]
        public ObjectResultEx GetRoleAuthorPage(PageParamList<RequestRoleAuthorWeb> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetRoleAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveRole")]
        public ObjectResultEx RemoveRole(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 企业认证
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditEnterpriseIdent")]
        public ObjectResultEx EditEnterpriseIdent(RequestEnterpriseIdent Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditEnterpriseIdent(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 企业字典
        /// <summary>
        /// 字典分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDicPage")]
        public ObjectResultEx GetDicPage(PageParamList<RequestEnterpriseDictionary> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDicPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveDic")]
        public ObjectResultEx RemoveDic(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveDic(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 字典详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetDicDetail")]
        public ObjectResultEx GetDicDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDicDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDic")]
        public ObjectResultEx EditDic(RequestEnterpriseDictionary Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditDic(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
        #region 成长档案
        #region 育苗信息
        /// <summary>
        /// 育苗分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetGrowInfoPage")]
        public ObjectResultEx GetGrowInfoPage(PageParamList<RequestEnterpriseGrowInfo> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGrowInfoPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取育苗详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetGrowDetail")]
        public ObjectResultEx GetGrowDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGrowDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑育苗信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditGrow")]
        public ObjectResultEx EditGrow(RequestEnterpriseGrowInfo Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditGrow(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除育苗信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveGrow")]
        public ObjectResultEx RemoveGrow(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveGrow(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取批次列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetGrowList")]
        public ObjectResultEx GetGrowList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGrowList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 施养管理
        /// <summary>
        /// 施养管理分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetPlantingPage")]
        public ObjectResultEx GetPlantingPage(PageParamList<RequestEnterprisePlanting> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetPlantingPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 新增施养记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditPlanting")]
        public ObjectResultEx EditPlanting(RequestEnterprisePlanting Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditPlanting(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemovePlanting")]
        public ObjectResultEx RemovePlanting(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemovePlanting(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 农药疫情
        /// <summary>
        /// 农药疫情分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDrugPage")]
        public ObjectResultEx GetDrugPage(PageParamList<RequestEnterpriseDrug> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDrugPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 新增农药疫情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDrug")]
        public ObjectResultEx EditDrug(RequestEnterpriseDrug Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditDrug(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveDrug")]
        public ObjectResultEx RemoveDrug(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveDrug(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 环境检测
        /// <summary>
        /// 获取环境检测主表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetEnvPage")]
        public ObjectResultEx GetEnvPage(PageParamList<RequestEnterpriseEnvironment> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnvPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取环境检测从表分页
        /// </summary>
        /// <param name="pageParam"></param>
        [HttpPost("GetEnvAttachPage")]
        public ObjectResultEx GetEnvAttachPage(PageParamList<RequestEnterpriseEnvironmentAttach> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnvAttachPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑主表信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditEnv")]
        public ObjectResultEx EditEnv(RequestEnterpriseEnvironment Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditEnv(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑从表信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditEnvAttach")]
        public ObjectResultEx EditEnvAttach(RequestEnterpriseEnvironmentAttach Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditEnvAttach(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除主表信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveEnv")]
        public ObjectResultEx RemoveEnv(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveEnv(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除从表信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveEnvAttach")]
        public ObjectResultEx RemoveEnvAttach(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveEnvAttach(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 成长日记
        /// <summary>
        /// 成长日记分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetNotePage")]
        public ObjectResultEx GetNotePage(PageParamList<RequestEnterpriseNote> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetNotePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑日记
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditNote")]
        public ObjectResultEx EditNote(RequestEnterpriseNote Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditNote(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除日记
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveNote")]
        public ObjectResultEx RemoveNote(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveNote(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        [HttpPost("GetNoteDetail")]
        public ObjectResultEx GetNoteDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetNoteDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 成长流程
        /// <summary>
        /// 成长流程分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetAgeUpPage")]
        public ObjectResultEx GetAgeUpPage(PageParamList<RequestEnterpriseAgeUp> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetAgeUpPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑成长流程
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditAgeUp")]
        public ObjectResultEx EditAgeUp(RequestEnterpriseAgeUp Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditAgeUp(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveAgeUp")]
        public ObjectResultEx RemoveAgeUp(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveAgeUp(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
        #region 二维码管理
        /// <summary>
        /// 二维码分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagPage")]
        public ObjectResultEx GetTagPage(PageParamList<RequestEnterpriseTag> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTagPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("CreateTag")]
        public ObjectResultEx CreateTag(RequestEnterpriseTag Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.CreateTag(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除二维码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveTag")]
        public ObjectResultEx RemoveTag(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveTag(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 申请二维码分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagApplyPage")]
        public ObjectResultEx GetTagApplyPage(PageParamList<RequestEnterpriseApply> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTagApplyPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除申请标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveApplyTag")]
        public ObjectResultEx RemoveApplyTag(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveApplyTag(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 申请标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ApplyEdit")]
        public ObjectResultEx ApplyEdit(RequestEnterpriseApply Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.ApplyEdit(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取缴费详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetPaymentDetail")]
        public ObjectResultEx GetPaymentDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetPaymentDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 厂商管理
        /// <summary>
        /// 厂商分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetSellerPage")]
        public ObjectResultEx GetSellerPage(PageParamList<RequestEnterpriseSeller> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetSellerPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除厂商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveSeller")]
        public ObjectResultEx RemoveSeller(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveSeller(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑厂商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditSeller")]
        public ObjectResultEx EditSeller(RequestEnterpriseSeller Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditSeller(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 厂商详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetSellerDetail")]
        public ObjectResultEx GetSellerDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetSellerDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 原料管理
        /// <summary>
        /// 原料分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMaterialPage")]
        public ObjectResultEx GetMaterialPage(PageParamList<RequestEnterpriseMaterial> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetMaterialPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑原料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditMaterial")]
        public ObjectResultEx EditMaterial(RequestEnterpriseMaterial Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditMaterial(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除原料
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("Remove")]
        public ObjectResultEx Remove(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveMaterial(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}