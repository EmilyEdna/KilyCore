﻿using KilyCore.DataEntity.RequestMapper.System;
using Newtonsoft.Json;
using PaySharp.Alipay;
using PaySharp.Alipay.Domain;
using PaySharp.Alipay.Request;
using System;

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

#endregion << 版 本 注 释 >>

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
                AppId = null, //AliPayModel.AppId,
                Privatekey = null, //AliPayModel.PrivateKey,
                AlipayPublicKey = null,//AliPayModel.PublicKey,
                ReturnUrl = null, //AliPayModel.ReturnUrl,
                NotifyUrl = null, //AliPayModel.NotifyUrl
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
                GatewayUrl = AliPayModel.GatewayUrl
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
            ResponsePayCoreContent Model = new ResponsePayCoreContent
            {
                PayType = true,
                PayContent = GetGatewayData().Execute(GetPayRequest(Param)).Html.Replace("'gateway'", "'gateway' target='_blank'")
            };
            return JsonConvert.SerializeObject(Model);
        }

        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <returns></returns>
        public string GetTradeNo()
        {
            return AliPayModel.OutTradeNo;
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="TradeNo"></param>
        /// <returns></returns>
        public string QueryAliPay(String TradeNo)
        {
            QueryRequest Request = new QueryRequest();
            Request.AddGatewayData(new QueryModel()
            {
                OutTradeNo = TradeNo
            });
            return GetGatewayData().Execute(Request).TradeStatus;//TRADE_SUCCESS
        }
    }
}