using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEAP.Apps.ReadViewerDesktop.Core.Navigation;
using LEAP.Apps.ReadViewerDesktop.Core.Services;
using LEAP.Apps.Workbench.Plugins.ImageViewerExample.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LEAP.Apps.Workbench.Plugins.ImageViewerExample
{
    [Module(ModuleName = "ImageViewerExampleModule")]
    //[ModuleDependency("ModuleDefault")]
    //[ModuleDependency("ModuleEngine")]
    public class ImageViewerExampleModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IRegionManager _regionManager;
        private IUnityContainer _container;

        public ImageViewerExampleModule(IRegionViewRegistry regionViewRegistry, IRegionManager regionManager, IUnityContainer container, IWorkspaceIoService savedFileLoadService)
        {
            _regionViewRegistry = regionViewRegistry;
            _regionManager = regionManager;
            _container = container;

            var loader = savedFileLoadService.RegisterLoader("jpg", Guid.NewGuid(), "Image Loader", x =>
            {
                var img = new Bitmap(x);

                return img;
            });

            loader.RegisterActioner(new Guid(), "Image Actioner", x =>
            {
                var p = new NavigationParameters { { "imageData", x } };
                _regionManager.RequestNavigate("WorkspaceRegion", nameof(ViewImagePage), p);
            });
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion("WorkspaceRegion", typeof(ViewImagePage));
        }
    }
}
