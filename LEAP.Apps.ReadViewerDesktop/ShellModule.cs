using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEAP.Apps.ReadViewerDesktop.Core.Navigation;
using LEAP.Apps.ReadViewerDesktop.Core.Services;
using LEAP.Apps.ReadViewerDesktop.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LEAP.Apps.ReadViewerDesktop
{
    [Module(ModuleName = "ApplicationDefaultsModule")]
    [ModuleDependency("ModuleEngine")]
    public class ShellModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private IUnityContainer _container;

        public ShellModule(IRegionViewRegistry regionViewRegistry, IUnityContainer container, IWorkspaceIoService savedFileLoadService)
        {
            _regionViewRegistry = regionViewRegistry;
            _container = container;
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion(ShellNavigationTargets.WorkspaceRegion.ToString(), typeof(EmptyWorkspacePage));
        }
    }
}
