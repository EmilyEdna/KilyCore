using KilyCore.Extension.HttpClientFactory;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：PhoneSMS
* 类 描 述 ：
* 命名空间 ：KilyCore.Extension.SendMessage
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/5 14:02:39
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Extension.SendMessage
{
    public class PhoneSMS
    {
        public static string SendPhoneMsg(String Phone, String Contents = null, IList<String> Phones = null)
        {
            String Address = "http://utf8.api.smschinese.cn/?Uid=cdyancheng&Key=ed8350884ae88ea84dc2&smsMob={0}&smsText={1}";
            if (Phones != null)
                Address = string.Format(Address, string.Join(",", Phones), Contents);
            else
                Address = string.Format(Address, Phone, Contents);
            return HttpClientExtension.HttpGetAsync(Address).Result;
        }
    }
}
