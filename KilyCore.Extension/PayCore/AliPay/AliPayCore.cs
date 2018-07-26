using KilyCore.DataEntity.RequestMapper.System;
using PaySharp.Alipay;
using PaySharp.Alipay.Domain;
using PaySharp.Alipay.Request;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：Class1
* 类 描 述 ：
* 命名空间 ：KilyCore.Extension.PayCore.AliPay
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/24 15:49:45
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Extension.PayCore.AliPay
{
    /// <summary>
    /// 支付宝支付
    /// </summary>
    public class AliPayCore
    {
        public static AliPayCore Instance { get => new AliPayCore(); }

        /// <summary>
        /// 商户数据
        /// </summary>
        /// <returns></returns>
        private Merchant GetMerchantData()
        {
            return new Merchant
            {
                AppId = AliPayModel.AppId,
                Privatekey = AliPayModel.PrivateKey,
                AlipayPublicKey = AliPayModel.PublicKey,
                ReturnUrl = AliPayModel.ReturnUrl,
                NotifyUrl = AliPayModel.NotifyUrl
            };
        }
        /// <summary>
        /// 网关数据
        /// </summary>
        /// <returns></returns>
        private AlipayGateway GetGatewayData()
        {
            return new AlipayGateway(GetMerchantData())
            {
                GatewayUrl = AliPayModel.GatewayUrlTest
            };
        }
        /// <summary>
        /// 初始化支付宝请求
        /// </summary>
        /// <returns></returns>
        private WebPayRequest GetPayRequest(RequestAliPayModel Param)
        {
            var Request = new WebPayRequest();
            Request.AddGatewayData(new WebPayModel()
            {
                TotalAmount = Param.Money,
                OutTradeNo = AliPayModel.OutTradeNo,
                Body = Param.OrderDescription,
                Subject = Param.OrderTitle
            });
            return Request;
        }
        /// <summary>
        /// 支付宝PC端支付
        /// </summary>
        /// <returns></returns>
        public string WebPay(RequestAliPayModel Param)
        {
            return GetGatewayData().Execute(GetPayRequest(Param)).Html;
        }
    }
}
