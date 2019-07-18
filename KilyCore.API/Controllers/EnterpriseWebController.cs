using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using KilyCore.Cache;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Function;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.SessionExtension;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
    /// <summary>
    /// 企业后台接口
    /// </summary>
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
        public ObjectResultEx GetSellerList(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetSellerList(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 下拉字典类型
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDictionaryList")]
        public ObjectResultEx GetDictionaryList(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDictionaryList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取入住企业的经销商
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetSellerInEnterprise")]
        public ObjectResultEx GetSellerInEnterprise(SimpleParam<String> Param)
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
        public ObjectResultEx GetEnterpriseWebTree(SimpleParam<String> Partam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnterpriseWebTree(Partam.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx GetEnterpriseInfo(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetEnterpriseInfo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取子公司
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetChildAccount")]
        public ObjectResultEx GetChildAccount(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetChildAccount(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("SaveEnterprise")]
        public ObjectResultEx SaveEnterprise(RequestEnterprise Param)
        {
            Param.CodeStar = Param.CodeStar.ToLower();
            return ObjectResultEx.Instance(EnterpriseWebService.SaveEnterprise(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 修改账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveCompanyAccount")]
        public ObjectResultEx SaveCompanyAccount(RequestEnterprise Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveCompanyAccount(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 完善区域
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveCompanyArea")]
        public ObjectResultEx SaveCompanyArea(RequestEnterprise Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveCompanyArea(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
            return ObjectResultEx.Instance(EnterpriseWebService.SaveContract(Param), 5, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("SaveUser")]
        public ObjectResultEx SaveUser(RequestEnterpriseUser Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveUser(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetUserDetail")]
        public ObjectResultEx GetUserDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetUserDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除子账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteUser")]
        public ObjectResultEx DeleteUser(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteUser(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        #region 权限角色
        /// <summary>
        /// 新增账户
        /// </summary>
        /// <returns></returns>
        [HttpPost("SaveRoleAuthor")]
        public ObjectResultEx SaveRoleAuthor(RequestRoleAuthorWeb Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveRoleAuthor(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("DeleteRole")]
        public ObjectResultEx DeleteRole(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 集团账户
        /// <summary>
        /// 集团账户列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetChildInfo")]
        public ObjectResultEx GetChildInfo(PageParamList<RequestEnterprise> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetChildInfo(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑子公司
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveChildInfo")]
        public ObjectResultEx SaveChildInfo(RequestEnterprise Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveChildInfo(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 企业认证
        /// <summary>
        /// 企业认证
        /// </summary>
        /// <returns></returns>
        [HttpPost("SaveEnterpriseIdent")]
        public ObjectResultEx SaveEnterpriseIdent(RequestEnterpriseIdent Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveEnterpriseIdent(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 认证分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetIndentPage")]
        public ObjectResultEx GetIndentPage(PageParamList<RequestEnterpriseIdent> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetIndentPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("DeleteDic")]
        public ObjectResultEx DeleteDic(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteDic(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 字典详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetDicDetail")]
        public ObjectResultEx GetDicDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDicDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveDic")]
        public ObjectResultEx SaveDic(RequestEnterpriseDictionary Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveDic(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("SaveContinued")]
        public ObjectResultEx SaveContinued(RequestEnterpriseContinued Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveContinued(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑升级
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveUpLevel")]
        public ObjectResultEx SaveUpLevel(RequestEnterpriseUpLevel Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveUpLevel(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditContinuedAndLevel")]
        public ObjectResultEx AuditContinuedAndLevel(SimpleParam<Guid> Key, SimpleParam<bool> Param)
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
        public ObjectResultEx GetFileDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetFileDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteFile")]
        public ObjectResultEx DeleteFile(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑文件
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveFile")]
        public ObjectResultEx SaveFile(RequestEnterpriseInsideFile Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveFile(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 视频监控
        /// <summary>
        /// 视频分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetVedioPage")]
        public ObjectResultEx GetVedioPage(PageParamList<RequestEnterpriseVedio> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetVedioPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑视频
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditVedio")]
        public ObjectResultEx EditVedio(RequestEnterpriseVedio Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditVedio(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteVedio")]
        public ObjectResultEx DeleteVedio(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteVedio(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 显示视频
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        [HttpPost("ShowVedio")]
        public ObjectResultEx ShowVedio(SimpleParam<Guid> Param, SimpleParam<bool> flag)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.ShowVedio(Param.Id, flag.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 企业自查
        /// <summary>
        /// 获取企业检查分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTemplateChild")]
        public ObjectResultEx GetTemplateChild(PageParamList<RequestGovtTemplateChild> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTemplateChild(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑企业检查
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditTemplateChild")]
        public ObjectResultEx EditTemplateChild(RequestGovtTemplateChild Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditTemplateChild(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除企业自查
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteTemplate")]
        public ObjectResultEx DeleteTemplate(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteTemplate(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx GetGrowDetail(SimpleParam<Guid> Param)
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveGrow")]
        public ObjectResultEx RemoveGrow(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveGrow(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取批次列表
        /// </summary>
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
        public ObjectResultEx RemovePlanting(SimpleParam<Guid> Param)
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
        public ObjectResultEx RemoveDrug(SimpleParam<Guid> Param)
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
        public ObjectResultEx RemoveEnv(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveEnv(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除从表信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveEnvAttach")]
        public ObjectResultEx RemoveEnvAttach(SimpleParam<Guid> Param)
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
        public ObjectResultEx RemoveNote(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveNote(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 日记详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetNoteDetail")]
        public ObjectResultEx GetNoteDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetNoteDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 日记列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetNoteList")]
        public ObjectResultEx GetNoteList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetNoteList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx RemoveAgeUp(SimpleParam<Guid> Param)
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
        /// 首页号段查询接口
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTagDetailWeb")]
        [AllowAnonymous]
        public ObjectResultEx GetTagDetailWeb(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTagDetailWeb(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 创建空白标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("UpdateEmptyTag")]
        public ObjectResultEx UpdateEmptyTag(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.UpdateEmptyTag(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx RemoveTag(SimpleParam<Guid> Param)
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
        public ObjectResultEx RemoveApplyTag(SimpleParam<Guid> Param)
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
        public ObjectResultEx GetPaymentDetail(SimpleParam<Guid> Param)
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
        public ObjectResultEx AcceptVeinTarget(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.AcceptVeinTarget(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 纹理二维码删除
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveVeinTarget")]
        public ObjectResultEx RemoveVeinTarget(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveVeinTarget(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取标签批次
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTagList")]
        public ObjectResultEx GetTagList(SimpleParam<int> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTagList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取开始标签
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCodeNo")]
        public ObjectResultEx GetCodeNo(SimpleParam<int> Key, SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetCodeNo(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 扫码管理
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetScanCodePage")]
        public ObjectResultEx GetScanCodePage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetScanCodePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 查看二维码绑定信息
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagAttachPage")]
        public ObjectResultEx GetTagAttachPage(PageParamList<RequestEnterpriseTagAttach> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetTagAttachPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除绑定信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteBindTagInfo")]
        public ObjectResultEx DeleteBindTagInfo(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteBindTagInfo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 箱码绑定情况
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetBoxPage")]
        public ObjectResultEx GetBoxPage(PageParamList<RequestEnterpriseBoxing> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetBoxPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        public ObjectResultEx RemoveSeller(SimpleParam<Guid> Param)
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
        public ObjectResultEx GetSellerDetail(SimpleParam<Guid> Param)
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
        public ObjectResultEx RemoveMaterial(SimpleParam<Guid> Param)
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveStock")]
        public ObjectResultEx RemoveStock(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveStock(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 原料质检
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("UpdateMaterStockCheck")]
        public ObjectResultEx UpdateMaterStockCheck(RequestEnterpriseMaterialStock Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.UpdateMaterStockCheck(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveStockAttach")]
        public ObjectResultEx RemoveStockAttach(SimpleParam<Guid> Param)
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
        [HttpPost("SaveDevice")]
        public ObjectResultEx SaveDevice(RequestEnterpriseDevice Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveDevice(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑设备清洗
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveDeviceClean")]
        public ObjectResultEx SaveDeviceClean(RequestEnterpriseDeviceClean Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveDeviceClean(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑设备维护
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveDeviceFix")]
        public ObjectResultEx SaveDeviceFix(RequestEnterpriseDeviceFix Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveDeviceFix(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteDevice")]
        public ObjectResultEx DeleteDevice(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteDevice(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除清洗记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteDeviceClean")]
        public ObjectResultEx DeleteDeviceClean(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteDeviceClean(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除维护记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteDeviceFix")]
        public ObjectResultEx DeleteDeviceFix(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteDeviceFix(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteTarget")]
        public ObjectResultEx DeleteTarget(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteTarget(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("SaveTarget")]
        public ObjectResultEx SaveTarget(RequestEnterpriseTarget Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveTarget(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveSeries")]
        public ObjectResultEx RemoveSeries(SimpleParam<Guid> Param)
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveProBatch")]
        public ObjectResultEx RemoveProBatch(SimpleParam<Guid> Param)
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetProBatchAttachList")]
        public ObjectResultEx GetProBatchAttachList(SimpleParam<Guid> Param)
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
        [HttpPost("SaveFac")]
        public ObjectResultEx SaveFac(RequestEnterpriseFacilities Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveFac(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除设施
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteFac")]
        public ObjectResultEx DeleteFac(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteFac(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        [HttpPost("SaveFacAttach")]
        public ObjectResultEx SaveFacAttach(RequestEnterpriseFacilitiesAttach Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.SaveFacAttach(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除设施附加
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DeleteFacAttach")]
        public ObjectResultEx DeleteFacAttach(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.DeleteFacAttach(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveGoods")]
        public ObjectResultEx RemoveGoods(SimpleParam<Guid> Param)
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetGoodsDetail")]
        public ObjectResultEx GetGoodsDetail(SimpleParam<Guid> Param)
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
        /// 编辑说明书
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditExplanation")]
        public ObjectResultEx EditExplanation(RequestEnterpriseGoodsStock Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditExplanation(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveGoodsStock")]
        public ObjectResultEx RemoveGoodsStock(SimpleParam<Guid> Param)
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
        /// 装箱管理
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditBoxing")]
        public ObjectResultEx EditBoxing(RequestEnterpriseBoxing Param)
        {
            if (Param.BoxCode.EndsWith("\r\n"))
                Param.BoxCode = HttpUtility.UrlDecode(Param.BoxCode).Substring(0, HttpUtility.UrlDecode(Param.BoxCode).LastIndexOf("\r\n"));
            else
                Param.BoxCode = HttpUtility.UrlDecode(Param.BoxCode);
            if (Param.ThingCode.EndsWith("\r\n"))
                Param.ThingCode = HttpUtility.UrlDecode(Param.ThingCode).Substring(0, HttpUtility.UrlDecode(Param.ThingCode).LastIndexOf("\r\n"));
            else
                Param.ThingCode = HttpUtility.UrlDecode(Param.ThingCode);
            return ObjectResultEx.Instance(EnterpriseWebService.EditBoxing(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 绑定二维码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("BindTarget")]
        public ObjectResultEx BindTarget(RequestEnterpriseTagAttach Param)
        {
            var data = EnterpriseWebService.BindTarget(Param);
            if (!data.Contains("新增"))
                return ObjectResultEx.Instance(data, 1, RetrunMessge.SUCCESS, HttpCode.FAIL);
            else
                return ObjectResultEx.Instance(data, 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
            if (Param.BoxCodeNo.EndsWith("\r\n"))
                Param.BoxCodeNo = HttpUtility.UrlDecode(Param.BoxCodeNo).Substring(0, HttpUtility.UrlDecode(Param.BoxCodeNo).LastIndexOf("\r\n"));
            else
                Param.BoxCodeNo = HttpUtility.UrlDecode(Param.BoxCodeNo);
            if (Param.SourceCodeNo.EndsWith("\r\n"))
                Param.SourceCodeNo = HttpUtility.UrlDecode(Param.SourceCodeNo).Substring(0, HttpUtility.UrlDecode(Param.SourceCodeNo).LastIndexOf("\r\n"));
            else
                Param.SourceCodeNo = HttpUtility.UrlDecode(Param.SourceCodeNo);
            return ObjectResultEx.Instance(EnterpriseWebService.EditStockAttach(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除出库
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveGoodsStockAttach")]
        public ObjectResultEx RemoveGoodsStockAttach(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveGoodsStockAttach(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <summary>
        /// 添加产品检测
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("UpdateStockCheck")]
        public ObjectResultEx UpdateStockCheck(RequestEnterpriseGoodsStock Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.UpdateStockCheck(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 仓库类型
        /// <summary>
        /// 仓库类型分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetStockTypePage")]
        public ObjectResultEx GetStockTypePage(PageParamList<RequestEnterpriseStockType> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetStockTypePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 仓库类型列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetStockTypeList")]
        public ObjectResultEx GetStockTypeList(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetStockTypeList(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除仓库类型
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveStockType")]
        public ObjectResultEx RemoveStockType(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveStockType(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑仓库类型
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditStockType")]
        public ObjectResultEx EditStockType(RequestEnterpriseStockType Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditStockType(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveCheckMaterial")]
        public ObjectResultEx RemoveCheckMaterial(SimpleParam<Guid> Param)
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveCheckGoods")]
        public ObjectResultEx RemoveCheckGoods(SimpleParam<Guid> Param)
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
        /// 过期数据
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetExpiredPage")]
        public ObjectResultEx GetExpiredPage(PageParamList<Object> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetExpiredPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetInferiorExpriredDetail")]
        public ObjectResultEx GetInferiorExpriredDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetInferiorExpriredDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除过期不合格处理
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveInferiorExprired")]
        public ObjectResultEx RemoveInferiorExprired(SimpleParam<Guid> Param)
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetRecoverDetail")]
        public ObjectResultEx GetRecoverDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetRecoverDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑召回
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("SaveRecover")]
        public ObjectResultEx SaveRecover(RequestEnterpriseRecover Param)
        {
            if (Param.Id == Guid.Empty)
            {
                var SessionCode = CacheFactory.Cache().GetCache<string>("PhoneCode").Trim();
                if (!string.IsNullOrEmpty(SessionCode))
                {
                    var Result = SessionCode.Equals(Param.Code) ? EnterpriseWebService.SaveRecover(Param) : "验证码错误!";
                    return ObjectResultEx.Instance(Result, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                }
                else
                {
                    return ObjectResultEx.Instance("请输入手机验证码", 1, RetrunMessge.SUCCESS, HttpCode.Success);
                }
            }
            else
            {
                return ObjectResultEx.Instance(EnterpriseWebService.SaveRecover(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
            }
        }
        /// <summary>
        /// 删除召回
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveRecover")]
        public ObjectResultEx RemoveRecover(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveRecover(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
        #region 物流管理
        #region 装车管理
        /// <summary>
        /// 装车分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetPackagePage")]
        public ObjectResultEx GetPackagePage(PageParamList<RequestEnterpriseGoodsPackage> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetPackagePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑装车
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditGoodsPackage")]
        public ObjectResultEx EditGoodsPackage(RequestEnterpriseGoodsPackage Param)
        {
            if (Param.BoxCode.EndsWith("\r\n"))
                Param.BoxCode = HttpUtility.UrlDecode(Param.BoxCode).Substring(0, HttpUtility.UrlDecode(Param.BoxCode).LastIndexOf("\r\n"));
            else
                Param.BoxCode = HttpUtility.UrlDecode(Param.BoxCode);
            return ObjectResultEx.Instance(EnterpriseWebService.EditGoodsPackage(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 装车详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetGoodsPackageDetail")]
        [AllowAnonymous]
        public ObjectResultEx GetGoodsPackageDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetGoodsPackageDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除装车
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveGoodsPackge")]
        public ObjectResultEx RemoveGoodsPackge(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveGoodsPackge(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 装车批次号
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetPackagesList")]
        public ObjectResultEx GetPackagesList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetPackagesList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// 串货分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetLogisticsErrorPage")]
        public ObjectResultEx GetLogisticsErrorPage(PageParamList<RequestEnterpriseScanCodeInfo> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetLogisticsErrorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetReceipt")]
        public ObjectResultEx GetReceipt()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetReceipt(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑发货
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditLogistics")]
        public ObjectResultEx EditLogistics(RequestEnterpriseLogistics Param)
        {
            if (Param.BoxCode.EndsWith("\r\n"))
                Param.BoxCode = HttpUtility.UrlDecode(Param.BoxCode).Substring(0, HttpUtility.UrlDecode(Param.BoxCode).LastIndexOf("\r\n"));
            else
                Param.BoxCode = HttpUtility.UrlDecode(Param.BoxCode);
            if (Param.OneCode.EndsWith("\r\n"))
                Param.OneCode = HttpUtility.UrlDecode(Param.OneCode).Substring(0, HttpUtility.UrlDecode(Param.OneCode).LastIndexOf("\r\n"));
            else
                Param.OneCode = HttpUtility.UrlDecode(Param.OneCode);
            return ObjectResultEx.Instance(EnterpriseWebService.EditLogistics(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除发货
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveLogistics")]
        public ObjectResultEx RemoveLogistics(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveLogistics(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
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
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveBuyer")]
        public ObjectResultEx RemoveBuyer(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.RemoveBuyer(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 进货列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetBuyerList")]
        public ObjectResultEx GetBuyerList()
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetBuyerList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #endregion
        #region 支付模块
        /// <summary>
        /// 版本续费和升级使用支付宝支付
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("AliPay")]
        public ObjectResultEx AliPay(SimpleParam<int> Key, SimpleParam<int?> Value)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.AliPay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 版本续费和升级使用微信支付
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [HttpPost("WxPay")]
        public ObjectResultEx WxPay(SimpleParam<int> Key, SimpleParam<int?> Value)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.WxPay(Key.Id, Value.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 查询微信支付
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("WxQueryPay")]
        public ObjectResultEx WxQueryPay(RequestContractTemp Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.WxQueryPay(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 导出Excel
        /// <summary>
        /// 原料入库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExportMaterialInStockFile")]
        [AllowAnonymous]
        public ObjectResultEx ExportMaterialInStockFile(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.ExportMaterialInStockFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 原料出库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExportMaterialOutStockFile")]
        [AllowAnonymous]
        public ObjectResultEx ExportMaterialOutStockFile(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.ExportMaterialOutStockFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产品入库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExportProInStockFile")]
        [AllowAnonymous]
        public ObjectResultEx ExportProInStockFile(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.ExportProInStockFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产品出库Excel导出
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("ExportProOutStockFile")]
        [AllowAnonymous]
        public ObjectResultEx ExportProOutStockFile(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.ExportProOutStockFile(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 数据统计
        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDataCount")]
        public ObjectResultEx GetDataCount(SimpleParam<Guid?> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetDataCount(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产量统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetPieCount")]
        public ObjectResultEx GetPieCount(SimpleParam<Guid?> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetPieCount(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 批次统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetPieCountBatch")]
        public ObjectResultEx GetPieCountBatch(SimpleParam<Guid?> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetPieCountBatch(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 手机扫描页面
        /// <summary>
        /// 一企一码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetScanCompanyFirst")]
        [AllowAnonymous]
        public ObjectResultEx GetScanCompanyFirst(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetScanCompanyFirst(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 一品一码一物一码
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetScanCodeInfo")]
        [AllowAnonymous]
        public ObjectResultEx GetScanCodeInfo(SimpleParam<Guid?> Key, SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetScanCodeInfo(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 新增扫码记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditScanInfo")]
        [AllowAnonymous]
        public ObjectResultEx EditScanInfo(RequestEnterpriseScanCodeInfo Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.EditScanInfo(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 一箱一码
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetScanBoxInfo")]
        [AllowAnonymous]
        public ObjectResultEx GetScanBoxInfo(SimpleParam<Guid?> Key, SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetScanBoxInfo(Key.Id, Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 发货二维码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetScanSendInfo")]
        [AllowAnonymous]
        public ObjectResultEx GetScanSendInfo(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetScanSendInfo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("CheckLogistics")]
        [AllowAnonymous]
        public ObjectResultEx CheckLogistics(RequestEnterpriseLogistics Param)
        {
            if (string.IsNullOrEmpty(Param.PackageNo))
                return ObjectResultEx.Instance(null, -1, "验证码不能为空", HttpCode.FAIL);
            string Code = CacheFactory.Cache().GetCache<string>("PhoneCode").Trim(); 
            if (!Code.Equals(Param.PackageNo))
                return ObjectResultEx.Instance(null, -1, "请检查验证码", HttpCode.FAIL);
            return ObjectResultEx.Instance(EnterpriseWebService.CheckLogistics(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 装车信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetScanPackageInfo")]
        [AllowAnonymous]
        public ObjectResultEx GetScanPackageInfo(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseWebService.GetScanPackageInfo(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}