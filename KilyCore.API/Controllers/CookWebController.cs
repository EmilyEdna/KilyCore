using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.Configure;
using KilyCore.DataEntity.RequestMapper.Cook;
using KilyCore.DataEntity.RequestMapper.System;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.SessionExtension;
using KilyCore.Extension.Token;
using KilyCore.Service.QueryExtend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilyCore.API.Controllers
{
    [Route("api/[controller]")]
    public class CookWebController : BaseController
    {
        #region 登录注册退出
        /// <summary>
        /// 厨师注册
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("RegistCookAccount")]
        [AllowAnonymous]
        public ObjectResultEx RegistCookAccount(SimlpeParam<RequestCookInfo> Param)
        {
            return ObjectResultEx.Instance(CookWebService.RegistCookAccount(Param.Parameter), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 厨师登录
        /// </summary>
        /// <param name="LoginValidate"></param>
        /// <returns></returns>
        [HttpPost("CookLogin")]
        [AllowAnonymous]
        public ObjectResultEx CookLogin(RequestValidate LoginValidate)
        {
            try
            {
                string Code = HttpContext.Session.GetSession<string>("ValidateCode").Trim();
                var CookAdmin = CookWebService.CookLogin(LoginValidate);
                if (CookAdmin != null && Code.Equals(LoginValidate.ValidateCode.Trim()))
                {
                    CookieInfo cookie = new CookieInfo();
                    VerificationExtension.WriteToken(cookie, CookAdmin);
                    return ObjectResultEx.Instance(new { ResponseCookieInfo.RSAToKen, ResponseCookieInfo.RSAApiKey, ResponseCookieInfo.RSASysKey, CookAdmin }, 1, RetrunMessge.SUCCESS, HttpCode.Success);
                }
                else
                    return ObjectResultEx.Instance(null, -1, "登录失败", HttpCode.NoAuth);
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region 全局菜单
        [HttpPost("GetCookMenu")]
        public ObjectResultEx GetCookMenu()
        {
            return ObjectResultEx.Instance(CookWebService.GetCookMenu(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 账号管理
        /// <summary>
        /// 账号分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        [HttpPost("GetCookVipPage")]
        public ObjectResultEx GetCookVipPage(PageParamList<RequestCookInfo> pageParam)
        {
            return ObjectResultEx.Instance(CookWebService.GetCookVipPage(pageParam), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 账号详情
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("GetCookVipDetail")]
        public ObjectResultEx GetCookVipDetail(SimlpeParam<Guid> Param)
        {
            return ObjectResultEx.Instance(CookWebService.GetCookVipDetail(Param.Id), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCookVip")]
        public ObjectResultEx EditCookVip(RequestCookInfo Param)
        {
            return ObjectResultEx.Instance(CookWebService.EditCookVip(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 开通服务
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("OpenService")]
        public ObjectResultEx OpenService(RequestStayContract Param)
        {
            return ObjectResultEx.Instance(CookWebService.OpenService(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion

        #region 厨师信息
        /// <summary>
        /// 厨师详情
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCookInfoDetail")]
        public ObjectResultEx GetCookInfoDetail()
        {
            return ObjectResultEx.Instance(CookWebService.GetCookInfoDetail(), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        /// <summary>
        /// 编辑厨师信息
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost("EditCookInfo")]
        public ObjectResultEx EditCookInfo(RequestCookInfo Param)
        {
            return ObjectResultEx.Instance(CookWebService.EditCookInfo(Param), 1, RetrunMessge.SUCCESS, HttpCode.Success);
        }
        #endregion
    }
}