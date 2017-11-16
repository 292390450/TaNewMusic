using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Unity;
using Music.View.MoreViews;
using NetMusic;
using NetMusic.Mode;
using Prism.Logging;

namespace Music.ViewModel
{
    public class MarvellousViewModel:ViewModelBase
    {
        #region 变量

        private UserControl _selectiveView;
        private UserControl _rankingView;
        private UserControl _songSheetView;
        private UserControl _currentControl;
        private IUnityContainer _container;
        private ObservableCollection<Song> _coverSongs;
        private ILoggerFacade _logger;
        #endregion
        #region 属性

        public UserControl CurrentControl
        {
            get { return _currentControl; }
            set
            {
                _currentControl = value;
                RaisePropertyChanged();
            }
        }
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
        #region 命令
        public RelayCommand<string> TabChangedCommand { get; private set; }
        #endregion
        public MarvellousViewModel(ILoggerFacade logger,IUnityContainer container)
        {
            _logger = logger;
      
            _container = container;
            //命令初始化
            TabChangedCommand=new RelayCommand<string>((str) =>
            {
                switch (str)
                {
                    case "精选":
                        CurrentControl = _selectiveView;
                        break;
                    case "排行":
                        CurrentControl = _rankingView;
                        break;
                    case "歌单":
                        CurrentControl = _songSheetView;
                        break;
                    default:
                        break;
                }
            });
            //页面初始化
            _selectiveView = _container.Resolve<View.MoreViews.SelectiveView>();
            _rankingView = _container.Resolve<RankingView>();
            _songSheetView = _container.Resolve<SongSheetView>();
            //默认当前页
            CurrentControl = _selectiveView;
            //实例化songApi
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

            _logger.Log("精彩ViewModel构造", Category.Debug, Priority.Low);
        }
    
    }
}
