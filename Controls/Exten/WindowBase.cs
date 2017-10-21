using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controls.Annotations;
using Microsoft.Expression.Interactivity.Core;

namespace Controls.Exten
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:Controls;assembly=Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:WindowBase/>
    ///
    /// </summary>
    public class WindowBase : Window,INotifyPropertyChanged
    {

        #region 变量

        private Visibility _restoreButtonVisibility=Visibility.Collapsed;
        private Visibility _maxButtonVisibility=Visibility.Visible;
        #endregion

        #region 属性

        public Visibility RestoreButtonVisibility
        {
            get { return _restoreButtonVisibility; }
            set
            {
                _restoreButtonVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility MaxButtonVisibility
        {
            get { return _maxButtonVisibility; }
            set
            {
                _maxButtonVisibility = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region 依赖属性

        public static readonly DependencyProperty IconImageProperty = DependencyProperty.Register(
            "IconImage", typeof(ImageSource), typeof(WindowBase), new PropertyMetadata(default(ImageSource),new PropertyChangedCallback((
                (o, args) =>
                {
                    if (args.NewValue!=args.OldValue)
                    {
                        
                    }
                   
                } ))));

        public ImageSource IconImage
        {
            get { return (ImageSource) GetValue(IconImageProperty); }
            set { SetValue(IconImageProperty, value); }
        }

        #endregion
        #region 命令
        public ICommand MinCommand { get; private set; }
        public  ICommand CloseWindowCommand { get; private set; }
        public  ICommand SizeChangedCommand { get; private set; }
        public ICommand WindowMoveCommand { get; private set; }
        public ICommand MaxMinCommand { get; private set; } 
        #endregion

        #region 构造

        static WindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowBase), new FrameworkPropertyMetadata(typeof(WindowBase)));
        }

        public WindowBase()
        {
           MinCommand=new ActionCommand(() =>
           {
               WindowState = WindowState.Minimized;
           });
            MaxMinCommand = new ActionCommand(() =>
            {
                if (WindowState==WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    MaxButtonVisibility = Visibility.Visible;
                    RestoreButtonVisibility = Visibility.Collapsed;
                }
                else if(WindowState==WindowState.Normal)
                {
                    WindowState = WindowState.Maximized;
                    MaxButtonVisibility = Visibility.Collapsed;
                    RestoreButtonVisibility = Visibility.Visible;
                }
            });
            WindowMoveCommand=new ActionCommand(() =>
            {
                this.DragMove();
            });
            SizeChangedCommand=new ActionCommand(() =>
            {
                this.Clip = new RectangleGeometry(new Rect(new Size(Width, Height)), 3, 3);
            });
            CloseWindowCommand=new ActionCommand(() =>
            {
                this.Close();
            });
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
