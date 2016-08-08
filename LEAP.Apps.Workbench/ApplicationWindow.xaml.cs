using System.Windows;
using LEAP.Apps.Workbench.ViewModels;

namespace LEAP.Apps.Workbench
{
    /// <summary>
    /// Interaction logic for ApplicationWindow.xaml
    /// </summary>
    public partial class ApplicationWindow : Window
    {
        public ApplicationWindow()
        {
            InitializeComponent();
        }

        public ApplicationWindow(ApplicationWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
