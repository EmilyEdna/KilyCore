using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseSystemOrderLog
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public  Guid? OrderId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public  string HandlerUser { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public  DateTime? HandlerTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public  OrderEnum? OrderStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatusTxt { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public  string OrderRemark { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public  string LogType { get; set; }
    }
}
