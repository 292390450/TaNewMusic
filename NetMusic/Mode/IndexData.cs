using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using NetMusic.Annotations;
using Newtonsoft.Json.Linq;

namespace NetMusic.Mode
{
    public class IndexData:INotifyPropertyChanged
    {
        #region 变量

        private ObservableCollection<Focus> _foucuses;
        private ObservableCollection<Hotdiss> _hotdisses;
        private ObservableCollection<Top> _tops;

        #endregion

        #region  属性

        public ObservableCollection<Focus> Foucuses
        {
            get { return _foucuses; }
            set
            {
                _foucuses = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Hotdiss> Hotdisses
        {
            get { return _hotdisses; }
            set
            {
                _hotdisses = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Top> Tops
        {
            get { return _tops; }
            set
            {
                _tops = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region  构造函数

        public IndexData()
        {
            //初始化列表
            Foucuses=new ObservableCollection<Focus>();
            Hotdisses=new ObservableCollection<Hotdiss>();
            Tops=new ObservableCollection<Top>();
        }

        #endregion
        #region 外部方法
        /// <summary>
        /// 同步方式获取首页的数据
        /// </summary>
        public void GetIndexData()
        {
            string indexUrl = MusicApi.GetIndexList();
            string indexRes = HttpClientHelper.GetAsync(indexUrl).Result;
            try
            {
                JObject resulJObject=JObject.Parse(indexRes);
                JToken foucuses = resulJObject["data"]["focus"];
                JToken hotdisses = resulJObject["data"]["hotdiss"]["list"];
                JToken tops = resulJObject["data"]["toplist"];
                if (foucuses!=null)
                {
                    foreach (var foucuse in foucuses)
                    {
                       DispatcherHelper.CheckBeginInvokeOnUI((() =>
                       {
                           Foucuses.Add(new Focus()
                           {
                               Fid = foucuse["fid"].ToString(),
                               Id = foucuse["id"].ToString(),
                               JumpUrl = foucuse["jumpurl"].ToString(),
                               PicUrl = foucuse["pic"].ToString(),
                               Title = foucuse["title"].ToString(),
                               Type = foucuse["type"].ToString()
                           });
                       }));
                    }
                }
                if (hotdisses!=null)
                {
                    foreach (var hotdiss in hotdisses)
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI((() =>
                        {
                            Hotdisses.Add(new Hotdiss()
                            {
                                Author = hotdiss["author"].ToString(),
                                DissId = hotdiss["dissid"].ToString(),
                                DissName = hotdiss["dissname"].ToString(),
                                ImgUrl = hotdiss["imgurl"].ToString(),
                                ListenNum = hotdiss["listennum"].ToString()
                            });
                        }));
                    }
                }
                if (tops!=null)
                {
                    foreach (var top in tops)
                    {
                        JToken songs = top["songlist"];
                        List<Song> songList=new List<Song>();
                        foreach (var song in songs)
                        {
                            songList.Add(new Song()
                            {
                                Singer = new Singer()
                                {
                                    Id = song["singerid"].ToString(),
                                    Name = song["singername"].ToString(),
                                },
                                Id = song["songid"].ToString(),
                                Name = song["songname"].ToString()
                            });
                        }
                        DispatcherHelper.CheckBeginInvokeOnUI((() =>
                        {
                            Tops.Add(new Top()
                            {
                                HeadPic_v12 = top["headPic_v12"].ToString(),
                                ListenNum = top["listennum"].ToString(),
                                ListName = top["ListName"].ToString(),
                                MacDetailPicUrl = top["MacDetailPicUrl"].ToString(),
                                ShowTime = top["showtime"].ToString(),
                                SongList = new ObservableCollection<Song>(songList)
                            });
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        

        #endregion
        #region properchanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
