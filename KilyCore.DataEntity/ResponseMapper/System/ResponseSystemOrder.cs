using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseSystemOrder
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单类型 -工单 -订单
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 下单企业
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public SystemVersionEnum? ServiceVersion { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public string ServiceVersionTxt { get; set; }
        /// <summary>
        /// 悬赏金额
        /// </summary>
        public decimal? ServicePrice { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public string ServiceContent { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string SubmitUser { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? SubmitTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderEnum? OrderStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatusTxt { get; set; }
        /// <summary>
        /// 接单人
        /// </summary>
        public string OrderAccepter { get; set; }
        /// <summary>
        /// 接单时间
        /// </summary>
        public DateTime? OrderAccepterTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }
        /// <summary>
        /// 是否过期
        /// </summary>
        public bool? IsExpire { get; set; }
        /// <summary>
        /// 是否过期
        /// </summary>
        public string IsExpireTxt => IsExpire.HasValue ? (IsExpire.Value ? "是" : "否") : "-";
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 政府名称
        /// </summary>
        public string GovtName { get; set; }
        /// <summary>
        /// 政府Id
        /// </summary>
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid? CompanyId { get; set; }
    }
}
