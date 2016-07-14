using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LEAP.Apps.ReadViewerDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILoggingService _logger;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Create a logger
            try
            {
                _logger = new NLogLoggingProvider();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
            try
            {
                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                _logger?.Log(LogLevelTypes.Critical, ex);

                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _logger?.Log(LogLevelTypes.Info, "Application instance terminating.");

            base.OnExit(e);
        }
    }
}
