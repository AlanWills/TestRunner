using System;
using System.Windows;
using System.Windows.Controls;

namespace TestRunner.UserControls
{
    /// <summary>
    /// Interaction logic for NavBarButton.xaml
    /// </summary>
    public partial class NavBarButton : UserControl
    {
        #region Properties and Fields

        private static DependencyProperty buttonContent = DependencyProperty.Register("ButtonText", typeof(string), typeof(NavBarButton), new PropertyMetadata(""));
        public string ButtonContent
        {
            get { return (string)GetValue(buttonContent); }
            set
            {
                SetValue(buttonContent, value);
                Button.Content = value;
            }
        }

        private static DependencyProperty targetView = DependencyProperty.Register("TargetView", typeof(string), typeof(NavBarButton), new PropertyMetadata(""));
        public string TargetView
        {
            get { return (string)GetValue(targetView); }
            set
            {
                SetValue(targetView, value);
            }
        }

        #endregion

        public NavBarButton()
        {
            InitializeComponent();
        }

        private void Navigate(object sender, RoutedEventArgs e)
        {
            ((Application.Current.MainWindow.Content as Grid).Children[1] as Frame).Navigate(new Uri("Views\\" + TargetView + ".xaml", UriKind.Relative));
        }
    }
}
