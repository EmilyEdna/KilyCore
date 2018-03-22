using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

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
