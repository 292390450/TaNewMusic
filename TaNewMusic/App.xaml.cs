using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using NetMusic.Mode;
using Prism.Unity;

namespace TaNewMusic
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherHelper.Initialize();
            base.OnStartup(e);

          
            TaNewMusicBootstrapper bootstrapper = new TaNewMusicBootstrapper();
            bootstrapper.InitCompleted = () =>
            {
                Action waiteAction = () =>
                {
                    var indexdata=bootstrapper.Container.TryResolve<IndexData>();
                    Action getIndexData = indexdata.GetIndexData;
                    getIndexData.BeginInvoke(null, null);
                    Thread.Sleep(2000);
                    
                };
                waiteAction.BeginInvoke((ar =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI((() =>
                    {
                        bootstrapper.Container.TryResolve<MainWindow>().Show();
                    }));
                }), null);

            };
            bootstrapper.Run();
         
        }
    }
}
