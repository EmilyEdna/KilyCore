using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 日志记录表
    /// </summary>
    public class SystemLog : BaseEntity
    {
        /// <summary>
        /// 日志名称
        /// </summary>
        public virtual string LogName { get; set; }
        /// <summary>
        /// 事件数据
        /// </summary>
        public virtual string EventData { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public virtual string Source { get; set; }
    }
}
