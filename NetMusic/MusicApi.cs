
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMusic.Mode;
using Newtonsoft.Json.Linq;

namespace NetMusic
{
    public class MusicApi
    {
        private const string BaseAddress = "https://c.y.qq.com";

        private const string MustParas =
            "g_tk=5381&loginUin=0&hostUin=0&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
        private const string MusicianUri =
                "https://c.y.qq.com/v8/fcg-bin/v8.fcg?channel=singer&page=list&g_tk=5381&loginUin=0&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0"
            ;

        private const string KeySugges =
                "https://c.y.qq.com/splcloud/fcgi-bin/smartbox_new.fcg?is_xml=0&format=json&g_tk=5381&loginUin=0&hostUin=0&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0"
            ;

        private const string MusicSearch =
            "https://c.y.qq.com/soso/fcgi-bin/client_search_cp?format=json&ct=24&qqmusic_ver=1298&new_json=1&t=0&aggr=1&cr=1&catZhida=1&lossless=0&flag_qc=0&g_tk=5381&loginUin=0&hostUin=0&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";

        private const string MusicianMusic = BaseAddress +
                                             "/v8/fcg-bin/fcg_v8_singer_track_cp.fcg?format=json&order=listen&songstatus=1&" +
                                             MustParas;

        private const string SingerAlbum = BaseAddress + "/v8/fcg-bin/fcg_v8_singer_album.fcg?format=json&order=time&" +
                                           MustParas;

        private const string MusicInfo = BaseAddress +
                                         "/v8/fcg-bin/fcg_play_single_song.fcg?tpl=yqq_song_detail&format=json&g_tk=1885845528&" +
                                         MustParas;

        private const string MusicAlbum = BaseAddress +
                                          "/rcmusic/fcgi-bin/fcg_iphone_music_rec_songlist?cid=338&ct=20&uin=10000&&g_tk=1885845528&" +
                                          MustParas;

        private const string AlbumInfo = BaseAddress + "/v8/fcg-bin/fcg_v8_album_info_cp.fcg?" + MustParas;
        /// <summary>
        /// 获取前一百位受欢迎的歌手，参数貌似无效
        /// </summary>
        /// <param name="pagenum">页码</param>
        /// <param name="pagesize">一页大小一般一页100，后面页码大小可以定制</param>
        /// <param name="type"></param>
        /// <param name="head">A-Z或all</param>
        /// <returns></returns>
        public static string GetMusicianUrl(int pagenum, int pagesize,MusicianType type,string head)
        {
            return $"{MusicianUri}&pagesize={pagesize}&pagenum={pagenum}&key={type}_{head}";
        }
        /// <summary>
        /// 根据关键字搜索的提示
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string GetKeySuggesUrl(string key)
        {
            return $"{KeySugges}&key={key}";
        }
        /// <summary>
        /// 根据关键字搜索歌曲
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pagesize">一页大小，第一页27个后面页大小可控</param>
        /// <param name="pagenum"></param>
        /// <returns></returns>
        public static string GetMusicSearchUrl(string key, int pagesize, int pagenum)
        {
            return $"{MusicSearch}&remoteplace=txt.yqq.center&w={key}&p={pagenum}&n={pagesize}";
        }
        /// <summary>
        /// 获取音乐人的歌曲
        /// </summary>
        /// <param name="singermid">音乐人的mid</param>
        /// <param name="offset">偏移的数目</param>
        /// <param name="pagesize">返回的大小</param>
        /// <returns></returns>
        public static string GetMusicianMusicUrl(string singermid, int offset, int pagesize)
        {
            return $"{MusicianMusic}&singermid={singermid}&begin={offset}&num={pagesize}";
        }
        /// <summary>
        /// 获取歌手专辑
        /// </summary>
        /// <param name="singermid"></param>
        /// <param name="offset"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static string GetSingerAlbum(string singermid, int offset, int pagesize)
        {
            return $"{SingerAlbum}&singermid={singermid}&begin={offset}&num={pagesize}";
        }
        /// <summary>
        /// 获取歌手描述
        /// </summary>
        /// <param name="singermid"></param>
        /// <returns></returns>
        public static string GetSingerDesric(string singermid)
        {

            return
                $"{BaseAddress}/splcloud/fcgi-bin/fcg_get_singer_desc.fcg?utf8=1&outCharset=utf-8&format=xml&singermid={singermid}&r=" + GetTimeLikeJs();
        }
        /// <summary>
        /// 获取各个分类排行
        /// </summary>
        /// <returns></returns>
        public static string GetTopClassi()
        {
            return BaseAddress +
                   "/v8/fcg-bin/fcg_v8_toplist_opt.fcg?page=index&format=html&tpl=macv4&v8debug=1";
            ;
         }
        /// <summary>
        /// 获取分类的排行数据
        /// </summary>
        /// <param name="topId">3 欧美 4:流行指数 5：内地 6 港台 16 韩国 26：热歌 27：新歌 28：网络歌曲 36：网友k歌</param>
        /// <param name="offset"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetTopList(int topId,int offset,int num)
        {
           return $"https://c.y.qq.com/v8/fcg-bin/fcg_v8_toplist_cp.fcg?tpl=3&page=detail&topid={topId}&type=top&song_begin={offset}&song_num={num}&g_tk=5381&loginUin=0&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
        }
        /// <summary>
        /// 热门关键词的url
        /// </summary>
        /// <returns></returns>
        public static string GetHotKey()
        {
            return BaseAddress + "/splcloud/fcgi-bin/gethotkey.fcg?" + MustParas;
        }
   
        /// <summary>
        /// 获取歌曲信息
        /// </summary>
        /// <param name="musicId">mid</param>
        /// <returns></returns>
        public static string GetMusicInfo(string musicId)
        {
            return $"{MusicInfo}&songmid={musicId}";
        }
        /// <summary>
        /// 获取歌曲的专辑
        /// </summary>
        /// <param name="musicId">Musicid</param>
        /// <returns></returns>
        public static string GetMusicAlblum(string musicId)
        {
            return $"{MusicAlbum}&songid={musicId}";
        }
        /// <summary>
        /// 获取专辑信息
        /// </summary>
        /// <param name="albumid">id</param>
        /// <returns></returns>
        public static string GetAlbumInfo(string albumid)
        {
            return
                $"https://c.y.qq.com/v8/fcg-bin/musicmall.fcg?cmd=get_album_buy_page&albumid={albumid}&p=0.8817215290873384&g_tk=5381&loginUin=0&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
        }
        /// <summary>
        /// 首页推荐
        /// </summary>
        /// <returns></returns>
        public static string GetIndexList()
        {
            //0-1随机数
            Random ran=new Random();
            double r1 = ran.NextDouble();
            double r2 = Math.Pow(10, 16);
            string r3 = Math.Ceiling(Convert.ToDecimal(r1 * r2)).ToString();
            return $"{BaseAddress}/v8/fcg-bin/fcg_first_yqq.fcg?format=json&tpl=v12&page=other&{MustParas}&rnd={r3}";
        }
        #region 额外

        private static long GetTimeLikeJs()
        {
            DateTime d2 = Convert.ToDateTime("1970-01-01");
            DateTime dt = DateTime.UtcNow;
            TimeSpan span = dt - d2;
            string t = span.TotalMilliseconds.ToString("0");
            long st = long.Parse(t);
            return st;
        }

        #endregion
    }
}
