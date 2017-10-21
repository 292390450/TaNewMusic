using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;

namespace Community
{
    public class CommunityModule:IModule
    {
        private readonly ILoggerFacade _callbackLogger;
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public CommunityModule(IUnityContainer container, IRegionManager regionManager, ILoggerFacade callbackLogger)
        {
            this._container = container;
            this._regionManager = regionManager;
            this._callbackLogger = callbackLogger;
            _callbackLogger.Log("构建社区模块", Category.Debug, Priority.Low);
        }
        public void Initialize()
        {
            _callbackLogger.Log("社区模块初始化完成", Category.Debug, Priority.Low);
        }
    }
}
