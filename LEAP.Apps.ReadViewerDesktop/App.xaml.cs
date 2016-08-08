using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LEAP.Apps.ReadViewerDesktop.Core.Components;
using LEAP.Apps.ReadViewerDesktop.Core.Services;
using LEAP.Apps.ReadViewerDesktop.Services;
using LEAP.Apps.ReadViewerDesktop.ViewModels;

namespace LEAP.Apps.ReadViewerDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILogger _logger;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Create a logger
            try
            {
                var loggerFactory = new LoggingServiceProvider();
                _logger = loggerFactory.CreateLogger();
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
                _logger?.Critical(ex);

                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _logger?.Info("Application instance terminating.");

            base.OnExit(e);
        }
    }
}
