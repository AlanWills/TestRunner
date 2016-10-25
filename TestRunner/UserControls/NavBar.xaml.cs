using System;
using System.Windows;
using System.Windows.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TestRunner.UserControls
{
    public sealed partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

        private void NavigateToMainPage(object sender, RoutedEventArgs args)
        {
            (Application.Current.MainWindow.Content as Frame).Navigate(new Uri("Views\\HomeView.xaml", UriKind.Relative));
        }

        private void NavigateToNewConfigurationView(object sender, RoutedEventArgs args)
        {
            (Application.Current.MainWindow.Content as Frame).Navigate(new Uri("Views\\NewConfigurationView.xaml", UriKind.Relative));
        }

        private void NavigateToProcessesView(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.Content as Frame).Navigate(new Uri("Views\\ProcessesView.xaml", UriKind.Relative));
        }

        private void NavigateToAppSettingsView(object sender, RoutedEventArgs args)
        {
            (Application.Current.MainWindow.Content as Frame).Navigate(new Uri("Views\\AppSettingsView.xaml", UriKind.Relative));
        }
    }
}
