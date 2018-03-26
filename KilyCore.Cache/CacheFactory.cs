using KilyCore.Cache.MongoCache;
using KilyCore.Cache.RedisCache;
using System;
using System.Collections.Generic;
using System.Text;

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
