using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TestRunner.TestRunnerService;
using TestRunnerLibrary;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class NewConfigurationView : Page
    {
        public NewConfigurationView()
        {
            InitializeComponent();
        }

        private async void CreateTestRunProcess(object sender, RoutedEventArgs e)
        {
            TestRunnerServiceClient client = new TestRunnerServiceClient();
            TestRunConfigData configData = new TestRunConfigData()
            {
                FullPathToDll = DllPathPicker.FilePath,
                OutputFileFullPath = OutputFilePicker.FilePath,
                ErrorFileFullPath = ErrorFilePicker.FilePath,
            };

            string path = ApplicationData.Current.LocalFolder.Path + "\\Test.xml";
            await configData.SerializeAsync(path);

            ulong x = await client.StartTestingAsync(path);
            TestingStatus result = await client.GetTestingStatusAsync(x);
        }
    }
}
