using System.Windows;
using LEAP.Apps.Workbench.Core.Events;
using LEAP.Apps.Workbench.Core.Services;
using LEAP.Apps.Workbench.Services;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace LEAP.Apps.Workbench
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var splash = Container.Resolve<Splash>();
            splash.Show();

            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<InitialisationCompleteEvent>().Subscribe(x =>
            {
                splash.Hide();
                splash.Close();
            }, ThreadOption.UIThread, true);

            return Container.Resolve<ApplicationWindow>();
        }

        protected override void InitializeModules()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();
            var logger = Container.Resolve<ILoggingService>().CreateLogger();

            eventAggregator.GetEvent<InitialisationErrorEvent>().Subscribe(x =>
            {
                logger.Error(x);
            }, ThreadOption.UIThread, true);

            eventAggregator.GetEvent<InitialisationStartedEvent>().Subscribe(x =>
            {
                base.InitializeModules();

                eventAggregator.GetEvent<InitialisationStatusUpdateEvent>().Publish("Initialisation Complete");
                eventAggregator.GetEvent<InitialisationCompleteEvent>().Publish(null);
            }, ThreadOption.UIThread, true);

            eventAggregator.GetEvent<InitialisationCompleteEvent>().Subscribe(x =>
            {
                Application.Current.MainWindow = (ApplicationWindow) Shell;
                Application.Current.MainWindow.Show();
            }, ThreadOption.UIThread, true);

            eventAggregator.GetEvent<InitialisationStartedEvent>().Publish(null);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @"." };
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILoggingService, LoggingServiceProvider>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICommandParameterService, CommandParameterServiceProvider>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IWorkspaceManagementService, WorkspaceIoServiceProvider>(new ContainerControlledLifetimeManager());
            // Container.RegisterType<IActionComponentService, CommandParameterServiceProvider>(new ContainerControlledLifetimeManager());
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewModelFactory(t => Container.Resolve(t));
        }

        protected override void ConfigureModuleCatalog()
        {
            // TODO: Load Pluggins

            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ShellModule));

            base.ConfigureModuleCatalog();
        }
    }
}
