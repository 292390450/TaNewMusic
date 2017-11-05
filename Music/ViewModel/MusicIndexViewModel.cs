using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private string _selecteItem;
        private string _searchPara;
        private ObservableCollection<string> _suggests;
        private ObservableCollection<string> _hotKey;
        private IUnityContainer _container;
        private ILoggerFacade _logger;
        private ContentControl _marvellous;
        private Visibility _searchPanelVisibility=Visibility.Collapsed;
        #endregion
        #region 属性

        public string SelectedItem
        {
            get { return _selecteItem; }
            set
            {
                _selecteItem = value;
                RaisePropertyChanged();
            }
        }
        public string SearchPara
        {
            get { return _searchPara; }
            set
            {
                _searchPara = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<string> Suggests
        {
            get { return _suggests; }
            set
            {
                _suggests = value;
                RaisePropertyChanged();
            }
        }
        public Visibility SearchPanelVisibility
        {
            get { return _searchPanelVisibility; }
            set
            {
                _searchPanelVisibility = value;
                RaisePropertyChanged();
            }
        }
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
        public RelayCommand<string> TextChangedCommand { get; private set; }
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
                if (!string.IsNullOrEmpty(pars))
                {
                    SearchPara = pars;
                    //转到搜索页
                }
                else
                {
                    if (!string.IsNullOrEmpty(SelectedItem))
                    {
                        SearchPara = SelectedItem;
                        //转
                        //清空选中
                        SelectedItem = null;
                    }
                }
            });
            TextChangedCommand=new RelayCommand<string>((str) =>
            {
                    SearchPanelVisibility = Visibility.Visible;
                    UpdateSearchSuggest(str);
                
               
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
        /// <summary>
        /// 更新搜索提示
        /// </summary>
        private void UpdateSearchSuggest(string para)
        {
            if (Suggests==null)
            {
                Suggests=new ObservableCollection<string>();
            }
            SearchPanelVisibility = Visibility.Collapsed;
            Suggests.Clear();
            if (string.IsNullOrEmpty(para))
            {   
                return;
            }
            Func<string, List<string>> suggests = SearchApi.GetKeySuggest;
            suggests.BeginInvoke(para, (ar =>
            {
                var resu = suggests.EndInvoke(ar);
                if (resu.Any())
                {
                    DispatcherHelper.CheckBeginInvokeOnUI((() =>
                    {
                        if (resu.Count>15)
                        {
                            var sds = resu.Take(15);
                            Suggests.AddRange(sds);
                        }
                        else
                        {
                            Suggests.AddRange(resu);
                        }
                        SearchPanelVisibility = Visibility.Visible;
                    }));
                }
            }),null);
        }
        #endregion

    }
}
