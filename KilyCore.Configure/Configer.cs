using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace KilyCore.Configure
{
    public static class Configer
    {
        public static string ConnentionString { get; set; }

        public static string DataProvider { get; set; }

        public static IList<Assembly> Assembly { get; set; }

        public static HttpContext httpContext { get; set; }

        public static string RedisConnectionString { get; set; }

        public static string ApiKey { get; set; }
        public static string ClientIP { get; set; }
    }
}
