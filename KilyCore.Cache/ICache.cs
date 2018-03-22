﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Cache
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
    }
}
