using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMusic.Mode;
using Newtonsoft.Json.Linq;

namespace NetMusic
{
    public class AlbumApi
    {
        #region 外部

        public static async Task<Album> InitBriefInfoAlbumAsync(string albumId)
        {
            string albumidUri=MusicApi.GetAlbumInfo(albumId);
            string resul = await HttpClientHelper.GetAsync(albumidUri);
            return await Task.Run((() =>
            {
                Album album=new Album();
                try
                {
                    JObject jObject = JObject.Parse(resul);
                    album.AlbumId = jObject["data"]["album_id"].ToString();
                    album.Name = jObject["data"]["album_name"].ToString();
                    if (!string.IsNullOrEmpty(jObject["data"]["companyname"].ToString()))
                    {
                        album.Companyname = jObject["data"]["companyname"].ToString();
                    }
                    if (!string.IsNullOrEmpty(jObject["data"]["desc"].ToString()))
                    {
                        album.Desc = jObject["data"]["desc"].ToString();
                    }
                    if (!string.IsNullOrEmpty(jObject["data"]["publictime"].ToString()))
                    {
                        album.PubTime = jObject["data"]["publictime"].ToString();
                    }
                    var jToken = jObject["data"]["headpiclist"];
                    if (jToken.Any())
                    {
                        album.CoverUrl = jToken.First["picurl"].ToString();
                    }
                    return album;
                }
                catch (Exception e)
                {
                    return null;
                }
            }));

        
        }
        #endregion
    }
}
