using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Controls.Exten
{
    public class BlurMask : Grid
    {

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(string), typeof(BlurMask));
        public static readonly DependencyProperty BlurRadiusProperty = DependencyProperty.Register("BlurRadius", typeof(int), typeof(BlurMask));



        public string Target
        {
            get { return (string)base.GetValue(TargetProperty); }
            set { base.SetValue(TargetProperty, value); }
        }

        public int BlurRadius
        {
            get { return (int)base.GetValue(BlurRadiusProperty); }
            set
            {
                base.SetValue(BlurRadiusProperty, value);
            }
        }

        BlurEffect blur;
        Panel targetPanel;
        Border border;

        public BlurMask()
        {
            border = new Border();
            this.Loaded += BlurMask_Loaded;

        }





        void BlurMask_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (string.IsNullOrEmpty(Target))
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    targetPanel = (Panel)FindName(Target);
                }
                if (targetPanel == null)
                {
                    return;
                }

                if (blur == null || blur.Radius != BlurRadius)
                {
                    blur = new BlurEffect()
                    {
                        Radius = BlurRadius,
                        RenderingBias = RenderingBias.Performance
                    };
                }

                VisualBrush brush = new VisualBrush();
                brush.Visual = targetPanel;
                brush.Stretch = Stretch.Uniform;
                border.Background = brush;
                border.Effect = blur;


                border.Width = targetPanel.ActualWidth;
                border.Height = targetPanel.ActualHeight;

                if (Background == null)
                {
                    this.Background = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
                }
                border.Margin = new Thickness(-this.Margin.Left, -this.Margin.Top, 0, 0);
                this.ClipToBounds = true;
                this.Children.Clear();
                this.Children.Add(border);
            }));
        }


    }
}
