using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestSystemLogInfo
    {
        public Guid Id { get; set; }
        public string HandlerUser { get; set; }
        public string HandlerType { get; set; }
        public DateTime? HandlerTime { get; set; }
        public string HandlerContent { get; set; }
        public string TypePath { get; set; }
    }
}
