using System.Windows;
using System.Windows.Input;

namespace LEAP.Apps.Workbench.Core.Styles
{
    /// <summary>
    /// Interaction logic for BaseStyles.xaml
    /// </summary>
    partial class ControlStyles : ResourceDictionary
    {
        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
