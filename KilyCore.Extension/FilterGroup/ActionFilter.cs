using KilyCore.Cache;
using KilyCore.Configure;
using KilyCore.Extension.ResultExtension;
using KilyCore.Extension.RSACryption;
using KilyCore.Extension.SessionExtension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.FilterGroup
{
    /// <summary>
    /// 请求过滤器
    /// </summary>
    public class ActionFilter : IActionFilter
    {
        /// <summary>
        /// 第四执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //HttpContext httpContext = context.HttpContext;
            //var  headerOrginUrl = httpContext.Request.Headers.GetCommaSeparatedValues("Origin");
            //httpContext.Response.Headers.SetCommaSeparatedValues("Access-Control-Allow-Origin", headerOrginUrl);
            //httpContext.Response.Cookies.Append("Token", ResponseCookieInfo.RSAToken,
            //     new CookieOptions() { HttpOnly = false, Expires = DateTimeOffset.Now.Add(TimeSpan.FromHours(12)), Domain = "localhost", Path = "/", SameSite = SameSiteMode.None }
            //     );//有效期12小时
            //httpContext.Response.Cookies.Append("ApiKey", ResponseCookieInfo.RSAApiKey,
            //    new CookieOptions() { HttpOnly = false, Expires = DateTimeOffset.Now.Add(TimeSpan.FromHours(12)), Domain = "localhost", Path = "/", SameSite = SameSiteMode.None }
            //    );//有效期12小时
        }
        /// <summary>
        /// 第三执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var RequestPath = request.Path.ToString();
            if (RequestPath.Contains("Regist"))
            {
               var PhoneCode = request.Form["Parameter[PhoneCode]"].ToString();
                var SessionCode = CacheFactory.Cache().GetCache<string>("PhoneCode").Trim();
                if (string.IsNullOrEmpty(SessionCode))
                    context.Result = ObjectResultEx.Instance("请输入验证码", 1, RetrunMessge.SUCCESS, HttpCode.FAIL);
                if (!SessionCode.Trim().Equals(PhoneCode))
                    context.Result =ObjectResultEx.Instance("请输入正确的验证码", 1, RetrunMessge.SUCCESS, HttpCode.FAIL);
                CacheFactory.Cache().RemoveCache("PhoneCode");
            }
            if (RequestPath.Contains("EnterpriseWeb/Edit") || RequestPath.Contains("EnterpriseWeb/Remove"))
            {
                if (RequestPath.Contains("Edit"))
                {
                    if (string.IsNullOrEmpty(request.Form["Id"].ToString()))
                    {
                        return;
                    }
                    if (RequestPath.Contains("EditProBatchAttach"))
                        return;
                }
                context.Result = new StatusCodeResult(403);
            }
            if (RequestPath.Contains("RepastWeb/Edit") || RequestPath.Contains("RepastWeb/Remove"))
            {
                if (RequestPath.Contains("Edit"))
                {
                    if (string.IsNullOrEmpty(request.Form["Id"].ToString()))
                    {
                        return;
                    }
                }
                if (RequestPath.Contains("RemoveScan"))
                    return;
                context.Result = new StatusCodeResult(403);
            }
            if (context.Filters.Any(t => (t as AllowAnonymousFilter) != null))
                return;
            if (request.Headers.ContainsKey("ApiKey") && request.Headers.ContainsKey("SysKey"))
            {
                String ApiKey = RSACryptionExtension.RSADecrypt(request.Headers["ApiKey"].FirstOrDefault());
                String SysKey = RSACryptionExtension.RSADecrypt(request.Headers["SysKey"].FirstOrDefault());
                if (ApiKey.Equals(Configer.ApiKey + DateTime.Now.ToShortDateString()) && SystemInfoKey.PrivateKey.Equals(SysKey))
                    return;
            }
            context.Result = new UnauthorizedResult();
        }
    }
}
