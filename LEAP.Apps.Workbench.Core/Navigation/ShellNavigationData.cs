using Prism.Regions;

namespace LEAP.Apps.Workbench.Core.Navigation
{
    public class ShellNavigationData
    {
        public ShellNavigationTargets Target { get; set; }
        public string Destination { get; set; }
        public NavigationParameters Parameters { get; set; }
    }
}
