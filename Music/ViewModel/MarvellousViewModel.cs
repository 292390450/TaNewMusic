using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using NetMusic;
using NetMusic.Mode;
using Prism.Logging;
using Prism.Modularity;

namespace Music.ViewModel
{
    public class MarvellousViewModel:ViewModelBase
    {
        #region 变量

        private IUnityContainer _container;
        private ObservableCollection<Song> _coverSongs;
        private ILoggerFacade _logger;
        #endregion
        #region 属性

        public ObservableCollection<Song> CoverSongs
        {
            get { return _coverSongs; }
            set
            {
                _coverSongs = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        public MarvellousViewModel(ILoggerFacade logger,IUnityContainer container)
        {
            _logger = logger;
            _logger.Log("精彩ViewModel构造",Category.Debug,Priority.Low);
            //实例化songApi
            _container = container;

            SongApi songApi = _container.Resolve<SongApi>();
            Action initAction=new Action( songApi.InitAllList);
            initAction.BeginInvoke((ar =>
            {
               
                CoverSongs = songApi.GetCover();
                foreach (var coverSong in CoverSongs)
                {
                    coverSong.Album.GetAllInfor.BeginInvoke(string.Empty, null, null);
                }
            }), null);
            
        }
    
    }
}
