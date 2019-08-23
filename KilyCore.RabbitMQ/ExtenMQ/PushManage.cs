using EasyNetQ;
using EasyNetQ.Topology;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using KilyCore.EntityFrameWork.Model.System;
using KilyCore.Repositories.BaseRepository;

namespace KilyCore.RabbitMQ.ExtenMQ
{
    public class PushManage
    {
        /// <summary>
        /// 发布消息队列异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Param"></param>
        /// <param name="Bus"></param>
        /// <returns></returns>
        public async Task SendMQAsync<T>(PushEntity<T> Param, IBus Bus) where T : class
        {
            //one to one
            var msg = new Message<T>(Param.BodyData);
            IExchange EX = null;
            if (Param.SendType == MQEnum.Sub)
            {
                EX = Bus.Advanced.ExchangeDeclare(Param.ExchangeName, ExchangeType.Fanout);
            }
            if (Param.SendType == MQEnum.Push)
            {
                EX = Bus.Advanced.ExchangeDeclare(Param.ExchangeName, ExchangeType.Direct);
            }
            if (Param.SendType == MQEnum.Top)
            {
                EX = Bus.Advanced.ExchangeDeclare(Param.ExchangeName, ExchangeType.Topic);
            }
            await Bus.Advanced.PublishAsync(EX, Param.RouteName, false, msg).ContinueWith(t =>
            {
                //消息投递失败
                if (!t.IsCompleted && t.IsFaulted)
                {
                    //将消息记录到数据库轮询
                    IRepository Repository = new Repository();
                    SystemLog Log = new SystemLog
                    {
                        LogName = "消息队列",
                        Source = "发布队列",
                        EventData = JsonConvert.SerializeObject(Param.BodyData)
                    };
                    Repository.Insert(Log);
                }
            });
        }
        /// <summary>
        /// 发布消息队列同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Param"></param>
        /// <param name="Bus"></param>
        public void SendMQ<T>(PushEntity<T> Param, IBus Bus) where T : class
        {
            //one to one
            var msg = new Message<T>(Param.BodyData);
            IExchange EX = null;
            if (Param.SendType == MQEnum.Sub)
            {
                EX = Bus.Advanced.ExchangeDeclare(Param.ExchangeName, ExchangeType.Fanout);
            }
            if (Param.SendType == MQEnum.Push)
            {
                EX = Bus.Advanced.ExchangeDeclare(Param.ExchangeName, ExchangeType.Direct);
            }
            if (Param.SendType == MQEnum.Top)
            {
                EX = Bus.Advanced.ExchangeDeclare(Param.ExchangeName, ExchangeType.Topic);
            }
            Bus.Advanced.Publish(EX, Param.RouteName, false, msg);
        }
    }
}
