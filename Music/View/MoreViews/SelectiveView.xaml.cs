using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Music.ViewModel.MoreViewModel;

namespace Music.View.MoreViews
{
    /// <summary>
    /// SelectiveView.xaml 的交互逻辑
    /// </summary>
    public partial class SelectiveView : UserControl
    {
        public SelectiveView(SelectiveViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void ItemsControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            ItemsControl.RaiseEvent(eventArg);
        }

       
    }
}
