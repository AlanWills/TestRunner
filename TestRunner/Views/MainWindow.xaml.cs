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
            WindowState = WindowState.Maximized;
            InitializeComponent();
            Frame.Navigate(new HomeView());
        }
    }
}
