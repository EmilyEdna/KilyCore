using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
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
        public static void SetSession<T>(this ISession session, String key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 取出Session
        /// </summary>
        /// <returns></returns>
        public static T GetSession<T>(this ISession session, String key)
        {
            return session.GetString(key) == null ? default(T) : JsonConvert.DeserializeObject<T>(session.GetString(key));
        }

        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="Key"></param>
        public static void DeleteSession(this ISession session, String Key)
        {
            session.Remove(Key);
        }

        /// <summary>
        /// 清空Session
        /// </summary>
        /// <param name="session"></param>
        public static void ClearSession(this ISession session)
        {
            session.Clear();
        }
    }
}