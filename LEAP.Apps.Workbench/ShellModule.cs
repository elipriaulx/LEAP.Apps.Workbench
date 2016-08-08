using LEAP.Apps.Workbench.Core.Navigation;
using LEAP.Apps.Workbench.Core.Services;
using LEAP.Apps.Workbench.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LEAP.Apps.Workbench
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
