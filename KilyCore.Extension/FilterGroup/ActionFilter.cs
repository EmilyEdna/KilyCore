using KilyCore.Configure;
using KilyCore.Extension.RSACryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

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
            //客服端IP
            Configer.ClientIP = context.HttpContext.Connection.RemoteIpAddress.ToString();
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
            if (context.Filters.Any(t => (t as AllowAnonymousFilter) != null))
                return;
            if (request.Headers.ContainsKey("ApiKey"))
            {
                String ApiKey = RSACryptionExtension.RSADecrypt(request.Headers["ApiKey"].FirstOrDefault());
                if (ApiKey.Equals(Configer.ApiKey + DateTime.Now.ToShortDateString()))
                    return;
            }
            context.Result = new UnauthorizedResult();
        }
    }
}
