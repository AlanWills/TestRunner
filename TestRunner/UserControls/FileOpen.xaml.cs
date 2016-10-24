using System.Windows;
using System.Windows.Controls;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FileOpen : UserControl
    {
        #region Properties and Fields

        private static DependencyProperty filePathProperty = DependencyProperty.Register("FilePath", typeof(string), typeof(FileOpen), new PropertyMetadata(""));
        public string FilePath
        {
            get { return (string)GetValue(filePathProperty); }
            set
            {
                SetValue(filePathProperty, value);
                Path.Text = value;
            }
        }

        private static DependencyProperty fileExtensionProperty = DependencyProperty.Register("FileExtension", typeof(string), typeof(FileOpen), new PropertyMetadata(""));
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
        }

        private async void OpenFileBrowseDialog(object sender, RoutedEventArgs args)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(FileExtension);

            //StorageFile file = await filePicker.PickSingleFileAsync();
            //if (file != null)
            //{
            //    FilePath = file.Path;
            //}
        }

        private void Path_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetValue(filePathProperty, (sender as TextBox).Text);
        }
    }
}
