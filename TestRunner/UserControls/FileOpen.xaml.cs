using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FileOpen : Page
    {
        #region Properties and Fields

        private readonly DependencyProperty filePathProperty;
        public string FilePath
        {
            get { return (string)GetValue(filePathProperty); }
            set
            {
                SetValue(filePathProperty, value);
                Path.Text = value;
            }
        }

        private readonly DependencyProperty fileExtensionProperty;
        public string FileExtension
        {
            get { return (string)GetValue(fileExtensionProperty); }
            set
            {
                SetValue(fileExtensionProperty, value);
            }
        }

        #endregion

        public FileOpen()
        {
            InitializeComponent();
            filePathProperty = DependencyProperty.Register("FilePath", typeof(string), typeof(FileOpen), new PropertyMetadata(""));
            fileExtensionProperty = DependencyProperty.Register("FileExtension", typeof(string), typeof(FileOpen), new PropertyMetadata(""));
        }

        private async void OpenFileBrowseDialog(object sender, RoutedEventArgs args)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(FileExtension);

            StorageFile file = await filePicker.PickSingleFileAsync();
            if (file != null)
            {
                FilePath = file.Path;
            }
        }

        private void Path_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetValue(filePathProperty, (sender as TextBox).Text);
        }
    }
}
