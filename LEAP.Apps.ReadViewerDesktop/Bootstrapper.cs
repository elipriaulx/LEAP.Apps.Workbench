using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace LEAP.Apps.ReadViewerDesktop
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ApplicationWindow>();
        }

        protected override void InitializeModules()
        {
            var splash = new Splash();

            splash.Show();
            
            base.InitializeModules();

            splash.Close();

            Application.Current.MainWindow = (ApplicationWindow)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @"." };
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<ILoggingService, NLogLoggingProvider>();
            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewModelFactory(t => Container.Resolve(t));
        }
    }
}
