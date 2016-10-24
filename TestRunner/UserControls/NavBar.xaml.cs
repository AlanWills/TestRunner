using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TestRunner.Views;

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
            //(Application.Current.MainWindow.Content as Frame).Navigate(typeof(MainWindow));
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
