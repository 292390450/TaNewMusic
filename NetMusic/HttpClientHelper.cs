using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetMusic
{
    public class HttpClientHelper
    {
        /// <summary>
        /// 异步Get
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate,UTF-8");

                //  client.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
                // client.DefaultRequestHeaders.Add("Accept-Language", "zh-Hans-CN, zh-Hans; q=0.5");
                //  client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063");
                try
                {
                    HttpResponseMessage message = await client.GetAsync(uri);
                    message.EnsureSuccessStatusCode();
                    return await message.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    return string.Empty;
                }

            }
        }
        public static async Task<string> GetSingerDesricAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate,UTF-8");
                client.DefaultRequestHeaders.Add("Referer", "https://c.y.qq.com/xhr_proxy_utf8.html");
                  client.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
                // client.DefaultRequestHeaders.Add("Accept-Language", "zh-Hans-CN, zh-Hans; q=0.5");
                //  client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063");
                try
                {
                    HttpResponseMessage message = await client.GetAsync(uri);
                    message.EnsureSuccessStatusCode();
                    return await message.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    return string.Empty;
                }

            }
        }
      
        public static async Task<string> PostAsync(string uri, List<KeyValuePair<string, string>> paras)
        {
            using (var client=new HttpClient())
            {
                try
                {
                    var content = new FormUrlEncodedContent(paras);
                    var message = await client.PostAsync(uri, content);
                    return await message.Content.ReadAsStringAsync();
                }
                catch (Exception exception)
                {
                    return string.Empty;
                }
            }
          
        }
    }
}
