using Microsoft.AspNetCore.Mvc;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.ResultExtension
{
    /// <summary>
    /// EmptyResult结果封装
    /// </summary>
    public class EmptyResultEx : EmptyResult
    {
        public EmptyResultEx(object obj)
        {
            Value = obj;
        }

        public object Value { get; set; }

        public int Flag { get; set; }

        public string Messege { get; set; }

        public HttpCode Code { get; set; }

        public static EmptyResultEx Instance(object obj, int flag, string msg, HttpCode code)
        {
            var ex = new EmptyResultEx(obj);
            ex.Flag = flag;
            ex.Messege = msg;
            ex.Code = code;
            return ex;
        }
    }
}