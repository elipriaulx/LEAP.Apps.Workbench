using System.Windows;
using LEAP.Apps.Workbench.Core.Components;
using LEAP.Apps.Workbench.Core.Services;
using LEAP.Apps.Workbench.ViewModels;

namespace LEAP.Apps.Workbench
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
