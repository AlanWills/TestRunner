using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewProjectDialogView : Window
    {
        #region Properties and Fields

        public NewProjectDialogViewModel NewProject { get; private set; }

        #endregion

        public NewProjectDialogView()
        {
            NewProject = new NewProjectDialogViewModel();
            NewProject.PropertyChanged += NewConfig_PropertyChanged;
            DataContext = NewProject;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private void NewConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FullPathToDll.Name)
            {
                FullPathToDll.FilePath = NewProject.FullPathToDll;
            }
            else if (e.PropertyName == ProjectSaveLocation.Name)
            {
                ProjectSaveLocation.DirectoryPath = NewProject.ProjectSaveLocation;
            }

            CreateButton.IsEnabled = NewProject.IsConfigurationValid;
        }

        private void CreateProject(object sender, RoutedEventArgs e)
        {
            NewProject.CreateProject();

            DialogResult = true;
            Close();
        }

        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
