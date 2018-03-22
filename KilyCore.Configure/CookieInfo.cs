using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Configure
{
    public class CookieInfo
    {
        public string Token { get; set; }
        public string ApiKey { get; set; }
    }
    public class ResponseCookieInfo
    {
        public static string RSAToKen { get; set; }
        public static string RSAApiKey { get; set; }
    }
}
