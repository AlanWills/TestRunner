using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestRunner.TestRunnerService;
using TestRunnerLibrary;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace TestRunner
{
    public class NewConfigurationViewModel : INotifyPropertyChanged
    {
        #region Properties and Fields

        private TestRunConfigData Data { get; set; }
        private TestRunnerServiceClient Client { get; set; }

        public string ProcessName
        {
            get { return Data.ProcessName; }
            set
            {
                Data.ProcessName = value;
                OnPropertyChanged("ProcessName");
            }
        }

        public string FullPathToDll
        {
            get
            {
                return Data.FullPathToDll;
            }
            set
            {
                Data.FullPathToDll = value;
                OnPropertyChanged("FullPathToDll");
            }
        }

        public string OutputFileFullPath
        {
            get
            {
                return Data.OutputFileFullPath;
            }
            set
            {
                Data.OutputFileFullPath = value;
                OnPropertyChanged("OutputFileFullPath");
            }
        }

        public string ErrorFileFullPath
        {
            get
            {
                return Data.ErrorFileFullPath;
            }
            set
            {
                Data.ErrorFileFullPath = value;
                OnPropertyChanged("ErrorFileFullPath");
            }
        }

        public bool IsConfigurationValid
        {
            get
            {
                return !string.IsNullOrEmpty(FullPathToDll) && !string.IsNullOrEmpty(OutputFileFullPath) && !string.IsNullOrEmpty(ErrorFileFullPath);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public NewConfigurationViewModel()
        {
            Data = new TestRunConfigData();
            Client = new TestRunnerServiceClient();
        }

        public async void CreateTestRunConfiguration()
        {
            OpenFileDialog filePicker = new OpenFileDialog();
            filePicker.DefaultExt = ".xml";

            bool? result = filePicker.ShowDialog();

            if (result.HasValue && result.Value)
            {
                Data.Serialize(filePicker.FileName);
                await Client.StartTestingAsync(filePicker.FileName);

                MessageBox.Show("Test Process started");
            }
        }

        public void LoadTestRunConfiguration()
        {
            OpenFileDialog filePicker = new OpenFileDialog();
            filePicker.DefaultExt = ".xml";

            bool? result = filePicker.ShowDialog();

            if (result.HasValue && result.Value)
            {
                TestRunConfigData data = TestRunConfigData.Deserialize(filePicker.FileName);

                ProcessName = data.ProcessName;
                FullPathToDll = data.FullPathToDll;
                OutputFileFullPath = data.OutputFileFullPath;
                ErrorFileFullPath = data.ErrorFileFullPath;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
