using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 缴费表
    /// </summary>
    public class SystemPayment:BaseEntity
    {
        /// <summary>
        /// 缴费目标表Id
        /// </summary>
        public virtual Guid TableId { get; set; }
        /// <summary>
        /// 缴费目标表表明
        /// </summary>
        public virtual string TableName { get; set; }
        /// <summary>
        /// 缴费人
        /// </summary>
        public virtual string PaymentUser { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 缴费时间
        /// </summary>
        public virtual DateTime PayTime { get; set; }
        /// <summary>
        /// 缴费凭证
        /// </summary>
        public virtual string PayCertificate { get; set; }
        /// <summary>
        /// 缴费金额
        /// </summary>
        public virtual decimal Paymoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
