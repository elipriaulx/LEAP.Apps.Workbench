using System;
using LEAP.Apps.Workbench.Core.Components;
using LEAP.Apps.Workbench.Core.Navigation;
using LEAP.Apps.Workbench.Core.Services;
using Prism.Regions;

namespace LEAP.Apps.Workbench.Services
{
    public class ExtensibilityServiceProvider : IExtensibilityService
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IRegionManager _regionManager;
        private readonly ILogger _logger;

        protected ExtensibilityServiceProvider(IRegionViewRegistry regionViewRegistry, IRegionManager regionManager, ILoggingService loggingService)
        {
            _regionViewRegistry = regionViewRegistry;
            _regionManager = regionManager;

            _logger = loggingService.CreateLogger();
        }

        public void Navigate(ShellNavigationTargets target, string viewName, NavigationParameters navigationParameters = null)
        {
            var targetRegion = target.ToString();

            _logger.Debug($"Navigating region [{targetRegion}] to view with key [{targetRegion}].");

            _regionManager.RequestNavigate(targetRegion, viewName, navigationParameters);
        }

        public void RegisterView(ShellNavigationTargets target, Type view)
        {
            var targetRegion = target.ToString();

            _logger.Debug($"Registering view [{nameof(view)}] with region [{targetRegion}].");
            _regionViewRegistry.RegisterViewWithRegion(targetRegion, view);
        }
    }
}