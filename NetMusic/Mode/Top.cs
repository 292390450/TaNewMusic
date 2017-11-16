using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using NetMusic.Annotations;

namespace NetMusic.Mode
{
    public class Top:INotifyPropertyChanged
    {
        #region  变量
        private string _listName;
        private string _macDetailPicUrl;
        private string _headPic_v12;
        private string _listenNum;
        private string _showTime;
        private ObservableCollection<Song> _songList;


        #endregion

        #region  属性

        public string ListName
        {
            get { return _listenNum; }
            set
            {
                _listenNum = value;
                OnPropertyChanged();
            }
        }

        public string MacDetailPicUrl
        {
            get { return _macDetailPicUrl; }
            set
            {
                _macDetailPicUrl = value;
                OnPropertyChanged();
            }
        }

        public string HeadPic_v12
        {
            get { return _headPic_v12; }
            set
            {
                _headPic_v12 = value;
                OnPropertyChanged();
            }
        }

        public string ListenNum
        {
            get { return _listenNum; }
            set
            {
                _listenNum = value;
                OnPropertyChanged();
            }
        }

        public string ShowTime
        {
            get { return _showTime; }
            set
            {
                _showTime = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Song> SongList
        {
            get { return _songList; }
            set
            {
                _songList = value;
                OnPropertyChanged();
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
