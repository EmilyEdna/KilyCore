using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：HttpClientExtension
* 类 描 述 ：
* 命名空间 ：KilyCore.Extension.HttpClientFactory
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/8/15 11:55:32
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.Extension.HttpClientFactory
{
    /// <summary>
    /// Http客服端
    /// </summary>
    public class HttpClientExtension
    {
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        private static HttpClient Client;
        public static HttpClient CreateInstance()
        {
            if (Client == null)
                lock (locker)
                    if (Client == null)
                        Client = new HttpClient();
            return Client;
        }
        /// <summary>
        /// Post异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<String> HttpPostAsync(string url, string data, Dictionary<string, string> headers = null, string contentType = null, int timeout = 0, Encoding encoding = null)
        {
            Client = Client ?? CreateInstance();
            if (headers != null)
                foreach (KeyValuePair<string, string> header in headers)
                {
                    Client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            if (timeout > 0)
                Client.Timeout = new TimeSpan(0, 0, timeout);
            HttpContent content = new StringContent(data ?? "", encoding ?? Encoding.UTF8);
            if (contentType != null)
                content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            HttpResponseMessage responseMessage = await Client.PostAsync(url, content);
            Byte[] resultBytes = await responseMessage.Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(resultBytes);
        }
        /// <summary>
        /// Get异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<String> HttpGetAsync(string url, Dictionary<string, string> headers = null, int timeout = 0)
        {
            Client = Client ?? CreateInstance();
            if (headers != null)
                foreach (KeyValuePair<string, string> header in headers)
                {
                    Client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            if (timeout > 0)
                Client.Timeout = new TimeSpan(0, 0, timeout);
            Byte[] resultBytes = await Client.GetByteArrayAsync(url);
            return Encoding.Default.GetString(resultBytes);
        }
    }
}
