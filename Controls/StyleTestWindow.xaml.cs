using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controls.Exten;

namespace Controls
{
    /// <summary>
    /// StyleTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StyleTestWindow : Window
    {
        public StyleTestWindow()
        {
            InitializeComponent();
        
            
           
        }

        

        void onDragDelta(object sender, DragDeltaEventArgs e)
        {
            ////Move the Thumb to the mouse position during the drag operation
            //double yadjust =  + e.VerticalChange;
            //double xadjust = + e.HorizontalChange;
         
               
            //    Canvas.SetLeft(myThumb, Canvas.GetLeft(myThumb) +
            //                            e.HorizontalChange);
            //    Canvas.SetTop(myThumb, Canvas.GetTop(myThumb) +
            //                           e.VerticalChange);
                
         
        }


        private void StyleTestWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var bs = ScrollPanel.ViewportHeight;
        }
    }
}
