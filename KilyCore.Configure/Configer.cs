using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.Configure
{
    /// <summary>
    /// 系统信息配置
    /// </summary>
    public static class Configer
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string ConnentionString { get; set; }
        /// <summary>
        /// 数据库链接方式 SQLSERVER，MYSQL暂不支持ORACLE
        /// </summary>
        public static string DataProvider { get; set; }
        /// <summary>
        /// 系统程序集
        /// </summary>
        public static IList<Assembly> Assembly { get; set; }
        /// <summary>
        /// HTTP请求上下文
        /// </summary>
        public static HttpContext HttpContext { get; set; }
        /// <summary>
        /// Redis缓存链接字符串
        /// </summary>
        public static string RedisConnectionString { get; set; }
        /// <summary>
        /// MongoDB链接字符串
        /// </summary>
        public static string MongoDBConnectionString { get; set; }
        /// <summary>
        /// MongoDB数据库名称
        /// </summary>
        public static string MongoDBName { get; set; }
        /// <summary>
        /// 加密密匙
        /// </summary>
        public static string ApiKey { get; set; }
        /// <summary>
        /// 客服端IP地址
        /// </summary>
        public static string ClientIP { get; set; }
        /// <summary>
        /// 删除图片请求地址前缀
        /// </summary>
        public static string RemovePathHost { get; set; }
    }
}
