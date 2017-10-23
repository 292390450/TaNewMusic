using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Practices.Unity;
using Music.View;
using NetMusic;
using Prism.Logging;
using Prism.Modularity;
using Prism.Unity;

namespace Music.ViewModel
{
    public class MusicIndexViewModel:ViewModelBase
    {
        #region 变量

        private ObservableCollection<string> _hotKey;
        private IUnityContainer _container;
        private ILoggerFacade _logger;
        private ContentControl _marvellous;
        #endregion
        #region 属性

        public ObservableCollection<string> HotKey
        {
            get { return _hotKey; }
            set
            {
                _hotKey = value;
                RaisePropertyChanged();
            }
        }
        public ContentControl Marvellous
        {
            get { return _marvellous; }
            set
            {
                _marvellous = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region  命令

        public RelayCommand<string> SearchCommand { get; private set; }

        #endregion
        public MusicIndexViewModel(ILoggerFacade logger,IUnityContainer container)
        {
            _logger = logger;
            _container = container;
            Marvellous = container.Resolve<Marvellous>();
            //初始化命令
            SearchCommand=new RelayCommand<string>((pars) =>
            {
                
            });
           //关键字列表
           InitHotKey();
        }

        #region 私有方法
        /// <summary>
        /// 初始化热门搜索关键字
        /// </summary>
        private void InitHotKey()
        {
            //获取热门关键词
            HotKey = new ObservableCollection<string>();
            Func<List<string>> getHotKey = SearchApi.GetHotKey;
            getHotKey.BeginInvoke((ar =>
            {
                var resu = getHotKey.EndInvoke(ar);
                DispatcherHelper.CheckBeginInvokeOnUI((() =>
                {
                    HotKey.AddRange(resu);
                }));
            }), null);
        }
        

        #endregion

    }
}
