using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.RabbitMQ.ExtenMQ
{
    public class PushEntity<T>
    {
        /// <summary>
        /// 推送内容
        /// </summary>
        public T BodyData { get; set; }
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
