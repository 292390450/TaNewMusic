using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Controls.Annotations;
using GalaSoft.MvvmLight.Command;

namespace Controls.Exten
{
    public class TaNewScrollBar:Thumb,INotifyPropertyChanged
    {
        
        #region 变量
        
        private ScrollPanel _scrollPanel;
        private double _topValue=5;
        private double _barHeight;
        private double _thumpHeight;
        private double _thumpWidth;
        #endregion

        #region  属性

        
        public double TopValue
        {
            get
            {
                return _topValue;
                
            }
            set
            {
                _topValue = value;
                OnPropertyChanged();
            }
        }

     
        public double ThumpHeight
        {
            get { return _thumpHeight; }
            set
            {
                _thumpHeight = value;
                OnPropertyChanged();
            }
        }

        public double ThumpWidth
        {
            get { return _thumpWidth; }
            set
            {
                _thumpWidth = value;
                OnPropertyChanged();
            }
        }

        public double BarHeight
        {
            get { return _barHeight; }
            set
            {
                _barHeight = value;
                OnPropertyChanged();
            }
        }
        #region 依赖属性

        public static readonly DependencyProperty ScrollPanelProperty = DependencyProperty.Register(
            "ScrollPanel", typeof(ScrollPanel), typeof(TaNewScrollBar), new PropertyMetadata(default(ScrollPanel),
                (o, args) =>
                {
                  
                } ));

        public ScrollPanel ScrollPanel
        {
            get { return (ScrollPanel) GetValue(ScrollPanelProperty); }
            set { SetValue(ScrollPanelProperty, value); }
        }

        public static readonly DependencyProperty BarTopOffsetProperty = DependencyProperty.Register(
            "BarTopOffset", typeof(double), typeof(TaNewScrollBar), new PropertyMetadata(default(double)));

        public double BarTopOffset
        {
            get { return (double) GetValue(BarTopOffsetProperty); }
            set
            {
                SetValue(BarTopOffsetProperty, value);
            }
        }
        #endregion
        #endregion

        #region  命令

        public RelayCommand<DragDeltaEventArgs> DragDeltaCommand { get; private set; }

        #endregion
        public TaNewScrollBar()
        {
            this.Loaded += TaNewScrollBar_Loaded;
            //_scrollPanel = scrollPanel;
         
            //BindingOperations.SetBinding(this, TaNewScrollBar.ScrollPanelProperty, new Binding() { Source = _scrollPanel ,UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

       
            this.Width = 10 + 2;
            this.Background = Brushes.Transparent;
            DragDeltaCommand=new RelayCommand<DragDeltaEventArgs>((e) =>
            {
                double yadjust = e.VerticalChange;
                //double xadjust = e.HorizontalChange;
               
                if (e.Source is Thumb)
                {
                    if (!(((Thumb)e.Source).RenderTransform.Value.OffsetY+e.VerticalChange<=0)&& !(((Thumb)e.Source).RenderTransform.Value.OffsetY + e.VerticalChange >= this.BarHeight-ThumpHeight))
                    {
                        //Canvas.SetTop((Thumb)e.Source, Canvas.GetTop((Thumb)e.Source) +
                        //                               e.VerticalChange);

                        ScrollPanel.VerticalChanged(ScrollPanel.ExtentHeight*yadjust/BarHeight);
                    }                
                   
                }

            });
        }

        private void TaNewScrollBar_Loaded(object sender, RoutedEventArgs e)
        {
            ThumpHeight = ScrollPanel.ViewportHeight * ScrollPanel.ViewportHeight / ScrollPanel.ExtentHeight;
            ThumpWidth = 10;
            BarHeight = ScrollPanel.ViewportHeight;
            ScrollPanel.ViewPortChanged = (scSize, toSzie) =>
            {
                BarHeight = scSize.Height;
                ThumpHeight = scSize.Height * scSize.Height / toSzie.Height;
            };
        }

        #region 方法
        /// <summary>
        /// 外部滚动调bar滚动
        /// </summary>
        /// <param name="yAdjust">Y轴面板滚动距离</param>
        public static void AnimationScroll(double yAdjust)
        {
            
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }
    }

    public class VeOffsetToTopConvert : IMultiValueConverter
    {
        private TranslateTransform _translate=new TranslateTransform();
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length==4)
            {
                var vertic= (double)values[0] * (double)values[2] / (double)values[1];
                if (values[3] is Thumb&&!double.IsNaN(vertic))
                {
                    var yOffsetAnimation = new DoubleAnimation()
                    {
                        To = vertic,
                        Duration = TimeSpan.FromSeconds(0.1),
                        AccelerationRatio=0.2
                    };
                    ((Thumb) values[3]).RenderTransform = _translate;
                    _translate.BeginAnimation(TranslateTransform.YProperty, yOffsetAnimation);
                }
                return Brushes.Gray;
                //ScrollPanel panel = (ScrollPanel)value;
                //return panel.VerticalOffset * panel.ViewportHeight / panel.ExtentHeight;
            }
            return Brushes.Gray;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TaNewBarAdorner:Adorner
    {
        private TaNewScrollBar _taNewScrollBar;
        private VisualCollection _visuals;
        private Pen pen = new Pen(Brushes.Red, 1.0);
        public TaNewBarAdorner([NotNull] UIElement adornedElement,TaNewScrollBar taNewScrollBar) : base(adornedElement)
        {
            _taNewScrollBar = taNewScrollBar;
            this.AddVisualChild(_taNewScrollBar);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect adornedElementDesiredRect = new Rect(this.AdornedElement.DesiredSize);
            drawingContext.DrawRectangle(null, pen, adornedElementDesiredRect);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            this._taNewScrollBar.Arrange(new Rect(finalSize));
            return finalSize;
        }
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
        protected override Visual GetVisualChild(int index)
        {
            return this._taNewScrollBar;
        }
    }
}
