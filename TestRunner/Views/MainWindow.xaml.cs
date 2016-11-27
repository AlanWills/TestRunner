using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TestRunner.Views;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ServiceHost Host { get; set; }

        private bool ForceClose { get; set; }

        public MainWindow()
        {
            WindowState = WindowState.Maximized;
            InitializeComponent();

            ProjectManager.Initialize();
            Frame.Navigate(new ProjectsView());
        }
        
        private void SystemTrayIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Show();
        }

        private void SystemTrayIcon_TrayMiddleMouseDown(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //~MainWindow()
        //{
        //    // Close the ServiceHost.
        //    Host.Close();
        //}
    }
}