using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KilyCore.WEB.Util
{
    /// <summary>
    /// HttpClient
    /// </summary>
    public class HttpClientUtil
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
        /// 将数据制作表单数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="Map"></param>
        /// <returns></returns>
        public static IList<KeyValuePair<String, String>> keyValuePairs<T>(T Entity, IDictionary<string, string> Map = null) where T : class, new()
        {
            IList<KeyValuePair<String, String>> keyValuePairs = new List<KeyValuePair<string, string>>();
            Entity.GetType().GetProperties().ToList().ForEach(t =>
            {
                foreach (KeyValuePair<String, String> item in Map)
                {
                    var flag = t.CustomAttributes.Where(x => x.AttributeType == typeof(JsonIgnoreAttribute)).FirstOrDefault();
                    if (item.Key.Equals(t.Name))
                        keyValuePairs.Add(new KeyValuePair<string, string>(item.Value, t.GetValue(Entity).ToString()));
                    else
                        if (flag == null)
                        keyValuePairs.Add(new KeyValuePair<string, string>(t.Name, t.GetValue(Entity).ToString()));
                }
            });
            return keyValuePairs;
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
        public static async Task<String> HttpPostAsync(string url, IList<KeyValuePair<String, String>> data, Dictionary<string, string> headers = null, string contentType = null, int timeout = 0, Encoding encoding = null)
        {
            Client = Client ?? CreateInstance();
            if (headers != null)
                foreach (KeyValuePair<string, string> header in headers)
                {
                    Client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            Client.DefaultRequestHeaders.Remove("TimeSpan");
            Client.DefaultRequestHeaders.Add("TimeSpan", ((Int64)(new TimeSpan(DateTime.UtcNow.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0).Ticks)).TotalMilliseconds)).ToString());
            if (timeout > 0)
                Client.Timeout = new TimeSpan(0, 0, timeout);
            HttpContent content = new FormUrlEncodedContent(data);
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
            if (Client.DefaultRequestHeaders.Remove("TimeSpan"))
                Client.DefaultRequestHeaders.Add("TimeSpan", ((Int64)(new TimeSpan(DateTime.UtcNow.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0).Ticks)).TotalMilliseconds)).ToString());
            if (timeout > 0)
                Client.Timeout = new TimeSpan(0, 0, timeout);
            Byte[] resultBytes = await Client.GetByteArrayAsync(url);
            return Encoding.Default.GetString(resultBytes);
        }
    }
}
