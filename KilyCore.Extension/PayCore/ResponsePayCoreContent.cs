#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：ResponsePayCoreContent
* 类 描 述 ：
* 命名空间 ：KilyCore.Extension.PayCore
* 机器名称 ：DESKTOP-QPIVQ28
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/30 10:22:28
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.Extension.PayCore
{
    public class ResponsePayCoreContent
    {
        /// <summary>
        /// true表示支付宝false表示
        /// </summary>
        public bool PayType { get; set; }

        /// <summary>
        /// 支付成共返回的内容
        /// </summary>
        public string PayContent { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public string GoodsId { get; set; }
    }
}