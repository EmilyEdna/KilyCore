using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Extension.ResultExtension
{
    /// <summary>
    /// 返回状态自定义
    /// </summary>
    public enum HttpCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success=10,
        /// <summary>
        /// 无授权
        /// </summary>
        NoAuth=20,
        /// <summary>
        /// 系统异常
        /// </summary>
        SystemEx=30,
        /// <summary>
        /// 404
        /// </summary>
        NULL=40,
        /// <summary>
        /// 失败
        /// </summary>
        FAIL=50

    }
    /// <summary>
    /// 消息操作返回
    /// </summary>
    public class RetrunMessge {
        public const string SUCCESS = "操作成功!";
        public const string FAIL = "操作失败!";
        public const string EXCEPTION = "操作异常!";
    }
}
