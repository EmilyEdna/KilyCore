using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KilyCore.RabbitMQ.ExtenMQ
{
    public enum MQEnum
    {
        /// <summary>
        /// 订阅模式
        /// </summary>
        [Description("订阅模式")]
        Sub = 10,
        /// <summary>
        /// 推送模式
        /// </summary>
        [Description("推送模式")]
        Push = 20,
        /// <summary>
        /// 主路由模式
        /// </summary>
        [Description("主路由模式")]
        Top = 30
    }
}
