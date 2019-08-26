using EasyNetQ;
using KilyCore.Configure;
using KilyCore.RabbitMQ.ExtenMQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KilyCore.RabbitMQ.Publisher
{
    public class ServiceQueryPush
    {
        public static ServiceQueryPush QueryPush => new Lazy<ServiceQueryPush>().Value;
        public async Task<bool> PushMQAsync<T>(T Entity, MQEnum MQType = MQEnum.Push) where T : class, new()
        {
            //推送模式
            //推送模式下需指定管道名称和路由键值名称
            //消息只会被发送到指定的队列中去
            //订阅模式
            //订阅模式下只需指定管道名称
            //消息会被发送到该管道下的所有队列中
            //主题路由模式
            //路由模式下需指定管道名称和路由值
            //消息会被发送到该管道下，和路由值匹配的队列中去
            PushEntity<T> entity = new PushEntity<T>();
            entity.BodyData = Entity;
            entity.SendType = MQType;
            if (MQType == MQEnum.Push)
            {
                entity.ExchangeName = "Message.Direct";
                entity.RouteName = "RouteKey";
            }
            else if (MQType == MQEnum.Sub)
            {
                entity.ExchangeName = "Message.Fanout";
            }
            else
            {
                entity.ExchangeName = "Message.Topic";
                entity.RouteName = "RouteKey";
            }
            return await MQFactory.SendMQAsync(entity).ContinueWith(t => { return t.IsCompleted ? true : false; });
        }
        public bool PushMQ<T>(T Entity, MQEnum MQType = MQEnum.Push) where T : class, new()
        {
            //推送模式
            //推送模式下需指定管道名称和路由键值名称
            //消息只会被发送到指定的队列中去
            //订阅模式
            //订阅模式下只需指定管道名称
            //消息会被发送到该管道下的所有队列中
            //主题路由模式
            //路由模式下需指定管道名称和路由值
            //消息会被发送到该管道下，和路由值匹配的队列中去
            PushEntity<T> entity = new PushEntity<T>();
            entity.BodyData = Entity;
            entity.SendType = MQType;
            if (MQType == MQEnum.Push)
            {
                entity.ExchangeName = "Message.Direct";
                entity.RouteName = "RouteKey";
            }
            else if (MQType == MQEnum.Sub)
            {
                entity.ExchangeName = "Message.Fanout";
            }
            else
            {
                entity.ExchangeName = "Message.Topic";
                entity.RouteName = "RouteKey";
            }
            return MQFactory.SendMQ(entity);
        }
    }
}
