using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NetMusic.Annotations;

namespace NetMusic.Mode
{
    public class Focus:INotifyPropertyChanged
    {
        #region 变量

        private string _fid;
        private string _id;
        private string _jumpUrl;
        private string _picUrl;
        private string _title;
        private string _type;

        #endregion

        #region  属性

        public string Fid
        {
            get { return _fid; }
            set
            {
                _fid = value;
                OnPropertyChanged();
            }
        }

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string JumpUrl
        {
            get { return _jumpUrl; }
            set
            {
                _jumpUrl = value;
                OnPropertyChanged();
            }
        }

        public string PicUrl
        {
            get { return _picUrl; }
            set
            {
                _picUrl = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value;OnPropertyChanged(); }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
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
