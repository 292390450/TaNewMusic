using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Modularity;
using Prism.Unity;
using Microsoft.Practices.Unity;
using NetMusic;
using NetMusic.Mode;
using TaNewMusic.ModuleHelper;


namespace TaNewMusic
{
    public class TaNewMusicBootstrapper:UnityBootstrapper
    {
        public Action InitCompleted;  
        private readonly CallbackLogger _callbackLogger=new CallbackLogger();
        SongApi _songApi=new SongApi();
        IndexData _indexData=new IndexData();
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            this.Container.RegisterInstance<CallbackLogger>(this._callbackLogger);
            Container.RegisterInstance<SongApi>(this._songApi);
            Container.RegisterInstance<IndexData>(this._indexData);
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
            InitCompleted();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return this._callbackLogger;
        }

        protected override DependencyObject CreateShell()
        {

            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            //音乐模块
            ModuleCatalog.AddModule(new ModuleInfo(typeof(Music.MusicModule).Name,typeof(Music.MusicModule).AssemblyQualifiedName));
            //社区
            ModuleCatalog.AddModule(new ModuleInfo(typeof(Community.CommunityModule).Name,typeof(Community.CommunityModule).AssemblyQualifiedName));
            //用户模块
            ModuleCatalog.AddModule(new ModuleInfo(typeof(User.UserModule).Name,typeof(User.UserModule).AssemblyQualifiedName));
            
        }
        
        
    }
}
