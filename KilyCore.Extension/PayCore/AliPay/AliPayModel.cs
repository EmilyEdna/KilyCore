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
        public const string PrivateKey = @"MIIEowIBAAKCAQEAmhUFGz7XKi69Ax5GQ+VeTBoKYfJspYwCG6bG64M2ab6XOwe8wYDhA4DOy6Av4+0WKpolPh5Vea08nHb0l14/z6rFJvdg/TvIjsPcU4+00qCvz2an834Pa7UnEXCGkFk1Jn1rhFPhY/S8f+BODoePtWZIlKKqUFyADyQqiviKb/oizaR72gq1Olk3Lje06CMVUSHKkjRQPFjpll9Z3f+Z13Z+8PC98TKWxwJoU/H0qiE0TpGwKObFY3QllFRYbJIFNX52uK7DQ2rkfVD3ZLNqBubhlZEoXCHGWpmScIGBtflxlh3aQWkAMCZjzCrvhJk38wGrF4ZhrZSaM8/Zu6gZHwIDAQABAoIBAH79oA3Khw+PIdz2ELdYzZUvSStc4Eq4z+v2UrA8npYcrQvL3rgk/k8i1E1jItVZzBEkpllCKvRz4SabrA8tcK45cvAgpuFPaoavymFcaY3HPd9UHkxCs3b0ANUQxvclbvbgG8MeVv9I8Cr8tZioBbteJ6fJQVGmSy0vg5iyK3cJjCS6OoezVT6aQQSndt9qPw1vOX7FGjy/sdsKXzVVTDHxDwSviEtGYbk+XY68lmLqPX+1TESHxYIVW3gvBO0e5rvZguyy5PTeHWZsjiTtTteiPhoCJ9YHuTwZVgajM/U6W3TA9Ryx3QZKliuObtW3xLD6X8KFnUoL4pAArkB4GJECgYEAzR5AE50dCEQMCzgiH6DYZWvcc/KnZtKCvb45qe3epMwMACDuCDpYnyGTXTPRIFhtTCco0GHSGwErze39eSG4jKcSIcEul4sQSDz8uQSNinBANDJLorIrNuErsmuhcZj6BmU7BtLpVN0p63scRC/5D4hD4djdzIKFL0vyO50yFk0CgYEAwE3LXzICXSYnaLobtXSiU/NYjEZInFI78da0kDtoo8tD99KuWfGqYy2Un73sw+diECCSTpty1uhXH08BvGFmohvbyFgwBI3OmCw5alKiKde6mnxEO9S3MlgA4nlnkBGOF/9umEF3/hgh/skPEzN0pbAt/F7QeKgrFYAQgmMsOxsCgYBQq7gJvr3/VTvjYbVR+SGCafRvCZ06/En9NAZbxbjN2Y5SsqDLTZ8zHrJlco6qCP0beT89DWvjwQcoW5RYAqIhd0fTTLX1VfUWZB4E6jx8mP83SBylJSuHzvEvn9MD6WVUBJY9bc/k14K4IVgRnUh4CmCgP3mkCCU3X8gqF/3t9QKBgFpvO0jVV88+diIGA6nd48jfE7FEDDmXYqFkex2pEpiUqq4M4flicOKbON22j3XBhphW+PcGm4b9VJtgqExS8dUijMU2074QgiuGAKpDmgdbcGHeNqh8bHq8cA4RUeaqmswmT3hCsY9JAhOzsecxt5WDuNyKKjfTvg8qXpky0kYpAoGBAJaZb25pdCPMO6NASNgxZz0QrAujNjzVoA11RJR9jllqb9JvuLg3KqjEwttU6oLiBChj/lBFu8Zkja+3Bbw1RdeJ82a/nLsPznJtbqM5dxgqGOkOPzfwEAJv1Vndcz5lKCxDpkddXHyX0GuYvH/y8ysH/Q56H7nvaHqkLd22Vk0h";
        public const string PublicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmhUFGz7XKi69Ax5GQ+VeTBoKYfJspYwCG6bG64M2ab6XOwe8wYDhA4DOy6Av4+0WKpolPh5Vea08nHb0l14/z6rFJvdg/TvIjsPcU4+00qCvz2an834Pa7UnEXCGkFk1Jn1rhFPhY/S8f+BODoePtWZIlKKqUFyADyQqiviKb/oizaR72gq1Olk3Lje06CMVUSHKkjRQPFjpll9Z3f+Z13Z+8PC98TKWxwJoU/H0qiE0TpGwKObFY3QllFRYbJIFNX52uK7DQ2rkfVD3ZLNqBubhlZEoXCHGWpmScIGBtflxlh3aQWkAMCZjzCrvhJk38wGrF4ZhrZSaM8/Zu6gZHwIDAQAB";
        public const string ReturnUrl = @"http://localhost:50070/Pay/Payment";
        public const string NotifyUrl = @"http://localhost:50070/Pay/Notify";
        public const string GatewayUrlTest = @"https://openapi.alipaydev.com";
        public const string GatewayUrl = @"https://openapi.alipay.com";
        public static string OutTradeNo = DateTime.Now.ToString("yyyyMMddhhmmss");
        public static string OutRefundNo = DateTime.Now.ToString("yyyyMMddhhmmss");
    }
}
