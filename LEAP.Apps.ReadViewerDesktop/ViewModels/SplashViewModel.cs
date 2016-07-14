using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Events;

namespace LEAP.Apps.ReadViewerDesktop.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        public SplashViewModel()
        {
            Title = "LEAP Read Viewer";
            Status = "Loading...";
        }

        public SplashViewModel(IEventAggregator eventAggregator, ILoggingService loggingService)
        {
            loggingService?.SetContext("Splash");

            loggingService?.Log(LogLevelTypes.Debug, "Preparing splash screen.");

            Title = "LEAP Read Viewer";
            Status = "Getting Ready...";

            //eventAggregator.GetEvent<InitialisationStatusUpdateEvent>().Subscribe(x =>
            //{
            //    Application.Current.Dispatcher.Invoke(delegate
            //    {
            //        Status = x;
            //    });

            //    loggingService?.Log(Debug, $"Initialisation Status Update: {x}");

            //}, ThreadOption.UIThread, true);
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
    }
}
