using KilyCore.EntityFrameWork.Model.Base;
using KilyCore.EntityFrameWork.ModelEnum;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.Model.System
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/12/10 14:35:57
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************/
#endregion
namespace KilyCore.EntityFrameWork.Model.System
{
    /// <summary>
    /// 支付记录表
    /// </summary>
    public class SystemPayInfo:BaseEntity
    {
        /// <summary>
        /// 商家或企业的Id
        /// </summary>
        public virtual Guid MerchantId { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public virtual PayEnum PayType { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public virtual string TradeNo { get; set; }
        /// <summary>
        /// 支付描述
        /// </summary>
        public virtual string PayDes { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public virtual Guid GoodsId { get; set; }
    }
}
