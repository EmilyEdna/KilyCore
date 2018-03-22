using KilyCore.Extension.NlogExtension;
using KilyCore.Extension.ResultExtension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KilyCore.Extension.FilterGroup
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 第五执行
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            string path = context.Exception.Source;
            string methodName = context.Exception.TargetSite.Name;
            string parameter = string.Empty;
            string message = context.Exception.Message;
            context.Exception.TargetSite.GetParameters().ToList().ForEach(t => {
                parameter += "【" + t.Name + "】";
            });
            LogFactoryExtension.WriteFatal(path, methodName, parameter, message);
            context.Result = ObjectResultEx.Instance("", -1, RetrunMessge.EXCEPTION, HttpCode.SystemEx);
        }
    }
}
