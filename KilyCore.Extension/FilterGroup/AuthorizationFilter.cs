using KilyCore.Configure;
using KilyCore.Extension.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KilyCore.Extension.FilterGroup
{
    /// <summary>
    /// 认证过滤器
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// 第一执行
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Configer.HttpContext = context.HttpContext;
            if (context.Filters.Any(t => (t as AllowAnonymousFilter) != null))
                return;
            //验证用户是否登录
            if (VerificationExtension.Verification() != null)
                return;
            context.Result = new UnauthorizedResult();
        }
    }
}
