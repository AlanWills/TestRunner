using System;
using System.Windows;
using System.Windows.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TestRunner.UserControls
{
    public sealed partial class MainMenuView : UserControl
    {
        #region Properties and Fields

        private MainMenuViewModel MainMenuViewModel { get; set; }

        #endregion

        public MainMenuView()
        {
            MainMenuViewModel = new MainMenuViewModel();
            DataContext = MainMenuViewModel;

            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            MainMenuViewModel.New();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            MainMenuViewModel.Open();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MainMenuViewModel.Save();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            MainMenuViewModel.Minimize();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainMenuViewModel.Exit();
        }
    }
}
