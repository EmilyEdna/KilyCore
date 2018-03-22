using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Extension.SessionExtension
{
    /// <summary>
    /// Session管理
    /// </summary>
    public static class SessionExtensionManage
    {
        /// <summary>
        ///添加Session
        /// </summary>
        /// <param name="value"></param>
        public static void SetSession<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 取出Session
        /// </summary>
        /// <returns></returns>
        public static T GetSession<T>(this ISession session, string key)
        {
            return session.GetString(key) == null ? default(T) : JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
    }
}
