using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：WxPayModel
* 类 描 述 ：
* 命名空间 ：KilyCore.Extension.PayCore.WxPay
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/26 13:02:21
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Extension.PayCore.WxPay
{
    public static class WxPayModel
    {
        public const string AppId = @"wxbd2dadd9e0775bcd";
        public const string AppSecret = @"c071fa71707e8afa6f078b1762d674b5";
        public const string MerchantAccount = @"1451396702";
        public const string MerchantPayKey = @"chengduyanchengkejigs02885336372";
        public static string OutTradeNo = DateTime.Now.ToString("yyyyMMddhhmmss");
        public const string NotifyUrl = @"http://main.cfdacx.com/StaticHtml/WxNotify.html";
        public const string GateWayUrl = @"https://api.mch.weixin.qq.com";
    }
}
