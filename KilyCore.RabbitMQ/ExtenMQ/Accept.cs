using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KilyCore.RabbitMQ.ExtenMQ
{
    public class Accept : IAccept
    {
        public void AcceptMQ<T>(string msg)
        {
            JsonConvert.DeserializeObject<T>(msg);
            //do somethings
        }
    }
}
