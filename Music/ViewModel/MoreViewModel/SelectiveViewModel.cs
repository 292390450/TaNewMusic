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
using Microsoft.Practices.Unity;
using NetMusic.Mode;
using Prism.Modularity;

namespace Music.ViewModel.MoreViewModel
{
    public class SelectiveViewModel:ViewModelBase
    {
        #region 变量
        private IndexData _indexData;
        private IUnityContainer _container;
        private ObservableCollection<Focus> _focuses;
        private ObservableCollection<Hotdiss> _hotdisses;
        private ObservableCollection<Top> _tops;
        #endregion
        #region 属性

        public ObservableCollection<Focus> Focuses
        {
            get { return _focuses; }
            set
            {
                _focuses = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Hotdiss> Hotdisses
        {
            get { return _hotdisses; }
            set
            {
                _hotdisses = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Top> Tops
        {
            get { return _tops; }
            set
            {
                _tops = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region 命令
        public RelayCommand<object> NextDissCommand { get; private set; }
        public RelayCommand<object> PreviousCommand { get; private set; }
        #endregion
        #region 构造

        public SelectiveViewModel(IUnityContainer container)
        {
          
            _container = container;
            //获取焦点 
            _indexData= _container.Resolve<IndexData>();
            Focuses =new ObservableCollection<Focus>( _indexData.Foucuses.Where(x=>x.Type=="10002"));
            Hotdisses=new ObservableCollection<Hotdiss>(_indexData.Hotdisses);
            Tops=new ObservableCollection<Top>(_indexData.Tops);
            //初始化命令
            NextDissCommand=new RelayCommand<object>((obj) =>
            {
                if (obj is ScrollViewer)
                {
                   
                   ((ScrollViewer)obj).PageRight();     
                }
            });
            PreviousCommand=new RelayCommand<object>((obj) =>
            {
                if (obj is ScrollViewer)
                {

                    ((ScrollViewer)obj).PageLeft();
                }
            });
        }

        #endregion
    }
}
