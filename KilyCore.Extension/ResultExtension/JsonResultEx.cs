using Microsoft.AspNetCore.Mvc;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.ResultExtension
{
    /// <summary>
    /// JsonResult结果封装
    /// </summary>
    public class JsonResultEx : JsonResult
    {
        public JsonResultEx(object obj) : base(obj)
        {
        }

        public int Flag { get; set; }

        public string Messege { get; set; }

        public HttpCode Code { get; set; }

        public static JsonResultEx Instance(object obj, int flag, string msg, HttpCode code)
        {
            var ex = new JsonResultEx(obj);
            ex.Flag = flag;
            ex.Messege = msg;
            ex.Code = code;
            return ex;
        }
    }
}