using EasyNetQ;
using EasyNetQ.Topology;
using KilyCore.Configure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KilyCore.RabbitMQ.ExtenMQ
{
    public class MQFactory
    {
        private volatile static IBus Bus = null;
        /// <summary>
        /// 创建链接
        /// </summary>
        /// <returns></returns>
        public static IBus CreateMQ()
        {
            if (Bus == null)
                Bus = RabbitHutch.CreateBus(Configer.RabbitMQConnectionString);
            return Bus;
        }
        /// <summary>
        /// 释放链接
        /// </summary>
        public static void DisposeBus()
        {
            Bus?.Dispose();
        }
        /// <summary>
        /// 同步执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static bool SendMQ<T>(PushEntity<T> Param) where T : class
        {
            try
            {
                if (Bus == null)
                    CreateMQ();
                new PushManage().SendMQ(Param, Bus);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 推荐异步执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static async Task SendMQAsync<T>(PushEntity<T> Param) where T : class
        {

            if (Bus == null)
                CreateMQ();
            await new PushManage().SendMQAsync(Param, Bus);
        }
        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="TAccept"></typeparam>
        /// <param name="Args"></param>
        public static void Subscriber<TAccept, T>(AcceptEntity Args) where TAccept : IAccept, new() where T : new()
        {
            if (Bus == null)
                CreateMQ();
            if (string.IsNullOrEmpty(Args.ExchangeName))
                return;
            Expression<Action<IAccept>> methodCall;
            IExchange EX = null;
            if (Args.SendType == MQEnum.Sub)
            {
                EX = Bus.Advanced.ExchangeDeclare(Args.ExchangeName, ExchangeType.Fanout);
            }
            if (Args.SendType == MQEnum.Push)
            {
                EX = Bus.Advanced.ExchangeDeclare(Args.ExchangeName, ExchangeType.Direct);
            }
            if (Args.SendType == MQEnum.Top)
            {
                EX = Bus.Advanced.ExchangeDeclare(Args.ExchangeName, ExchangeType.Topic);
            }
            IQueue queue = Bus.Advanced.QueueDeclare(Args.QueeName ?? null);
            Bus.Advanced.Bind(EX, queue, Args.RouteName);
            Bus.Advanced.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
              {
                  try
                  {
                      var message = Encoding.UTF8.GetString(body);
                      //处理消息
                      methodCall = job => job.AcceptMQ<T>(message);
                      methodCall.Compile()(new TAccept());
                  }
                  catch (Exception ex)
                  {
                      throw ex;
                  }
              }));
        }
    }
}
