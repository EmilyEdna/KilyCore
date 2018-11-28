using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：PayEnum
* 类 描 述 ：
* 命名空间 ：KilyCore.EntityFrameWork.ModelEnum
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/6/27 9:59:40
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.EntityFrameWork.ModelEnum
{
    /// <summary>
    /// 支付枚举
    /// </summary>
    public enum PayEnum
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay=10,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WxPay =20,
        /// <summary>
        /// 银联
        /// </summary>
        [Description("银联")]
        Unionpay=30,
        /// <summary>
        /// 自定义支付
        /// </summary>
        [Description("自定义支付")]
        AgentPay =40

    }
}
