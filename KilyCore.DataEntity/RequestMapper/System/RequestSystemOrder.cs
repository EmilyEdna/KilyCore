using KilyCore.EntityFrameWork.Attributes;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestSystemOrder
    {
        [NoneUpdate]
        public Guid Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [NoneUpdate]
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单类型 -工单 -订单
        /// </summary>
        [NoneUpdate]
        public string OrderType { get; set; }
        /// <summary>
        /// 下单企业
        /// </summary>
        [NoneUpdate]
        public string CompanyName { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        [NoneUpdate]
        public SystemVersionEnum? ServiceVersion { get; set; }
        /// <summary>
        /// 悬赏金额
        /// </summary>
        [NoneUpdate]
        public decimal? ServicePrice { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        [NoneUpdate]
        public string ServiceContent { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        [NoneUpdate]
        public string SubmitUser { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        [NoneUpdate]
        public DateTime? SubmitTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderEnum? OrderStatus { get; set; }
        /// <summary>
        /// 接单人
        /// </summary>
        public string OrderAccepter { get; set; }
        /// <summary>
        /// 接单时间
        /// </summary>
        [NoneUpdate]
        public DateTime? OrderAccepterTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        [NoneUpdate]
        public DateTime? ExpireTime { get; set; }
        /// <summary>
        /// 是否过期
        /// </summary>
        [NoneUpdate]
        public bool? IsExpire { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 政府名称
        /// </summary>
        [NoneUpdate]
        public string GovtName { get; set; }
        /// <summary>
        /// 政府Id
        /// </summary>
        [NoneUpdate]
        public Guid? GovtId { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        [NoneUpdate]
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// 是否为手机端请求
        /// </summary>
        public bool IsApp { get; set; }
    }
}
