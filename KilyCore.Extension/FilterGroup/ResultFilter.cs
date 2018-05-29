using KilyCore.Extension.ResultExtension;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.FilterGroup
{
    /// <summary>
    /// 结果过滤器
    /// </summary>
    public class ResultFilter : IResultFilter
    {
        /// <summary>
        /// 第七执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
        /// <summary>
        /// 第六执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResultEx)
            {
                var res = context.Result as ObjectResultEx;
                context.Result = new ObjectResultEx(new { HttpCode = res.Code, msg = res.Messege, flag = res.Flag, data = res.Value });
            }
            if (context.Result is EmptyResultEx)
            {
                var res = context.Result as EmptyResultEx;
                context.Result = new EmptyResultEx(new { HttpCode = res.Code, msg = res.Messege, flag = res.Flag, data = res.Value });
            }
            if (context.Result is ContentResultEx)
            {
                var res = context.Result as ContentResultEx;
                context.Result = new ContentResultEx(new { HttpCode = res.Code, msg = res.Messege, flag = res.Flag, data = res.Value });
            }
            if (context.Result is StatusCodeResultEx)
            {
                var res = context.Result as StatusCodeResultEx;
                context.Result = new StatusCodeResultEx(new { HttpCode = res.Code, msg = res.Messege, flag = res.Flag, data = res.Value });
            }
            if (context.Result is JsonResultEx)
            {
                var res = context.Result as JsonResultEx;
                context.Result = new JsonResultEx(new { HttpCode = res.Code, msg = res.Messege, flag = res.Flag, data = res.Value });
            }
        }
    }
}
