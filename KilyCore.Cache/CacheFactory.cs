using KilyCore.Cache.MongoCache;
using KilyCore.Cache.RedisCache;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.Cache
{
    /// <summary>
    /// 缓存工厂
    /// </summary>
    public class CacheFactory
    {
        /// <summary>
        /// Redis缓存
        /// </summary>
        /// <returns></returns>
        public static ICache Cache()
        {
            return new RedisCache.Cache();
        }
        /// <summary>
        /// Mongodb缓存
        /// </summary>
        /// <returns></returns>
        public static IMongoDbCache Caches() {
            return new MongoDbCache();
        }
    }
}
