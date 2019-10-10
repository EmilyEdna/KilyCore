using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Enterprise;
using KilyCore.DataEntity.RequestMapper.Govt;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.Token;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KilyCore.API.Controllers
{
    /// <summary>
    /// 政府APP接口
    /// </summary>
    [Route("api/[controller]")]
    public class GovtAppController : BaseController
    {
        #region 登录退出

        /// <summary>
        /// 监管登录
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public ObjectResultEx Login(RequestGovtInfo Param)
        {
            try
            {
                var GovtAdmin = GovtWebService.GovtLogin(Param);
                string Code = string.Empty;
                if (GovtAdmin != null)
                {
                    CookieInfo cookie = new CookieInfo();
                    VerificationExtension.WriteToken(cookie, GovtAdmin);
                    return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, GovtAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                }
                else
                    return ObjectResultEx.Instance(null, -1, "登录失败或账户冻结", HttpCode.NoAuth);
            }
            catch (Exception)
            {
                return ObjectResultEx.Instance(null, -1, "请检查账号和密码是否正确", HttpCode.FAIL);
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

        #endregion 登录退出

        #region 产品监管

        /// <summary>
        /// 产品分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetProductPage")]
        public ObjectResultEx GetProductPage(PageParamList<RequestEnterpriseGoods> pageParam)
        {
            if (string.IsNullOrEmpty(pageParam.QueryParam.ProductType))//默认食品
                pageParam.QueryParam.ProductType = "食品";
            if (pageParam.QueryParam.ProductType == "食品" || pageParam.QueryParam.ProductType == "农产品")
                return ObjectResultEx.Instance(GovtWebService.GetEdiblePage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
            else
                return ObjectResultEx.Instance(GovtWebService.GetWorkPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 产品监管

        #region 获取今日统计

        [HttpPost("GetAppTodayCount")]
        public ObjectResultEx GetAppTodayCount()
        {
            return ObjectResultEx.Instance(GovtWebService.GetAppTodayCount(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }

        #endregion 获取今日统计
    }
}