using System.IO;
using TestRunner.TestRunnerService;
using TestRunnerLibrary;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            TestRunnerServiceClient client = new TestRunnerServiceClient();

            TestRunConfigData configData = new TestRunConfigData()
            {
                FullPathToDll = @"C:\Users\Alan\Documents\Visual Studio 2015\Projects\OpenGL\OpenGL\Debug\TestKernel.dll",
                OutputFileFullPath = Directory.GetCurrentDirectory() + "\\Output.txt",
                ErrorFileFullPath = Directory.GetCurrentDirectory() + "\\Error.txt",
            };

            string path = ApplicationData.Current.LocalFolder.Path + "\\Test.xml";
            await configData.SerializeAsync(path);

            int x = await client.StartTestingAsync(path);
        }
    }
}
