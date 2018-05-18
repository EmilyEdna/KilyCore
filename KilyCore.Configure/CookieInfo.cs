using System;
using System.Collections.Generic;
using System.Text;

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
    public class SystemFlag
    {
        public static int EnterpriseFlag { get; set; }
    }
}
