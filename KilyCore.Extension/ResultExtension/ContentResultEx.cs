using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.ResultExtension
{
    /// <summary>
    /// ContentResult结果封装
    /// </summary>
    public class ContentResultEx: ContentResult
    {
        public ContentResultEx(object obj)
        {
            Value = obj;
        }
        public object Value { get; set; }

        public int Flag { get; set; }

        public string Messege { get; set; }

        public HttpCode Code { get; set; }
        public static ContentResultEx Instance(object obj, int flag, string msg, HttpCode code)
        {
            var ex = new ContentResultEx(obj);
            ex.Flag = flag;
            ex.Messege = msg;
            ex.Code = code;
            return ex;
        }
    }
}
