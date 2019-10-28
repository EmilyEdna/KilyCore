using KilyCore.Extension.RSACryption;
using KilyCore.Extension.Token;
using Microsoft.AspNetCore.Mvc;
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
    /// 资源过滤器
    /// </summary>
    public class ResourceFilter : IResourceFilter
    {
        /// <summary>
        /// 第八执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        /// <summary>
        /// 第二执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //判断请求超时
            var request = context.HttpContext.Request;
            if (request.Method.ToLower() == "get")
            {
                try
                {
                    if (request.Path.Value.Contains("EmilyCmd"))
                        context.Result = new JsonResult(MD5CryptionExtension.Command());
                    if(request.Path.Value.Contains("EmilyString"))
                        context.Result = new JsonResult(MD5CryptionExtension.GetCommand());
                    long timespan = 0;
                    if (!string.IsNullOrEmpty(request.Headers["TimeSpan"].FirstOrDefault()))
                        timespan = long.Parse(request.Headers["TimeSpan"].FirstOrDefault());
                    else
                        timespan = long.Parse(request.Query.Where(t => t.Key.Contains("TimeSpan")).Select(t => t.Value).FirstOrDefault().ToString());
                    if (VerificationExtension.VerificationExpriseTime(timespan) > 10 || VerificationExtension.VerificationExpriseTime(timespan) < -10)
                        context.Result = new JsonResult("请求超时");
                }
                catch (Exception)
                {
                    context.Result = new JsonResult("请求超时");
                }
            }
            else
            {
                try
                {
                    long timespan = 0;
                    if (!string.IsNullOrEmpty(request.Headers["TimeSpan"].FirstOrDefault()))
                        timespan = long.Parse(request.Headers["TimeSpan"].FirstOrDefault());
                    else
                        timespan = long.Parse(request.Form.Where(t => t.Key.Contains("TimeSpan")).Select(t => t.Value).FirstOrDefault().ToString());
                    if (VerificationExtension.VerificationExpriseTime(timespan) > 10 || VerificationExtension.VerificationExpriseTime(timespan) < -10)
                        context.Result = new JsonResult("请求超时");
                }
                catch (Exception)
                {
                    context.Result = new JsonResult("请求超时");
                }
            }
        }
    }
}