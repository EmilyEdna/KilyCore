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
        /// 以后会增加其他缓存
        /// </summary>
        /// <returns></returns>
        public static ICache Cache()
        {
            return new Cache();
        }
    }
}
