using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.Cache;
using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Repast;
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
    /// <summary>
    /// 系统餐饮接口
    /// </summary>
    [Route("api/[controller]")]
    public class RepastController : BaseController
    {
        #region  商家资料
        /// <summary>
        /// 商家分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantPage")]
        public ObjectResultEx GetMerchantPage(PageParamList<RequestMerchant> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetMerchantPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取商家详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMerchantDetail")]
        public ObjectResultEx GetMerchantDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.GetMerchantDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 审核商家
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditMerchant")]
        public ObjectResultEx AuditMerchant(RequestAudit Param)
        {
            return ObjectResultEx.Instance(RepastService.AuditMerchant(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 餐饮菜单
        /// <summary>
        /// 餐饮菜单分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDiningMenuPage")]
        public ObjectResultEx GetDiningMenuPage(PageParamList<RequestRepastMenu> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningMenuPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditDiningMenu")]
        public ObjectResultEx EditDiningMenu(RequestRepastMenu Param)
        {
            return ObjectResultEx.Instance(RepastService.EditDiningMenu(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveMenu")]
        public ObjectResultEx RemoveMenu(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.RemoveMenu(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 菜单详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetDiningMenuDetail")]
        public ObjectResultEx GetDiningMenuDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningMenuDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 显示父节菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddDiningParentMenu")]
        public ObjectResultEx AddDiningParentMenu()
        {
            return ObjectResultEx.Instance(RepastService.AddDiningParentMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 权限菜单树
        /// <summary>
        /// 权限菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDiningTree")]
        public ObjectResultEx GetDiningTree(SimpleParam<String> Param)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningTree(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 角色管理
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditRole")]
        public ObjectResultEx EditRole(RequestRepastRoleAuthor Param)
        {
            return ObjectResultEx.Instance(RepastService.EditRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetMerchantAuthorPage")]
        public ObjectResultEx GetMerchantAuthorPage(PageParamList<RequestRepastRoleAuthor> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetMerchantAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 权限分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("WatchRolePage")]
        public ObjectResultEx WatchRolePage(PageParamList<RequestRepastRoleAuthor> pageParam) {
            return ObjectResultEx.Instance(RepastService.WatchRolePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RemoveAuthorRole")]
        public ObjectResultEx RemoveAuthorRole(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.RemoveAuthorRole(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorPage")]
        public ObjectResultEx GetRoleAuthorPage(PageParamList<RequestRepastRoleAuthor> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetRoleAuthorPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 权限下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRoleAuthorList")]
        public ObjectResultEx GetRoleAuthorList()
        {
            return ObjectResultEx.Instance(RepastService.GetRoleAuthorList(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("DistributionRole")]
        public ObjectResultEx DistributionRole(RequestRepastRoleAuthor Param)
        {
            return ObjectResultEx.Instance(RepastService.DistributionRole(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 权限详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetRepastRoleAuthorDetail")]
        public ObjectResultEx GetRepastRoleAuthorDetail(SimpleParam<Guid> Param) {
            return ObjectResultEx.Instance(RepastService.GetRepastRoleAuthorDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 认证审核
        /// <summary>
        /// 商家认证分页列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetDiningIdentPage")]
        public ObjectResultEx GetDiningIdentPage(PageParamList<RequestRepastIdent> pageParam)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningIdentPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 获取认证审核详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetDiningIdentDetail")]
        public ObjectResultEx GetDiningIdentDetail(SimpleParam<Guid> Param)
        {
            return ObjectResultEx.Instance(RepastService.GetDiningIdentDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 认证审核
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditIdent")]
        public ObjectResultEx AuditIdent(RequestAudit Param)
        {
            return ObjectResultEx.Instance(RepastService.AuditIdent(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 认证缴费
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("AuditPayment")]
        public ObjectResultEx AuditPayment(RequestPayment Param)
        {
            return ObjectResultEx.Instance(RepastService.AuditPayment(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
        #region 登录注册退出
        /// <summary>
        /// 餐饮企业注册
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("RegistRepastAccount")]
        public ObjectResultEx RegistRepastAccount(SimpleParam<RequestMerchant> Param)
        {
            return ObjectResultEx.Instance(RepastService.RegistRepastAccount(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 餐饮企业登录
        /// </summary>
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("MerchantLogin")]
        public ObjectResultEx MerchantLogin(RequestValidate LoginValidate)
        {
            try
            {
                var RepAdmin = RepastService.MerchantLogin(LoginValidate);
                string Code = string.Empty;
                if (!LoginValidate.IsApp)
                {
                    
                    Code = CacheFactory.Cache().GetCache<string>("ValidateCode").Trim();
                    if (RepAdmin != null && Code.ToUpper().Equals(LoginValidate.ValidateCode.Trim().ToUpper()))
                    {
                        CookieInfo cookie = new CookieInfo();
                        VerificationExtension.WriteToken(cookie, RepAdmin);
                        return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, RepAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                    }
                    else if (!Code.ToUpper().Equals(LoginValidate.ValidateCode.Trim().ToUpper()))
                        return ObjectResultEx.Instance(null, -1, "验证码错误", HttpCode.NoAuth);
                    else
                        return ObjectResultEx.Instance(null, -1, "登录失败或账户冻结", HttpCode.NoAuth);
                }
                else {
                    if (RepAdmin != null)
                    {
                        CookieInfo cookie = new CookieInfo();
                        VerificationExtension.WriteToken(cookie, RepAdmin);
                        return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, RepAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                    }
                    else if (RepAdmin == null)
                        return ObjectResultEx.Instance(null, -1, "请检查用户名和密码是否正确", HttpCode.NoAuth);
                    else if (!Code.ToUpper().Equals(LoginValidate.ValidateCode.Trim().ToUpper()))
                        return ObjectResultEx.Instance(null, -1, "验证码错误", HttpCode.NoAuth);
                    else
                        return ObjectResultEx.Instance(null, -1, "登录失败或账户冻结", HttpCode.NoAuth);
                }
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
    }
}