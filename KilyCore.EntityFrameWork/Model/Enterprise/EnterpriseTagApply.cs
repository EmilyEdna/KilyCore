﻿using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
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
        public virtual string BatchNo { get; set; }
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
        public virtual string PaytTicket { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public AuditEnum AuditType { get; set; }
    }
}
