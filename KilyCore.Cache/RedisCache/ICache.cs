using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.Cache.RedisCache
{
    public interface ICache
    {
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <returns></returns>
        T GetCache<T>(String CacheKey);
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="CacheKey"></param>
        /// <param name="hours">缓存时间</param>
        void WriteCache<T>(T obj, String CacheKey, int hours);
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="CacheKey"></param>
        /// <param name="Minutes"></param>
        void WriteCaches<T>(T obj, String CacheKey, int Minutes);
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="CacheKey"></param>
        void WriteCache<T>(T obj, String CacheKey);
        /// <summary>
        /// 根据key删除指定缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        void RemoveCache(string CacheKey);
        /// <summary>
        /// 删除所有缓存
        /// </summary>
        void RemoveCache();
        /// 删除所有缓存
        /// </summary>
        Task RemoveCacheAsync();
    }
}
