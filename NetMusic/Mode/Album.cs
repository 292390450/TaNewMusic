using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Threading;

namespace NetMusic.Mode
{
    public class Album:INotifyPropertyChanged
    {
        #region 变量
        
        private string _coverUrl;
        private BitmapImage _cover;
        #endregion

        public Action<string> GetAllInfor;
        public string Name { get; set; }
        public string AlbumId { get; set; }
        public string Companyname { get; set; }
        public string Desc { get; set; }

        public BitmapImage Cover
        {
            get { return _cover; }
            set
            {
                _cover = value;
                OnPropertyChanged();
            }
        }
        public string CoverUrl
        {
            get
            {
                return _coverUrl;
            }
            set
            {
                _coverUrl = value;
                OnPropertyChanged();
            }
        }

        public string PubTime { get; set; }
        public Singer Singer { get; set; }
        public List<Song> SongList { get; set; }

        public Album()
        {
            GetAllInfor=new Action<string>(CoverUrlChangedAsync);
        }

        #region 私有
        /// <summary>
        /// 专辑的封面地址改变时重新下载图像
        /// </summary>
        private void CoverUrlChangedAsync(string url)
        {
            if (string.IsNullOrEmpty(CoverUrl))
            {
              var alaalbum=AlbumApi.InitBriefInfoAlbumAsync(AlbumId).Result ;
                Companyname = alaalbum.Companyname;
                Desc = alaalbum.Desc;
                _coverUrl = alaalbum.CoverUrl;
                PubTime = alaalbum.PubTime;
                Bitmap bit1 = TaNewHelper.ImageHelper.DownAsync(alaalbum.CoverUrl).Result;
              DispatcherHelper.CheckBeginInvokeOnUI((() => Cover = TaNewHelper.ImageHelper.GetBitmapImage(bit1)));
            }
            else
            {
                Bitmap bit = TaNewHelper.ImageHelper.DownAsync(url).Result;
                DispatcherHelper.CheckBeginInvokeOnUI((() => Cover = TaNewHelper.ImageHelper.GetBitmapImage(bit)));
            }
        
        }

        #endregion
        #region property


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
