using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows;
using TestRunner.TestRunnerService;
using TestRunner.Views;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ServiceHost Host { get; set; }

        public MainWindow()
        {
            //Host = new ServiceHost(typeof(TestRunnerServiceLibrary.TestRunnerService), new Uri("http://localhost:8733/Design_Time_Addresses/TestRunnerService/"));

            //// Enable metadata publishing.
            //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //smb.HttpGetEnabled = true;
            //smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            //Host.Description.Behaviors.Add(smb);

            //Host.AddServiceEndpoint(typeof(TestRunnerServiceLibrary.ITestRunnerService), new WSHttpBinding(), "");

            //// Open the ServiceHost to start listening for messages. Since
            //// no endpoints are explicitly configured, the runtime will create
            //// one endpoint per base address for each service contract implemented
            //// by the service.
            //Host.Open();
            
            //TestRunnerServiceClient client = new TestRunnerServiceClient();
            //client.Open();
            //client.GetTestingStatus(0);

            WindowState = WindowState.Maximized;
            InitializeComponent();
            Frame.Navigate(new HomeView());
        }
        
        //~MainWindow()
        //{
        //    // Close the ServiceHost.
        //    Host.Close();
        //}
    }
}