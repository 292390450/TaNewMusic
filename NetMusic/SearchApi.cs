using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NetMusic
{
    public class SearchApi
    {
        #region 外部

        public static List<string> GetHotKey()
        {
            List<string> resuList=new List<string>();
            string url = MusicApi.GetHotKey();
            JObject resul=JObject.Parse(HttpClientHelper.GetAsync(url).Result);
            JToken list = resul["data"]["hotkey"];
            foreach (var VARIABLE in list)
            {
                var key = VARIABLE["k"].ToString();
                resuList.Add(key);
            }
            return resuList;
        }

        public static async Task<List<string>> GetHoyKeyAsync()
        {
            List<string> resuList = new List<string>();
            string url = MusicApi.GetHotKey();
            JObject resul = JObject.Parse(await HttpClientHelper.GetAsync(url));
            JToken list = resul["data"]["hotkey"];
            foreach (var VARIABLE in list)
            {
                var key = VARIABLE["k"].ToString();
                resuList.Add(key);
            }
            return resuList;
        }
        #endregion
    }
}
