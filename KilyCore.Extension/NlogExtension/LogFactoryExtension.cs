using NLog;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.NlogExtension
{
    /// <summary>
    /// NLog日志工厂
    /// </summary>
    public static class LogFactoryExtension
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteInfo(string path, string MethodName, string parameter, string msg)
        {
            logger.Info($"位置：{path}，方法名：{MethodName}，参数：{parameter}，信息：{msg}");
        }

        /// <summary>
        /// 栈日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteTrace(string path, string MethodName, string parameter, string msg)
        {
            logger.Trace($"位置：{path}，方法名：{MethodName}，参数：{parameter}，信息：{msg}");
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteDebug(string path, string MethodName, string parameter, string msg)
        {
            logger.Debug($"位置：{path}，方法名：{MethodName}，参数：{parameter}，信息：{msg}");
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(string path, string MethodName, string parameter, string msg)
        {
            logger.Error($"位置：{path}，方法名：{MethodName}，参数：{parameter}，信息：{msg}");
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarn(string path, string MethodName, string parameter, string msg)
        {
            logger.Warn($"位置：{path}，方法名：{MethodName}，参数：{parameter}，信息：{msg}");
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteFatal(string path, string MethodName, string parameter, string msg)
        {
            logger.Fatal($"位置：{path}，方法名：{MethodName}，参数：{parameter}，信息：{msg}");
        }
    }
}