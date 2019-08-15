using System;
using System.Collections.Generic;
using System.Text;
using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class SystemOrder:BaseEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }
        /// <summary>
        /// 下单企业
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public virtual SystemVersionEnum? ServiceVersion { get; set; }
        /// <summary>
        /// 悬赏金额
        /// </summary>
        public virtual decimal? ServicePrice { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public virtual string ServiceContent { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public virtual string SubmitUser { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public virtual DateTime? SubmitTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public virtual OrderEnum? OrderStatus { get; set; }
        /// <summary>
        /// 接单人
        /// </summary>
        public virtual string OrderAccepter { get; set; }
        /// <summary>
        /// 接单时间
        /// </summary>
        public virtual DateTime? OrderAccepterTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public virtual DateTime? ExpireTime { get; set; }
        /// <summary>
        /// 是否过期
        /// </summary>
        public virtual bool? IsExpire { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 政府名称
        /// </summary>
        public virtual string GovtName { get; set; }
        /// <summary>
        /// 政府Id
        /// </summary>
        public virtual Guid? GovtId { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        public virtual Guid? CompanyId { get; set; }
    }
}
