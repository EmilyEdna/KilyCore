using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 订单日志表
    /// </summary>
    public class SystemOrderLog:BaseEntity
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public virtual Guid? OrderId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual string  HandlerUser { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime?  HandlerTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public virtual OrderEnum?  OrderStatus { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public virtual string  OrderRemark { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public virtual string  LogType { get; set; }
    }
}
