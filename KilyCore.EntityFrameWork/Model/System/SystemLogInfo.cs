using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 日志表
    /// </summary>
    public class SystemLogInfo
    {
        public virtual Guid Id { get; set; }
        public virtual string HandlerUser { get; set; }
        public virtual string HandlerType { get; set; }
        public virtual DateTime? HandlerTime { get; set; }
        public virtual string HandlerContent { get; set; }
        public virtual string TypePath { get; set; }
        //已读/未读
        public virtual string Status { set; get; }
    }
}
