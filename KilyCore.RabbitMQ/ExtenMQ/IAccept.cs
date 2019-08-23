using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.RabbitMQ.ExtenMQ
{
    public interface IAccept
    {
        void AcceptMQ<T>(string msg);
    }
}
