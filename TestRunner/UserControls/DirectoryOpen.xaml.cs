using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DirectoryOpen : UserControl
    {
        #region Properties and Fields

        private static DependencyProperty directoryPathProperty = DependencyProperty.Register("DirectoryPath", typeof(string), typeof(DirectoryOpen), new PropertyMetadata(""));
        public string DirectoryPath
        {
            get { return (string)GetValue(directoryPathProperty); }
            set
            {
                // Set the value of the property to be the full path
                SetValue(directoryPathProperty, value);
                Path.Text = value;
            }
        }
        
        #endregion

        public DirectoryOpen()
        {
            InitializeComponent();
        }

        private void OpenDirectoryBrowseDialog(object sender, RoutedEventArgs args)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryPath = dialog.SelectedPath;
            }
        }

        private void Path_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetValue(directoryPathProperty, (sender as TextBox).Text);
        }
    }
}
