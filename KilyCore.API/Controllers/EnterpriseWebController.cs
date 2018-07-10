using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Function;
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
        /// 获取入住企业的经销商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetSellerInEnterprise")]
        public ObjectResultEx GetSellerInEnterprise(SimlpeParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetSellerInEnterprise(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// 企业资料分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetInfoPage")]
        public ObjectResultEx GetInfoPage(PageParamList<RequestEnterprise> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetInfoPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetUserList")]
        public ObjectResultEx GetUserList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetUserList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        #region 升级续费
        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetLvPage")]
        public ObjectResultEx GetLvPage(PageParamList<RequestEnterpriseLevelUp> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetLvPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑续费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditContinued")]
        public ObjectResultEx EditContinued(RequestEnterpriseContinued Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditContinued(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑升级
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditUpLevel")]
        public ObjectResultEx EditUpLevel(RequestEnterpriseUpLevel Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditUpLevel(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 续费记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetContinuedPage")]
        public ObjectResultEx GetContinuedPage(PageParamList<RequestEnterpriseContinued> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetContinuedPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 升级记录
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetUpLevelPage")]
        public ObjectResultEx GetUpLevelPage(PageParamList<RequestEnterpriseUpLevel> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetUpLevelPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核审计续费
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditContinuedAndLevel")]
        public ObjectResultEx AuditContinuedAndLevel(SimlpeParam<Guid> Key, SimlpeParam<bool> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.AuditContinuedAndLevel(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 内部文件
        /// <summary>
        /// 文件分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetFilePage")]
        public ObjectResultEx GetFilePage(PageParamList<RequestEnterpriseInsideFile> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetFilePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 文件详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetFileDetail")]
        public ObjectResultEx GetFileDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetFileDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveFile")]
        public ObjectResultEx RemoveFile(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑文件
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditFile")]
        public ObjectResultEx EditFile(RequestEnterpriseInsideFile Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditFile(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <summary>
        /// 纹理二维码分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetVeinTargetPage")]
        public ObjectResultEx GetVeinTargetPage(PageParamList<RequestVeinTag> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetVeinTargetPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 纹理二维码签收
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AcceptVeinTarget")]
        public ObjectResultEx AcceptVeinTarget(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.AcceptVeinTarget(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 纹理二维码删除
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveVeinTarget")]
        public ObjectResultEx RemoveVeinTarget(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveVeinTarget(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取标签批次
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTagList")]
        public ObjectResultEx GetTagList(SimlpeParam<int> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTagList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("RemoveMaterial")]
        public ObjectResultEx RemoveMaterial(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveMaterial(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取下拉原料
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMaterialList")]
        public ObjectResultEx GetMaterialList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetMaterialList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 原料仓库
        /// <summary>
        /// 获取入库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetStockPage")]
        public ObjectResultEx GetStockPage(PageParamList<RequestEnterpriseMaterialStock> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetStockPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除入库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveStock")]
        public ObjectResultEx RemoveStock(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveStock(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑入库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditStock")]
        public ObjectResultEx EditStock(RequestEnterpriseMaterialStock Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditStock(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 出库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetOutStockPage")]
        public ObjectResultEx GetOutStockPage(PageParamList<RequestEnterpriseMaterialStockAttach> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetOutStockPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除出库记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveStockAttach")]
        public ObjectResultEx RemoveStockAttach(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveStockAttach(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑出库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditOutStock")]
        public ObjectResultEx EditOutStock(RequestEnterpriseMaterialStockAttach Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditOutStock(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取出库原料列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetOutStockMaterialList")]
        public ObjectResultEx GetOutStockMaterialList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetOutStockMaterialList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 生产管理
        #region 设备管理
        /// <summary>
        /// 设备分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDevicePage")]
        public ObjectResultEx GetDevicePage(PageParamList<RequestEnterpriseDevice> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDevicePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 设备清洗分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDeviceCleanPage")]
        public ObjectResultEx GetDeviceCleanPage(PageParamList<RequestEnterpriseDeviceClean> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDeviceCleanPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 设备维护分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDeviceFixPage")]
        public ObjectResultEx GetDeviceFixPage(PageParamList<RequestEnterpriseDeviceFix> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDeviceFixPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDeviceList")]
        public ObjectResultEx GetDeviceList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDeviceList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑设备
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDevice")]
        public ObjectResultEx EditDevice(RequestEnterpriseDevice Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditDevice(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑设备清洗
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EidtDeviceClean")]
        public ObjectResultEx EidtDeviceClean(RequestEnterpriseDeviceClean Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EidtDeviceClean(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑设备维护
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EidtDeviceFix")]
        public ObjectResultEx EidtDeviceFix(RequestEnterpriseDeviceFix Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EidtDeviceFix(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDevice")]
        public ObjectResultEx RemoveDevice(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveDevice(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除清洗记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDeviceClean")]
        public ObjectResultEx RemoveDeviceClean(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveDeviceClean(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除维护记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveDeviceFix")]
        public ObjectResultEx RemoveDeviceFix(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveDeviceFix(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 指标把控
        /// <summary>
        /// 指标分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTargetPage")]
        public ObjectResultEx GetTargetPage(PageParamList<RequestEnterpriseTarget> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTargetPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除指标
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveTarget")]
        public ObjectResultEx RemoveTarget(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveTarget(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取指标列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetTargetList")]
        public ObjectResultEx GetTargetList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTargetList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑指标
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditTarget")]
        public ObjectResultEx EditTarget(RequestEnterpriseTarget Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditTarget(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 产品系列
        /// <summary>
        /// 产品系列分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetSeriesPage")]
        public ObjectResultEx GetSeriesPage(PageParamList<RequestEnterpriseProductSeries> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetSeriesPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除系列
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveSeries")]
        public ObjectResultEx RemoveSeries(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveSeries(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 系列列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSeriesList")]
        public ObjectResultEx GetSeriesList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetSeriesList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑系列
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditSeries")]
        public ObjectResultEx EditSeries(RequestEnterpriseProductSeries Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditSeries(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 生产批次
        /// <summary>
        /// 生产批次分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetProBatchPage")]
        public ObjectResultEx GetProBatchPage(PageParamList<RequestEnterpriseProductionBatch> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetProBatchPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 生产批次列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetProBatchList")]
        public ObjectResultEx GetProBatchList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetProBatchList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除生产批次
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveProBatch")]
        public ObjectResultEx RemoveProBatch(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveProBatch(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑生产批次
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditProBatch")]
        public ObjectResultEx EditProBatch(RequestEnterpriseProductionBatch Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditProBatch(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 对比指标值
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetProBatchAttachList")]
        public ObjectResultEx GetProBatchAttachList(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetProBatchAttachList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑对比指标
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditProBatchAttach")]
        public ObjectResultEx EditProBatchAttach(RequestEnterpriseProductionBatchAttach Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditProBatchAttach(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 设施管理
        /// <summary>
        /// 设施列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetFacList")]
        public ObjectResultEx GetFacList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetFacList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 设施分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetFacPage")]
        public ObjectResultEx GetFacPage(PageParamList<RequestEnterpriseFacilities> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetFacPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑设施
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditFac")]
        public ObjectResultEx EditFac(RequestEnterpriseFacilities Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditFac(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除设施
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveFac")]
        public ObjectResultEx RemoveFac(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveFac(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 设施附加分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetFacAttachPage")]
        public ObjectResultEx GetFacAttachPage(PageParamList<RequestEnterpriseFacilitiesAttach> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetFacAttachPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑设施附加
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditFacAttach")]
        public ObjectResultEx EditFacAttach(RequestEnterpriseFacilitiesAttach Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditFacAttach(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除设施附加
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveFacAttach")]
        public ObjectResultEx RemoveFacAttach(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveFacAttach(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
        #region 产品管理
        #region 产品列表
        /// <summary>
        /// 产品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetGoodsPage")]
        public ObjectResultEx GetGoodsPage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGoodsPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveGoods")]
        public ObjectResultEx RemoveGoods(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveGoods(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑产品
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditGoods")]
        public ObjectResultEx EditGoods(RequestEnterpriseGoods Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditGoods(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取产品详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetGoodsDetail")]
        public ObjectResultEx GetGoodsDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGoodsDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取产品下拉
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetGoodsList")]
        public ObjectResultEx GetGoodsList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGoodsList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 产品仓库
        /// <summary>
        ///提交产品审核
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditGoods")]
        public ObjectResultEx AuditGoods(RequestEnterpriseGoodsStock Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.AuditGoods(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产品入库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetGoodsStockPage")]
        public ObjectResultEx GetGoodsStockPage(PageParamList<RequestEnterpriseGoodsStock> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGoodsStockPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除仓库中产品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveGoodsStock")]
        public ObjectResultEx RemoveGoodsStock(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveGoodsStock(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑产品仓库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditGoodsStock")]
        public ObjectResultEx EditGoodsStock(RequestEnterpriseGoodsStock Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditGoodsStock(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 绑定二维码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("BindTarget")]
        public ObjectResultEx BindTarget(RequestEnterpriseTagAttach Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.BindTarget(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产品出库分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetGoodsStockAttachPage")]
        public ObjectResultEx GetGoodsStockAttachPage(PageParamList<RequestEnterpriseGoodsStockAttach> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGoodsStockAttachPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 出库编辑
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditStockAttach")]
        public ObjectResultEx EditStockAttach(RequestEnterpriseGoodsStockAttach Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditStockAttach(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除出库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveGoodsStockAttach")]
        public ObjectResultEx RemoveGoodsStockAttach(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveGoodsStockAttach(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取二维码号段
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCodeSerialNo")]
        public ObjectResultEx GetCodeSerialNo(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetCodeSerialNo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取出库批次编号
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetStockOutNoList")]
        public ObjectResultEx GetStockOutNoList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetStockOutNoList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
        #region 品质管理
        #region 原料产品质检
        /// <summary>
        /// 原料质检分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCheckMaterialPage")]
        public ObjectResultEx GetCheckMaterialPage(PageParamList<RequestEnterpriseCheckMaterial> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetCheckMaterialPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑原料质检
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCheckMaterial")]
        public ObjectResultEx EditCheckMaterial(RequestEnterpriseCheckMaterial Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditCheckMaterial(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除原料质检
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveCheckMaterial")]
        public ObjectResultEx RemoveCheckMaterial(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveCheckMaterial(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产品质检分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCheckGoodsPage")]
        public ObjectResultEx GetCheckGoodsPage(PageParamList<RequestEnterpriseCheckGoods> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetCheckGoodsPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑产品质检
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCheckGoods")]
        public ObjectResultEx EditCheckGoods(RequestEnterpriseCheckGoods Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditCheckGoods(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除产品质检
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveCheckGoods")]
        public ObjectResultEx RemoveCheckGoods(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveCheckGoods(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 原料质检列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCheckMaterial")]
        public ObjectResultEx GetCheckMaterial()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetCheckMaterial(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产品质检列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCheckGoodsList")]
        public ObjectResultEx GetCheckGoodsList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetCheckGoodsList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 过期不合格处理
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetInferiorExpriredPage")]
        public ObjectResultEx GetInferiorExpriredPage(PageParamList<RequestEnterpriseInferiorExprired> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetInferiorExpriredPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetInferiorExpriredDetail")]
        public ObjectResultEx GetInferiorExpriredDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetInferiorExpriredDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除过期不合格处理
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveInferiorExprired")]
        public ObjectResultEx RemoveInferiorExprired(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveInferiorExprired(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑过期不合格处理
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditInferiorExprired")]
        public ObjectResultEx EditInferiorExprired(RequestEnterpriseInferiorExprired Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditInferiorExprired(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 召回处理
        /// <summary>
        /// 召回分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetRecoverPage")]
        public ObjectResultEx GetRecoverPage(PageParamList<RequestEnterpriseRecover> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetRecoverPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetRecoverDetail")]
        public ObjectResultEx GetRecoverDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetRecoverDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑召回
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditRecover")]
        public ObjectResultEx EditRecover(RequestEnterpriseRecover Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditRecover(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除召回
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveRecover")]
        public ObjectResultEx RemoveRecover(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveRecover(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
        #region 物流管理
        #region 打包管理
        /// <summary>
        /// 打包分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetPackagePage")]
        public ObjectResultEx GetPackagePage(PageParamList<RequestEnterpriseGoodsPackage> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetPackagePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑打包
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditGoodsPackage")]
        public ObjectResultEx EditGoodsPackage(RequestEnterpriseGoodsPackage Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditGoodsPackage(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 打包详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("GetGoodsPackageDetail")]
        public ObjectResultEx GetGoodsPackageDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGoodsPackageDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除打包
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveGoodsPackge")]
        public ObjectResultEx RemoveGoodsPackge(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveGoodsPackge(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="StockOutNo"></param>
        /// <returns></returns>
        [HttpPost("GetPackageCode")]
        public ObjectResultEx GetPackageCode(SimlpeParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetPackageCode(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 发货收货
        /// <summary>
        /// 发货收货分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetLogisticsPage")]
        public ObjectResultEx GetLogisticsPage(PageParamList<RequestEnterpriseLogistics> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetLogisticsPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑发货
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditLogistics")]
        public ObjectResultEx EditLogistics(RequestEnterpriseLogistics Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditLogistics(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除发货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveLogistics")]
        public ObjectResultEx RemoveLogistics(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveLogistics(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("CheckLogistics")]
        public ObjectResultEx CheckLogistics(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.CheckLogistics(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 进货管理
        /// <summary>
        /// 进货分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetBuyerPage")]
        public ObjectResultEx GetBuyerPage(PageParamList<RequestEnterpriseBuyer> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetBuyerPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑进货
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditBuyer")]
        public ObjectResultEx EditBuyer(RequestEnterpriseBuyer Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditBuyer(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除进货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("RemoveBuyer")]
        public ObjectResultEx RemoveBuyer(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveBuyer(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
    }
}