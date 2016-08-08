using System.Windows;
using LEAP.Apps.ReadViewerDesktop.Core.Components;
using LEAP.Apps.ReadViewerDesktop.Core.Events;
using LEAP.Apps.ReadViewerDesktop.Core.Services;
using LEAP.Apps.ReadViewerDesktop.Core.ViewModels;
using Prism.Events;

namespace LEAP.Apps.ReadViewerDesktop.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        private readonly ILogger _logger;

        public SplashViewModel()
        {
            Title = "LEAP Workbench";
            Status = "Initialising example module";
        }

        public SplashViewModel(IEventAggregator eventAggregator, ILoggingService loggingService)
        {
            _logger = loggingService.CreateLogger(nameof(SplashViewModel));

            _logger?.Debug("Preparing Splash.");

            Title = "LEAP Workbench";
            Status = string.Empty;

            eventAggregator.GetEvent<InitialisationStatusUpdateEvent>().Subscribe(UpdateStatus, ThreadOption.UIThread, true);
            
            eventAggregator.GetEvent<InitialisationErrorEvent>().Subscribe(x =>
            {
                UpdateStatus(x?.Message);
            }, ThreadOption.UIThread, true);

        }

        public string Title
        {
            get { return GetValue(() => Title); }
            set { SetValue(() => Title, value); }
        }

        public string Status
        {
            get { return GetValue(() => Status); }
            set { SetValue(() => Status, value); }
        }

        private void UpdateStatus(string message)
        {
            _logger?.Info($"Splash Status Update: {message}");

            Application.Current.Dispatcher.Invoke(delegate
            {
                Status = message;
            });
        }
    }
}
