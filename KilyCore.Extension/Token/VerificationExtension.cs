using KilyCore.Cache.RedisCache;
using KilyCore.Configure;
using KilyCore.Extension.RSACryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KilyCore.Extension.Token
{
    /// <summary>
    /// 验证拓展
    /// </summary>
    public class VerificationExtension
    {
        /// <summary>
        /// 写入cookie
        /// </summary>
        /// <param name="Cookie"></param>
        public static void WriteToken(CookieInfo Cookie)
        {
            Cookie.Token = Guid.NewGuid().ToString();
            Cookie.ApiKey = RSACryptionExtension.RSAEncrypt(Configer.ApiKey + DateTime.Now.ToShortDateString());
            CacheFactory.Cache().WriteCache(Cookie, Cookie.Token, 2);
            //将加密后的Token和Key回传给客服端
            ResponseCookieInfo.RSAToKen = RSACryptionExtension.RSAEncrypt(Cookie.Token);
            ResponseCookieInfo.RSAApiKey = Cookie.ApiKey;
        }
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <returns></returns>
        public static CookieInfo Verification()
        {
            CookieInfo Storage = (CookieInfo)Configer.HttpContext.Items["Storage"];
            if (Storage != null)
            {
                return Storage;
            }
            if (String.IsNullOrEmpty(Configer.HttpContext.Request.Headers["Token"].ToList().FirstOrDefault()))
                return null;
            String Token = RSACryptionExtension.RSADecrypt(Configer.HttpContext.Request.Headers["Token"].ToString());
            CookieInfo cookie = CacheFactory.Cache().GetCache<CookieInfo>(Token);
            Configer.HttpContext.Items["Storage"] = cookie;
            return cookie;
        }
        /// <summary>
        /// 验证超时
        /// </summary>
        /// <returns></returns>
        public static double VerificationExpriseTime(long TimeSpan)
        {
            return (TimeSpan - (new TimeSpan(DateTime.UtcNow.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0).Ticks)).TotalMilliseconds)) / 60000;
        }
    }
}
