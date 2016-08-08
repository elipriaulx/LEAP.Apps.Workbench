using System;
using LEAP.Apps.Workbench.Core.Navigation;
using Prism.Regions;

namespace LEAP.Apps.Workbench.Core.Services
{
    public interface IExtensibilityService
    {
        void Navigate(ShellNavigationTargets target, string viewName, NavigationParameters navigationParameters = null);
        void RegisterView(ShellNavigationTargets target, Type view);
    }
}