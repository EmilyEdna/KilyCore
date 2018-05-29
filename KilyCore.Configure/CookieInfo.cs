using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.Configure
{
    public class CookieInfo
    {
        public string Token { get; set; }
        public string ApiKey { get; set; }
        public string SysKey { get; set; }
    }
    public class SystemInfoKey
    {
        public static string PrivateKey { get; set; }
    }
    public class ResponseCookieInfo
    {
        public static string RSAToKen { get; set; }
        public static string RSAApiKey { get; set; }
        public static string RSASysKey { get; set; }
    }
}
