﻿using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Finance
{
    /// <summary>
    /// 后台用户信息附加表-财务专用表
    /// </summary>
    public class SystemAdminAttach :BaseEntity
    {
        /// <summary>
        /// 系统用户主表ID
        /// </summary>
        public virtual Guid AdminId { get; set; }
        /// <summary>
        /// 加盟开始时间
        /// </summary>
        public virtual DateTime? StartTime { get; set; }
        /// <summary>
        /// 加盟结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }
        /// <summary>
        /// 是否缴费
        /// </summary>
        public virtual bool? IsPay { get; set; }
        /// <summary>
        /// 加盟金额
        /// </summary>
        public virtual decimal Money { get; set; } 
        /// <summary>
        /// 缴费人
        /// </summary>
        public virtual string PayUser { get; set; }
    }
}
