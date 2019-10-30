using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseSystemLogInfo
    {
        public  DateTime? CreateTime { get; set; }
        public  string CreateUser { get; set; }
        public  DateTime? DeleteTime { get; set; }
        public  string DeleteUser { get; set; }
        public string HandlerContent { get; set; }
        public DateTime? HandlerTime { get; set; }
        public string HandlerType { get; set; }
        public string HandlerUser { get; set; }
        public Guid Id { get; set; }
        public  bool? IsDelete { get; set; }
        public string Open { get; set; }
        public string Status { set; get; }
        public string TypePath { get; set; }
        public  DateTime? UpdateTime { get; set; }
        public  string UpdateUser { get; set; }
    }
}
