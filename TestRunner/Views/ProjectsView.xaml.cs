using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using TestRunner.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProjectsView : Page
    {
        #region Properties and Fields

        private ProjectsViewModel ProcessesViewModel { get; set; }

        #endregion

        public ProjectsView()
        {
            ProcessesViewModel = new ProjectsViewModel();
            DataContext = ProcessesViewModel;
            InitializeComponent();

            ProcessesViewModel.PropertyChanged += ProcessesViewModel_PropertyChanged;
            Projects.AddHandler(Control.MouseDoubleClickEvent, new RoutedEventHandler(HandleDoubleClick));
        }

        private void TestResultSelected(TreeItemTestResultViewModel testResultViewModel)
        {
            // Need to get process ID somehow
            ProcessesViewModel.UpdateUIWithTestResult(0, testResultViewModel.TestResult);
        }

        private void HandleDoubleClick(object sender, RoutedEventArgs e)
        {
            DependencyObject depObj = e.OriginalSource as DependencyObject;
            if (depObj != null)
            {
                // go up the visual hierarchy until we find the tree view item the click came from  
                // the click might have been on the grid or column headers so we need to cater for this  
                DependencyObject current = depObj;
                while (current != null && current != Projects)
                {
                    TreeViewItem lvi = current as TreeViewItem;
                    if (lvi != null && (lvi.DataContext is TreeItemTestResultViewModel))
                    {
                        // this is the tree view item  
                        // do something with it here
                        TestResultSelected(lvi.DataContext as TreeItemTestResultViewModel);
      
                        // break out of loop  
                        return;
                    }
                    current = VisualTreeHelper.GetParent(current);
                }

                Debug.Assert(current != null, "Couldn't find Test result in hierarchy");
            }
        }

        private void ProcessesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Projects.Name)
            {
                Projects.Items.Refresh();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Projects.Items.Refresh();
        }
    }
}