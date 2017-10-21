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
using System.Windows.Shapes;
using Prism.Logging;
using Prism.Modularity;
using TaNewMusic.ModuleHelper;

namespace TaNewMusic
{

    /// <summary>
    /// Shell.xaml 的交互逻辑
    /// </summary>
    public partial class Shell : Window
    {
        private IModuleManager _moduleManager;
        private CallbackLogger _logger;
        public Shell(IModuleManager moduleManager,CallbackLogger logger)
        {
            _moduleManager = moduleManager;
            _logger = logger;
            this.Loaded += Shell_Loaded;
            InitializeComponent();       
        }

        private void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            this._logger.Callback = this.Log;
            this._logger.ReplaySavedLogs();
        }

        public void Log(string message, Category category, Priority priority)
        {
            statusBox.Text=$"[{message}]";
        }

    }
}
