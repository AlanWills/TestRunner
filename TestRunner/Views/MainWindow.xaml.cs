using System;
using System.ServiceProcess;
using System.Windows;
using TestRunner.Views;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                ServiceController service = new ServiceController("TestRunnerService");
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(3000));
            }
            catch { }

            WindowState = WindowState.Maximized;
            InitializeComponent();
            Frame.Navigate(new HomeView());
        }
    }
}
