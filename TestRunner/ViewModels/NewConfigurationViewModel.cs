using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.TestRunnerService;
using TestRunnerLibrary;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace TestRunner
{
    public class NewConfigurationViewModel : INotifyPropertyChanged
    {
        #region Properties and Fields

        private TestRunConfigData Data { get; set; }
        private TestRunnerServiceClient Client { get; set; }

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
            FileSavePicker filePicker = new FileSavePicker();
            filePicker.CommitButtonText = "Create Configuration File";
            filePicker.FileTypeChoices.Add("Test Runner Configuration File", new List<string> { ".xml" });

            StorageFile file = await filePicker.PickSaveFileAsync();
            if (file != null)
            {
                Data.SerializeAsync(file);
                await Client.StartTestingAsync(file.Path);
            }
        }

        public async void LoadTestRunConfiguration()
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".xml");
            filePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            StorageFile file = await filePicker.PickSingleFileAsync();
            if (file != null)
            {
                TestRunConfigData data = await TestRunConfigDataExtensions.DeserializeAsync(file);
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
