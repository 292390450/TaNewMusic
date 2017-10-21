using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Controls.Exten
{
    public class TextBoxHelper
    {
        public static readonly DependencyProperty TextCommandProperty = DependencyProperty.RegisterAttached(
            "TextCommand", typeof(ICommand), typeof(TextBoxHelper), new PropertyMetadata(default(ICommand),new PropertyChangedCallback((
                (o, args) =>
                {
                    
                }))));

        public static void SetTextCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(TextCommandProperty, value);
        }

        public static ICommand GetTextCommand(DependencyObject element)
        {
            return (ICommand) element.GetValue(TextCommandProperty);
        }
    }
}
