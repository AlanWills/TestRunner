using System.Windows;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppSettingsView : Window
    {
        private AppSettingsViewModel AppSettingsViewModel { get; set; }

        public AppSettingsView()
        {
            InitializeComponent();
            AppSettingsViewModel = new AppSettingsViewModel();
            DataContext = AppSettingsViewModel;
            VSTestFileOpen.FilePath = AppSettingsViewModel.VSTestPath;
        }
    }
}
