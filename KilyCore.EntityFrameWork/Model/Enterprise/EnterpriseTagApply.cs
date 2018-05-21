using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Enterprise
{
    /// <summary>
    /// 标签申请表
    /// </summary>
    public class EnterpriseTagApply : EnterpriseBase
    {
        /// <summary>
        /// 批次编号
        /// </summary>
        public virtual string BacthNo { get; set; }
        /// <summary>
        /// 申请数量
        /// </summary>
        public virtual string ApplyNum { get; set; }
        /// <summary>
        /// 申请类型
        /// </summary>
        public virtual TagEnum TagType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal? ApplyMoney { get; set; }
        /// <summary>
        /// 付款方式1：表示年付费。2：表示按数量付费。
        /// </summary>
        public virtual int Payment { get; set; }
        /// <summary>
        /// 是否付款
        /// </summary>
        public virtual bool? IsPay { get; set; }
        /// <summary>
        /// 支付凭证
        /// </summary>
        public virtual string PaytTcket { get; set; }
    }
}
