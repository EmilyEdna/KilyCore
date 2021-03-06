﻿using System;
using System.Threading.Tasks;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.Cache.RedisCache
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class Cache : ICache
    {
        /// <summary>
        /// 缓存中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public T GetCache<T>(string CacheKey)
        {
            return RedisCache.StringGet<T>(CacheKey);
        }
        /// <summary>
        /// 删除某个缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        public void RemoveCache(string CacheKey)
        {
            RedisCache.KeyDelete(CacheKey);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public void RemoveCache()
        {
            RedisCache.DeleteRedisDataBase();
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public async Task RemoveCacheAsync()
        {
            await RedisCache.DeleteRedisDataBaseAsync();
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="CacheKey"></param>
        /// <param name="hours"></param>
        public void WriteCache<T>(T obj, string CacheKey, int hours)
        {
            RedisCache.StringSet<T>(CacheKey, obj, (DateTime.Now.AddHours(hours) - DateTime.Now));
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="CacheKey"></param>
        /// <param name="Minutes"></param>
        public void WriteCaches<T>(T obj, string CacheKey, int Minutes)
        {
            RedisCache.StringSet<T>(CacheKey, obj, (DateTime.Now.AddMinutes(Minutes) - DateTime.Now));
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="CacheKey"></param>
        public void WriteCache<T>(T obj, string CacheKey)
        {
            RedisCache.StringSet<T>(CacheKey, obj);
        }
    }
}
