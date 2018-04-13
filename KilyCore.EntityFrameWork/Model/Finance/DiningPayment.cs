using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Finance
{
    /// <summary>
    /// 餐饮缴费表
    /// </summary>
    public class DiningPayment:BaseEntity
    {
        /// <summary>
        /// 商家Id
        /// </summary>
        public virtual Guid MerchantId { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public virtual string MerchantName { get; set; }
        /// <summary>
        /// 支付方式：1表示按月订单支付，2表示按年支付
        /// </summary>
        public virtual int PayType { get; set; }
        /// <summary>
        /// 缴费日期
        /// </summary>
        public virtual DateTime PayTime { get; set; }
        /// <summary>
        /// 开通年限
        /// </summary>
        public virtual string EnableYear { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public virtual DateTime? EnableYearEndTime { get; set; }
        /// <summary>
        /// 缴费金额
        /// </summary>
        public virtual decimal? Paymoney { get; set; }
        /// <summary>
        /// 月订单总额
        /// </summary>
        public virtual decimal? OrderMoneySum { get; set; }
        /// <summary>
        /// 缴费人
        /// </summary>
        public virtual string PayUser { get; set; }
        /// <summary>
        /// 缴款人电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
        /// <summary>
        /// 缴费凭证
        /// </summary>
        public virtual string PayCertificate { get; set; }
    }
}
