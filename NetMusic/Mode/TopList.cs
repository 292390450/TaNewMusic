using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NetMusic.Mode
{
    public class TopList:INotifyPropertyChanged
    {
        #region 变量

        private HttpClientHelper _helper;
        private ObservableCollection<Song> _songs;
        private string _date;
        private string _name;
        private string _listennum;
        private string _h5CoverUrl;
        private string _albumCoverUrl;
        private string _info;
        private int _cursor;
        private int _topId;
        private int _totalSong;
        #endregion
        #region  属性

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Listennum
        {
            get
            {
                return _listennum;
            }
            set
            {
                _listennum = value;
                OnPropertyChanged();
            }
        }

        public string H5CoverUrl
        {
            get
            {
                return _h5CoverUrl;
            }
            set
            {
                _h5CoverUrl = value;
                OnPropertyChanged();
            }
        }

        public string AlbumCoverUrl
        {
            get
            {
                return _albumCoverUrl;
            }
            set
            {
                _albumCoverUrl = value;
                OnPropertyChanged();
            }
        }

        public int TotalSong
        {
            get
            {
                return _totalSong;
            }
            set
            {
                _totalSong = value;
                OnPropertyChanged();
            }
        }

        public string Info
        {
            get
            {
                return _info;
            }
            set
            {
                _info = value;
                OnPropertyChanged();
            }
        }

        public int TopId
        {
            get { return _topId; }
            set
            {
                _topId = value;
                OnPropertyChanged();
            }
        }
        public int Cursor
        {
            get { return _cursor; }
            set
            {
                _cursor = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Song> Songs
        {
            get { return _songs; }
            set
            {
                _songs = value; 
                OnPropertyChanged();
            }
        }
        #endregion

        public TopList()
        {
            Songs=new ObservableCollection<Song>();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="topId"></param>
        /// <param name="offset"></param>
        /// <param name="num"></param>
        public async Task InitFromJsonAsync(int topId, int offset, int num)
        {
            string res = await HttpClientHelper.GetAsync(MusicApi.GetTopList(topId, offset, num));
            try
            {
                JObject json = JObject.Parse(res);
                Date = json["date"].ToString();
                TopId = topId;
                Cursor = json["cur_song_num"].ToObject<int>();
                TotalSong = json["total_song_num"].ToObject<int>();
                Name = json["topinfo"]["ListName"].ToString();
                Listennum = json["topinfo"]["listennum"].ToString();
                H5CoverUrl = json["topinfo"]["pic_h5"].ToString();
                AlbumCoverUrl = json["topinfo"]["pic_album"].ToString();
                Info = json["topinfo"]["info"].ToString();
                var list = json["songlist"];
                if (list.Any())
                {
                    foreach (var song in list)
                    {
                        Song son = new Song();
                        son.Time = song["data"]["interval"].ToString();
                        var singers = song["data"]["singer"];
                        var oneSinger = singers.First;
                        son.Singer = new Singer()
                        {
                            Id = oneSinger["id"].ToString(),
                            Name = oneSinger["name"].ToString(),
                            MId = oneSinger["mid"].ToString()
                        };
                        son.Id = song["data"]["songid"].ToString();
                        son.MId = song["data"]["songmid"].ToString();
                        son.Name = song["data"]["songname"].ToString();
                        son.Album = new Album()
                        {
                            AlbumId = song["data"]["albumid"].ToString(),
                            Name = song["data"]["albumname"].ToString(),
                            Desc = song["data"]["albumdesc"].ToString(),
                            CoverUrl = ""
                        };
                        Songs.Add(son);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }
    }
}
