using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseSystemLogInfo
    {
        public Guid Id { get; set; }
        public string HandlerUser { get; set; }
        public string HandlerType { get; set; }
        public DateTime? HandlerTime { get; set; }
        public string HandlerContent { get; set; }
        public string TypePath { get; set; }
        public string Status { set; get; }
    }
}
