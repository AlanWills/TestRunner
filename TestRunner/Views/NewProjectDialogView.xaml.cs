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

        private NewProjectDialogViewModel NewConfig { get; set; }

        #endregion

        public NewProjectDialogView()
        {
            NewConfig = new NewProjectDialogViewModel();
            NewConfig.PropertyChanged += NewConfig_PropertyChanged;
            DataContext = NewConfig;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private void NewConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FullPathToDll.Name)
            {
                if (e.PropertyName == FullPathToDll.Name)
                {
                    FullPathToDll.FilePath = NewConfig.FullPathToDll;
                }

                StartButton.IsEnabled = NewConfig.IsConfigurationValid;
            }
        }

        private void CreateTestRunConfiguration(object sender, RoutedEventArgs e)
        {
            NewConfig.CreateTestRunConfiguration();
        }

        private void LoadTestRunConfiguration(object sender, RoutedEventArgs e)
        {
            NewConfig.LoadTestRunConfiguration();
        }
    }
}
