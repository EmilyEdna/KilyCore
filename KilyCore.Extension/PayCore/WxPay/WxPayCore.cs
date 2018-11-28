using KilyCore.DataEntity.RequestMapper.System;
using Newtonsoft.Json;
using PaySharp.Wechatpay.Domain;
using PaySharp.Wechatpay.Request;
using PaySharp.Wechatpay;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：WxPayCore
* 类 描 述 ：
* 命名空间 ：KilyCore.Extension.PayCore.WxPay
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/26 13:01:36
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Extension.PayCore.WxPay
{
    public class WxPayCore
    {
        public static WxPayCore Instance { get => new WxPayCore(); }
        /// <summary>
        /// 商户数据
        /// </summary>
        /// <returns></returns>
        private Merchant GetMerchantData()
        {
            return new Merchant
            {
                AppId = WxPayModel.AppId,
                AppSecret = WxPayModel.AppSecret,
                Key = WxPayModel.MerchantPayKey,
                MchId = WxPayModel.MerchantAccount,
                NotifyUrl = WxPayModel.NotifyUrl
            };
        }
        /// <summary>
        /// 网关数据
        /// </summary>
        /// <returns></returns>
        private WechatpayGateway GetGatewayData()
        {
            return new WechatpayGateway(GetMerchantData())
            {
                GatewayUrl = WxPayModel.GateWayUrl
            };
        }
        /// <summary>
        /// 初始化微信支付请求
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        private ScanPayRequest GetPayRequest(RequestWxPayModel Param)
        {
            var Request = new ScanPayRequest();
            Request.AddGatewayData(new ScanPayModel
            {
                Body = Param.OrderTitle,
                TotalAmount = Param.Money * 100,
                OutTradeNo = WxPayModel.OutTradeNo
            });
            return Request;
        }
        /// <summary>
        /// 微信PC端支付
        /// </summary>
        /// <returns></returns>
        public string WebPay(RequestWxPayModel Param)
        {
            ResponsePayCoreContent Model = new ResponsePayCoreContent
            {
                PayType = false,
                PayContent = GetGatewayData().Execute(GetPayRequest(Param)).CodeUrl
            };
            return JsonConvert.SerializeObject(Model);
        }
    }
}
