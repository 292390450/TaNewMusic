using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;

namespace Music
{
    [Module(OnDemand = true)]
    public class MusicModule:IModule
   {
       private readonly ILoggerFacade _callbackLogger;
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        public MusicModule(IUnityContainer container, IRegionManager regionManager,ILoggerFacade callbackLogger)
        {
            this._container = container;
            this._regionManager = regionManager;
            this._callbackLogger = callbackLogger;
            _callbackLogger.Log("构建音乐模块",Category.Debug,Priority.Low);
        }
        public void Initialize()
        {
            _callbackLogger.Log("音乐模块初始化完成",Category.Debug,Priority.Low);
        }
    }
}
