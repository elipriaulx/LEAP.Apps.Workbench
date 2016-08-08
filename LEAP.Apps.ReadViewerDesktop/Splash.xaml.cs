using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LEAP.Apps.ReadViewerDesktop.Core.Components;
using LEAP.Apps.ReadViewerDesktop.Core.Events;
using LEAP.Apps.ReadViewerDesktop.Core.Services;
using LEAP.Apps.ReadViewerDesktop.ViewModels;
using Prism.Events;

namespace LEAP.Apps.ReadViewerDesktop
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        private readonly ILogger _logger;

        public Splash()
        {
            InitializeComponent();
        }

        public Splash(ILoggingService loggingService, SplashViewModel splashViewModel)
        {
            _logger = loggingService.CreateLogger();
            
            InitializeComponent();

            DataContext = splashViewModel;
        }
    }
}
