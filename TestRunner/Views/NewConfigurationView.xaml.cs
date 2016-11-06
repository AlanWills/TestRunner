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
    public sealed partial class NewConfigurationView : Page
    {
        #region Properties and Fields

        private NewConfigurationViewModel NewConfig { get; set; }

        #endregion

        public NewConfigurationView()
        {
            InitializeComponent();
            NewConfig = new NewConfigurationViewModel();
            NewConfig.PropertyChanged += NewConfig_PropertyChanged;
            DataContext = NewConfig;
        }

        private void NewConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FullPathToDll.Name ||
                e.PropertyName == OutputFileFullPath.Name ||
                e.PropertyName == ErrorFileFullPath.Name)
            {
                if (e.PropertyName == FullPathToDll.Name)
                {
                    FullPathToDll.FilePath = NewConfig.FullPathToDll;
                }
                else if (e.PropertyName == OutputFileFullPath.Name)
                {
                    OutputFileFullPath.FilePath = NewConfig.OutputFileFullPath;
                }
                else if (e.PropertyName == ErrorFileFullPath.Name)
                {
                    ErrorFileFullPath.FilePath = NewConfig.ErrorFileFullPath;
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
