using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Repast
{
    public class RepastBillTicket: RepastBase
    {
        /// <summary>
        /// 主题
        /// </summary>
        public virtual string Theme { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime? UpTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content { get; set; }
    }
}
