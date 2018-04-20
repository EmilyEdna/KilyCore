using KilyCore.Cache;
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
            if (String.IsNullOrEmpty(Configer.HttpContext.Request.Headers["Token"].ToList().FirstOrDefault()))
                return null;
            String Token = RSACryptionExtension.RSADecrypt(Configer.HttpContext.Request.Headers["Token"].ToString());
            return CacheFactory.Cache().GetCache<CookieInfo>(Token);
        }
        public static string LoginOut()
        {
            String Token = RSACryptionExtension.RSADecrypt(Configer.HttpContext.Request.Headers["Token"].ToString());
            CacheFactory.Cache().RemoveCache(Token);
            CacheFactory.Cache().RemoveCache(Configer.ClientIP);
            return "退出成功!";
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
