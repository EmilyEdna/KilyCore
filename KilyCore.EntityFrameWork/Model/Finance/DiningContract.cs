using KilyCore.EntityFrameWork.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.EntityFrameWork.Model.Finance
{
    /// <summary>
    /// 餐饮合同表
    /// </summary>
    public class DiningContract : BaseEntity
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
        /// 合同内容
        /// </summary>
        public virtual string Contract { get; set; }
    }
}
