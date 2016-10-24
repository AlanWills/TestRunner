using System.Windows;
using System.Windows.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FileSave : UserControl
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

        public FileSave()
        {
            InitializeComponent();
            filePathProperty = DependencyProperty.Register("FilePath", typeof(string), typeof(FileOpen), new PropertyMetadata(""));
            fileExtensionProperty = DependencyProperty.Register("FileExtension", typeof(string), typeof(FileOpen), new PropertyMetadata(""));
        }

        private async void OpenFileBrowseDialog(object sender, RoutedEventArgs args)
        {
            //FileSavePicker filePicker = new FileSavePicker();
            //filePicker.FileTypeChoices.Add("Test Runner Configuration File", new List<string> { FileExtension });
            //filePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

            //StorageFile file = await filePicker.PickSaveFileAsync();
            //if (file != null)
            //{
            //    FilePath = file.Path;
            //}
        }
    }
}
