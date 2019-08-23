using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.RabbitMQ.ExtenMQ
{
    public class AcceptEntity
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public String QueeName { get; set; }
        /// <summary>
        /// 推送模式
        /// </summary>
        public MQEnum SendType { get; set; }
        /// <summary>
        /// 管道名称
        /// </summary>
        public String ExchangeName { get; set; }
        /// <summary>
        /// 路由名称
        /// </summary>
        public String RouteName { get; set; }
    }
}
