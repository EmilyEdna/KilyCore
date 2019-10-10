using System;

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

#endregion << 版 本 注 释 >>

namespace KilyCore.Extension.PayCore.WxPay
{
    public static class WxPayModel
    {
        public const string AppId = @"wx0f1f6a2c8c4eb784";
        public const string AppSecret = @"c13856ce7b8bcfc683509547e7431d91";
        public const string MerchantAccount = @"1552401181";
        public const string MerchantPayKey = @"cdsbykjyxgs028ZSYzsglipt12345678";
        public static string OutTradeNo = DateTime.Now.ToString("yyyyMMddhhmmss");
        public const string NotifyUrl = @"http://system.cfda.vip/StaticHtml/WxNotify.html";
        public const string GateWayUrl = @"https://api.mch.weixin.qq.com";
    }
}