using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Controls.Exten
{
    public class TaNewBarHelper
    {
     
        public static readonly DependencyProperty HasTaNewBarProperty = DependencyProperty.RegisterAttached(
            "HasTaNewBar", typeof(bool), typeof(TaNewBarHelper), new PropertyMetadata(default(bool),HasBarPrpertyCallBack));

        public static void SetHasTaNewBar(DependencyObject element, bool value)
        {
            element.SetValue(HasTaNewBarProperty, value);
        }

        public static bool GetHasTaNewBar(DependencyObject element)
        {
            return (bool) element.GetValue(HasTaNewBarProperty);
        }

        public static void HasBarPrpertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {

                var scrollPanel = d as ScrollPanel;
                if (scrollPanel != null)
                    scrollPanel.Loaded += (o, args) =>
                    {
                        var element = d as Visual;
                        var praent = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(d));
                        if (element != null)
                        {
                            var adornerLayer = AdornerLayer.GetAdornerLayer(praent as Visual);

                            if (adornerLayer != null)
                            {
                                adornerLayer.Add(new TaNewBarAdorner(element as UIElement,
                                    new TaNewScrollBar()));
                            }
                        }
                    };
            }
        }
    }
}
