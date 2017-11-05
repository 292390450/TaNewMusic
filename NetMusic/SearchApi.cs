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
        /// <summary>
        /// 获取热门搜索关键字
        /// </summary>
        /// <returns></returns>
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

        public static List<string> GetKeySuggest(string para)
        {
            List<string> resuList=new List<string>();
            string url = MusicApi.GetKeySuggesUrl(para);
            JObject resul=JObject.Parse(HttpClientHelper.GetAsync(url).Result);
            if (resul["data"]["singer"]!=null)
            {
                JToken singers = resul["data"]["singer"]["itemlist"];
                foreach (var singer in singers)
                {
                    string name = singer["name"].ToString();
                    resuList.Add(name);
                }
            }
            if (resul["data"]["song"] != null)
            {
                JToken songs = resul["data"]["song"]["itemlist"];
                foreach (var song in songs)
                {
                    string name = song["name"] + " " + song["singer"];
                    resuList.Add(name);
                }
            }
            if (resul["data"]["album"]!=null)
            {
                JToken albums = resul["data"]["album"]["itemlist"];
                foreach (var album in albums)
                {
                    string name = album["name"] + " " + album["singer"];
                    resuList.Add(name);
                }
            }
            if (resuList.Count()>15)
            {
                var ress = resuList.Take(15);
                return ress.ToList();
            }
            return resuList;
        }
        #endregion
    }
}
