using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppSettingsView : Page
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
