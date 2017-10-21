using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMusic;
using NetMusic.Mode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Imaging;
using TaNewHelper;

namespace CommonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var swe = MusicApi.GetHotKey();
            var sdsad = HttpClientHelper.GetAsync(swe).Result;
            var sds = AlbumApi.InitBriefInfoAlbumAsync("2265047").Result;
            //var sss =
            //    MusicApi.GetSingerAlbum("000GGDys0yA0Nk", 0, 10);
            var bir=ImageHelper.DownAsync("http://y.gtimg.cn/music/photo_new/T002R500x500M000004Z9rf305fzyj.jpg").Result;
            SongApi songApi=new SongApi();
            songApi.InitAllListAsync().Wait();
            var lis = songApi.GetCover();
            foreach (var song in lis)
            {
                song.Album.GetAllInfor.BeginInvoke(string.Empty, null, null);
            }
            TopList top=new TopList();
            top.InitFromJsonAsync(4, 0, 10).Wait();
            var sss = MusicApi.GetTopList(4,0,30);
           
            var ssss = HttpClientHelper.GetAsync(sss).Result;

            JObject json=JObject.Parse(ssss);
            var list = json["songlist"];
            foreach (var VARIABLE in list)
            {
                
            }
            var str = MusicApi.GetKeySuggesUrl("林俊");
            var ss = HttpClientHelper.GetAsync(str).Result;
            var s = MusicApi.GetMusicianUrl(1, 200,NetMusic.Mode.MusicianType.cn_man,"L");
                
            var tt = HttpClientHelper.GetAsync(s).Result;
        }
    }
}
