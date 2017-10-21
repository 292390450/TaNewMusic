using System.Windows.Controls;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using Music.View;
using Prism.Logging;

namespace TaNewMusic.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region ±‰¡ø

        private ContentControl _rightControl;

        private IUnityContainer _container;
        #endregion
        #region  Ù–‘

        public ContentControl RightControl
        {
            get { return _rightControl; }
            set
            {
                _rightControl = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ILoggerFacade logger,IUnityContainer container)
        {
            _container = container;
            RightControl = _container.Resolve<MusicIndexView>();
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}