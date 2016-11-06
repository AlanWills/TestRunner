using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TestRunner.UserControls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProcessesView : Page
    {
        #region Properties and Fields

        private ProcessesViewModel ProcessesViewModel { get; set; }

        #endregion

        public ProcessesView()
        {
            ProcessesViewModel = new ProcessesViewModel();
            DataContext = ProcessesViewModel;
            InitializeComponent();
        }
        
        private void Process_Selected(object sender, RoutedEventArgs e)
        {
            string clickedProcessName = (e.Source as ListView).SelectedItem as string;

            CustomTabItem newItem = new CustomTabItem();
            newItem.Header = clickedProcessName;

            // Bind items from ViewModel and so move all of this there instead
            // Then we can also encapsulate the visibility shit with a binding?

            BuildFileTabControl.Visibility = Visibility.Visible;
            BuildFileTabControl.Items.Add(newItem);
            BuildFileTabControl.SelectedIndex = BuildFileTabControl.Items.Count - 1;

            ProcessesViewModel.UpdateUIWithProcessData(0);
        }
    }
}