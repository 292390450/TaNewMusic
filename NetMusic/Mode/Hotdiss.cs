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
    public class Hotdiss:INotifyPropertyChanged
    {
        #region  变量

        private string _author;
        private string _dissId;
        private string _dissName;
        private string _imgUrl;
        private string _listenNum;

        #endregion

        #region  属性

        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public string DissId
        {
            get { return _dissId; }
            set
            {
                _dissId = value;
                OnPropertyChanged();
            }
        }

        public string DissName
        {
            get { return _dissName; }
            set
            {
                _dissName = value;
                OnPropertyChanged();
            }
        }

        public string ImgUrl
        {
            get { return _imgUrl; }
            set
            {
                _imgUrl = value;
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
