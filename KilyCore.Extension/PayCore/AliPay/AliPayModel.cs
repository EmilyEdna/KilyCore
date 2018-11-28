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
        public const string AppId = @"2016080500171116";
        public const string PrivateKey = @"MIIEowIBAAKCAQEAyfAKok7mDyZZ1DVntedtnmdgi5p7RAXD5SrRNUA3XJvaWDwVteBxZEV2seIhs0r8jYT55Vomm1WuuZBmO8wjmh3dcobhOE1icyOsKdeJZh3L0f5fCuIvFTg9s8a9W3IgRAsAt3GVGYsC9fDSmH5q48uBQQI3BKtLgLFTwSvuUZ6JIMQ9L1NaR0vZeuw3bWtkYAnAi6lqS0tR5yKX8UfN2dmtZytD96reQrRfeCudmk315PQnoaPDoU/SmBnieJvU3R9bP1zp2F7p9oaFcHoVue5pa6iaxgkz5ExlhCMnLGrjL+jpCMjp7hPdE673mxYiFEODx4/Uoc4/Gq4GHTVCswIDAQABAoIBABjWkqhGJhwE7pWggAIq1hvewUxKkxDZ2K+OZa80F9snPGMP+OenFKtts9zLYLPJ7GiHy6j/PrJqhpRCg6ClSlfBhqR4nKktM4fQAnQfE8Jgtdo9fHk9OudURJqhPldKIVfdNphZuyoqJBIWLgEdw44DUQGrARNs8luA9DlVavnU2xG90lMFrNpFjlz+41U0JBfGdKNoiRornbQqZKPMljCRsG4wnixVzgNekkcXFfBE3IMvwExhlyau7mEB3BZmNP+Xj2L0czSV2fAIKMAQZ2yxIw8DDr0v+62j951ufhiyBigvvE6dnXmZtLAtzgasNI78bgyKtMewAFtIMwXTj7kCgYEA9nGZ+D3zF8xRTnSdxlMEi2vj5jusPE4VRBIgslpfaP96mf3TEK0JkjARKaaeUH/DRWzVdLbDyjkSk4V8KoAigJOHfqZKSEoOQQwnd7MaL61i5UhzwD7nhQcQEHMnDCkxOY8C8LAeFpcYeyPS9o9WZXTpsgrLMZBznBf8Ht+gQ+UCgYEA0cSjCtS6/u7hwHSgDI4MfKsGHRJfjJrl1nQMMUtJAG4EeIAlJ2pjfD5x3qdt9I0xpbOoUm7VEHyHpy1I/FZG+v2keGK1YowXGRaC+o0IxXZgRRftwXaQE925Kg211yPeVOx0ImHxa5yfBYZ4oJUH4h9V2TOM3CfouE+gZSC6MrcCgYAJHIQbyHaPX6913hvjNDiLyw5e+b/RbvWcUatBzNOQNznaLac7C5q5++r2hkF+KmsWywJNUWCFvnxwVdQUYi5kYI/238EJUSubvg5Lf6StNJTec0JMTkcTBQLUCN3dtjwqq53pNim2nhl6nAV1tjD0iVEGkx2WFIghskVRAuWS/QKBgF6dSsz/1rhWFw0vjWEcURWKLl++gcpxs2lfPoCdbHf9J5cSda+plOXdZI8BBd9gMByFRE6qHwAoqycEjf8DC1D6Rszl+NpSRbjTJpDMPECUfCet+1dfmuza/UGynWpEi2vSmy9G7hWTt0ZOuOn1TnD3oUkur0I93gIEKFx0vodRAoGBANTbcxoA5mhpeUEbKMXjtqVvRNARwGN8tL7zQzHlDJgdzbSRbeQk2a830FsP55noldApR2KUsqrfi+qLxMTBY0iRZKWIb+NJDbrScSuWxbFhHoEo64fvp4XkFs654t7H9pEQoiR98wdnQ0vSNknRzwTA41KmrK8T1lZAeMoxtC6w";
        public const string PublicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyfAKok7mDyZZ1DVntedtnmdgi5p7RAXD5SrRNUA3XJvaWDwVteBxZEV2seIhs0r8jYT55Vomm1WuuZBmO8wjmh3dcobhOE1icyOsKdeJZh3L0f5fCuIvFTg9s8a9W3IgRAsAt3GVGYsC9fDSmH5q48uBQQI3BKtLgLFTwSvuUZ6JIMQ9L1NaR0vZeuw3bWtkYAnAi6lqS0tR5yKX8UfN2dmtZytD96reQrRfeCudmk315PQnoaPDoU/SmBnieJvU3R9bP1zp2F7p9oaFcHoVue5pa6iaxgkz5ExlhCMnLGrjL+jpCMjp7hPdE673mxYiFEODx4/Uoc4/Gq4GHTVCswIDAQAB";
        public const string ReturnUrl = @"http://localhost:50070/Pay/Payment";
        public const string NotifyUrl = @"http://localhost:50070/Pay/Notify";
        public const string GatewayUrlTest = @"https://openapi.alipaydev.com";
        public const string GatewayUrl = @"https://openapi.alipay.com";
        public static string OutTradeNo = DateTime.Now.ToString("yyyyMMddhhmmss");
        public static string OutRefundNo = DateTime.Now.ToString("yyyyMMddhhmmss");
    }
}
