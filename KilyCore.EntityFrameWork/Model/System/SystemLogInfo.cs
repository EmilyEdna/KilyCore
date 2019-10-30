using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 日志表
    /// </summary>
    public class SystemLogInfo:BaseEntity
    {
        public virtual string HandlerUser { get; set; }
        public virtual string HandlerType { get; set; }
        public virtual DateTime? HandlerTime { get; set; }
        public virtual string HandlerContent { get; set; }
        public virtual string TypePath { get; set; }
        /// <summary>
        ///   已读/未读
        /// </summary>
        public virtual string Status { set; get; }
        public string Open { get; set; }
    }
}
