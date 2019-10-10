using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

#region << 版 本 注 释 >>

/*----------------------------------------------------------------
* 类 名 称 ：ResultContext
* 类 描 述 ：
* 命名空间 ：KilyCore.Extension.ResultExtension
* 机器名称 ：EMILY
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/7 9:16:58
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/

#endregion << 版 本 注 释 >>

namespace KilyCore.Extension.ResultExtension
{
    public class ResultContext
    {
        /// <summary>
        /// 过来数据中的密码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Object FilterResult(HttpContext context, IActionResult result)
        {
            String data = string.Empty;
            String Json = string.Empty;

            if (result is EmptyResultEx)
            {
                data = (result as EmptyResultEx).Value == null ? "" : (result as EmptyResultEx).Value.ToString();
                Json = JsonConvert.SerializeObject((result as EmptyResultEx).Value);
            }
            if (result is ObjectResultEx)
            {
                data = (result as ObjectResultEx).Value == null ? "" : (result as ObjectResultEx).Value.ToString();
                Json = JsonConvert.SerializeObject((result as ObjectResultEx).Value);
            }
            if (result is ContentResultEx)
            {
                data = (result as ContentResultEx).Value == null ? "" : (result as ContentResultEx).Value.ToString();
                Json = JsonConvert.SerializeObject((result as ContentResultEx).Value);
            }
            if (result is StatusCodeResultEx)
            {
                data = (result as StatusCodeResultEx).Value == null ? "" : (result as StatusCodeResultEx).Value.ToString();
                Json = JsonConvert.SerializeObject((result as StatusCodeResultEx).Value);
            }
            if (result is JsonResultEx)
            {
                data = (result as JsonResultEx).Value == null ? "" : (result as JsonResultEx).Value.ToString();
                Json = JsonConvert.SerializeObject((result as JsonResultEx).Value);
            }
            if (context.Request.Headers["PC"].Equals("PC"))
            {
                if (data.Contains("CookAdmin") || data.Contains("SysAdmin") || data.Contains("ComAdmin") || data.Contains("RepAdmin") || data.Contains("GovtAdmin"))
                    return JsonConvert.DeserializeObject(Json.Replace("\"PassWord\":\"admin\",", ""));
                else
                    return null;
            }
            else
                return null;
        }
    }
}