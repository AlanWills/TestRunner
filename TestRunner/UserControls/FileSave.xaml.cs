using Microsoft.Win32;
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

        private static DependencyProperty filePathProperty = DependencyProperty.Register("FilePath", typeof(string), typeof(FileSave), new PropertyMetadata(""));
        public string FilePath
        {
            get { return (string)GetValue(filePathProperty); }
            set
            {
                SetValue(filePathProperty, value);
                Path.Text = value;
            }
        }

        private readonly DependencyProperty fileExtensionProperty = DependencyProperty.Register("FileExtension", typeof(string), typeof(FileSave), new PropertyMetadata(""));
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
        }

        private void OpenFileBrowseDialog(object sender, RoutedEventArgs args)
        {
            SaveFileDialog fileSaver = new SaveFileDialog();
            fileSaver.CreatePrompt = true;
            fileSaver.AddExtension = true;
            fileSaver.OverwritePrompt = true;
            fileSaver.DefaultExt = FileExtension;

            bool? result = fileSaver.ShowDialog();

            if (result.HasValue && result.Value)
            {
                FilePath = fileSaver.FileName;
            }
        }
    }
}
