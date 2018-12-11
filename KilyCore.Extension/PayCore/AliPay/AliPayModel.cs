using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：AliPayModel
* 类 描 述 ：
* 命名空间 ：KilyCore.Extension.PayCore.AliPay
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/24 17:00:18
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Extension.PayCore.AliPay
{
    public static class AliPayModel
    {
        public const string AppId = @"2016101302139200";
        public const string PrivateKey = @"MIIEpQIBAAKCAQEA9kLMrRz36e0qpgUEKKzIrTW99s7K+OjFvuvdB95ShqqbzhhjZlymQDHGZe9MkG92yo1pPdInBM6C04LV8S8cWLYLPBl8Ml8QKX3PWdxbjh7w7+axVC3bkq577uYon/nikv6mVmz3oBKvNq1x2HJIeixjzaiQwJt1xqmQdSqKtsMnWSE7wh8NXGZm7+HQCDJ+Eae5F+6l597c5mcMAStFvhHeUQJVjp/PxCDyItmBLoPatz0fi7CJrYLYcEAe3vLxT/rnXQ3hLxBgj4GWuhmFjaNizsAPBi0mIVGVQsQqz+xjnwbB4/C8AUCHAYPlDmtfx3EWuCIplsbp7FpMkL4wMwIDAQABAoIBAQDincQmRjMp1LNbheA24hHluz/t4IzOE77tm7WSKRf+jv/D35ORPg9LocJ2SYlGnRxO92Tym/KGLNgddhgU04qdZCDglH9DB1IjLIBkrDCgj36e9ccXtP+Lcv/fb3PRC65iIO7HN42Eg80zXtcWHMzviOvq9IUIiccPIzkpaRWvagcX8DiePB0w10ZBs+PJMSyWbipNgsxNiEIvhsbqC782rWxHlUMNvC50uiUBFY9r3lTllP4uESKpSW6vVH8KeDasZgtyGF3kqgxtnWIGVcxVHh0KQEMwOHU3Ph+KjvQbMjQidTk8Hl7E+eRS9CKApgiYvbKGhKgbNvJCaA4NCzGZAoGBAP/mHLGUnrU+4urfW6FoxfgkBCNZVYC8Q6YwMB1sq6xyzqyJenQV5oCdhanbN15vmzsQbYO675jRsZwT2KwMtsZeo6ox64pFrHNNctFVDMUhhPObHKSpKLvYhblXcXRlXtT42oH9D+ODvYzoTdVV/VpiVkJgJQcWj7Zo7gvppxRHAoGBAPZbtmC2aQNQECLHho24Ao+d2bYq8B8/V57hVnTNNMu1L0YVEX+3y+NMliffvlphbj0HNOy4n/bP9Pon3Yh/5b9EMkNaQsHTUmQfXZ6wAnSFdV8gBZVPbDHQD1OxKJ5FaNrwLfUTxNA9vB+dXwsGzs5MXntn1pBV8yru4PhUS1a1AoGBAMPxaWs4h8X0n0CFQt53IY991IVUtDnc7lfkchM10MY9Kq/GnTMiSduVwnFrYBw5jT1OuOyzSR9VAZCnSbOEELgYY23Ax8Ca1Q988DwFyb9wiSMXVHlo5b3gb5SZtlAQButAmeAK7A2kJJIultqR+2sn/TDZ+cBRsD1WX2BTyxGlAoGBAN+jb9Mpo/jK5Gr+H6plAYYoMGvLXMGw+bSUMKzFYAkQiEKhe4oqah8D9kdqOF3Jlo42DZhbHXOrYmL4b4WrPP+9Q7eap4FWebPEOXOFqiTmxh69bfQV7kzl6BP8fbf3oHEnmUCiWocDk9RLQu06l9/0ucxi/gd2ztEJmxYzR1UFAoGASLdj6nHzxspNxGBvIAmvq2T8elyBchCGZWjWPq2SnKEmLi5CVEsMX/04aBgy2FY18SKOX1uflm2TzGVHdodGrYlq8Jx0HmIrkhlVUS3DkqPCk+aJ/mfow6ZiMb949EJ6wCVVcRNm5dDmraRDhClheW+aDXlxrLzyn/34hZJtvFA=";
        public const string PublicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlK9um+AYpZTIcD/BwJY74kF5EHnu/q2KPujFGGGG4/GV+ygQqNOMUxUCrvplNmMNhXO8uT4sbtfiscFfIHuE4myydhWwS8gAKCoOZ3ycTNFD2cugXKYPNSaqcI0PrCZEdux945tC40MFBdikZEhZQZiuR8WyRAs0TIjNcot5punjf5nORhJc8GDV86ZXjoL+hmPs0x+eu0AJkVML1yu509EKOX6Lmne2TKfvgmWWAVhUvhEvrTgw2gYR764O9TEhZo7cbSaBudh9S+DlT7IWoIfrnfWL86Ka6I6mHJCtlL5jRueRrn3vwZ3yX3TKju7LHD13NOI74SSBFBaXjppU0QIDAQAB";
        public const string ReturnUrl = @"http://main.cfdacx.com/StaticHtml/AliPayNotify.html";
        public const string NotifyUrl = @"http://main.cfdacx.com/StaticHtml/AliPayNotify.html";
        public const string GatewayUrlTest = @"https://openapi.alipaydev.com";
        public const string GatewayUrl = @"https://openapi.alipay.com";
        public static string OutTradeNo = DateTime.Now.ToString("yyyyMMddhhmmss");
        public static string OutRefundNo = DateTime.Now.ToString("yyyyMMddhhmmss");
    }
}
