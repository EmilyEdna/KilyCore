using System;
using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.SessionExtension;
using KilyCore.Extension.Token;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class EnterpriseController : BaseController
    {
        #region 集团菜单
        /// <summary>
        /// 修改新增菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("EditEnterpriseMenu")]
        public ObjectResultEx EditEnterpriseMenu(RequestEnterpriseMenu Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.EditEnterpriseMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("GetEnterpriseMenuDetail")]
        public ObjectResultEx GetEnterpriseMenuDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetEnterpriseMenuDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 父节菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddEnterpriseParentMenu")]
        public ObjectResultEx AddEnterpriseParentMenu()
        {
            return ObjectResultEx.Instance(EnterpriseService.AddEnterpriseParentMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 企业菜单分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetEnterpriseMenuPage")]
        public ObjectResultEx GetEnterpriseMenuPage(PageParamList<RequestEnterpriseMenu> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetEnterpriseMenuPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除企业菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("RemoveEnterpriseMenu")]
        public ObjectResultEx RemoveEnterpriseMenu(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.RemoveEnterpriseMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色权限菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetEnterpriseTree")]
        public ObjectResultEx GetEnterpriseTree()
        {
            return ObjectResultEx.Instance(EnterpriseService.GetEnterpriseTree(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 集团角色
        /// <summary>
        /// 集团角色分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyRoleAuthorPage")]
        public ObjectResultEx GetCompanyRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetCompanyRoleAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("WatchRolePage")]
        public ObjectResultEx WatchRolePage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.WatchRolePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑集团角色菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditEnterpriseRoleAuthor")]
        public ObjectResultEx EditEnterpriseRoleAuthor(RequestEnterpriseRoleAuthor Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.EditEnterpriseRoleAuthor(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色列表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorPage")]
        public ObjectResultEx GetRoleAuthorPage(PageParamList<RequestEnterpriseRoleAuthor> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetRoleAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveEnterpriseRoleAuthor")]
        public ObjectResultEx RemoveEnterpriseRoleAuthor(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.RemoveEnterpriseRoleAuthor(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorList")]
        public ObjectResultEx GetRoleAuthorList()
        {
            return ObjectResultEx.Instance(EnterpriseService.GetRoleAuthorList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DistributionRole")]
        public ObjectResultEx DistributionRole(RequestEnterpriseRoleAuthor Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.DistributionRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region  资料审核
        /// <summary>
        /// 企业分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyPage")]
        public ObjectResultEx GetCompanyPage(PageParamList<RequestEnterprise> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetCompanyPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取企业详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCompanyDetail")]
        public ObjectResultEx GetCompanyDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetCompanyDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核企业
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditCompany")]
        public ObjectResultEx AuditCompany(RequestAudit Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.AuditCompany(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 认证审核
        /// <summary>
        /// 企业认证分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyIdentPage")]
        public ObjectResultEx GetCompanyIdentPage(PageParamList<RequestEnterpriseIdent> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetCompanyIdentPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取认证详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyIdentDetail")]
        public ObjectResultEx GetCompanyIdentDetail(RequestEnterpriseIdent Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetCompanyIdentDetail(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 认证审核
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditIdent")]
        public ObjectResultEx AuditIdent(RequestAudit Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.AuditIdent(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 认证缴费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditPayment")]
        public ObjectResultEx AuditPayment(RequestPayment Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.AuditPayment(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 登录注册退出
        /// <summary>
        /// 企业注册
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("RegistCompanyAccount")]
        public ObjectResultEx RegistCompanyAccount(SimpleParam<RequestEnterprise> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.RegistCompanyAccount(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 集团登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("EnterpriseLogin")]
        [AllowAnonymous]
        public ObjectResultEx EnterpriseLogin(RequestValidate LoginValidate)
        {
            try
            {
                string Code = HttpContext.Session.GetSession<string>("ValidateCode").Trim();
                var ComAdmin = EnterpriseService.EnterpriseLogin(LoginValidate);
                if (ComAdmin != null && Code.ToUpper().Equals(LoginValidate.ValidateCode.Trim().ToUpper()))
                {
                    CookieInfo cookie = new CookieInfo();
                    VerificationExtension.WriteToken(cookie, ComAdmin);
                    return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, ComAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                }
                else
                    return ObjectResultEx.Instance(null, -1, "登录失败", HttpCode.NoAuth);
            }
            catch (Exception)
            {
                return ObjectResultEx.Instance(null, -1, "请输入验证码", HttpCode.FAIL);
            }
        }
        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        [HttpPost("LoginOut")]
        public ObjectResultEx LoginOut()
        {
            return ObjectResultEx.Instance(VerificationExtension.LoginOut(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 标签管理
        /// <summary>
        /// 二维码审核的标签分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetTagAuditPage")]
        public ObjectResultEx GetTagAuditPage(PageParamList<RequestEnterpriseApply> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetTagAuditPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核标签
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditCode")]
        public ObjectResultEx AuditCode(RequestAudit Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.AuditCode(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取二维码审核记录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetTagAuditDetail")]
        public ObjectResultEx GetTagAuditDetail(RequestAudit Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetTagAuditDetail(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region  产品审核
        /// <summary>
        /// 产品审核列表分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetWaitAuditGoodPage")]
        public ObjectResultEx GetWaitAuditGoodPage(PageParamList<RequestEnterpriseGoodsStock> pageParam)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetWaitAuditGoodPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核产品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("AuditGoodSuccess")]
        public ObjectResultEx AuditGoodSuccess(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.AuditGoodSuccess(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 产品详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetAuditProductDetail")]
        public ObjectResultEx GetAuditProductDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(EnterpriseService.GetAuditProductDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}