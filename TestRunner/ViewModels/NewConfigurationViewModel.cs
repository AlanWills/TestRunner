﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using TestRunner.Extensions;
using TestRunner.TestRunnerService;
using TestRunnerLibrary;

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

        public List<string> Frequencies
        {
            get
            {
                List<string> freqList = new List<string>();

                foreach (TestRunFrequency f in Enum.GetValues(typeof(TestRunFrequency)))
                {
                    freqList.Add(f.ToDisplayString());
                }

                return freqList;
            }
        }

        public TestRunFrequency Frequency { get; set; }

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
            Frequency = TestRunFrequency.kDaily;
        }

        public async void CreateTestRunConfiguration()
        {
            SaveFileDialog filePicker = new SaveFileDialog();
            filePicker.CreatePrompt = true;
            filePicker.OverwritePrompt = true;
            filePicker.DefaultExt = ".xml";

            bool? result = filePicker.ShowDialog();

            if (result.HasValue && result.Value)
            {
                Data.Serialize(filePicker.FileName);
                await Client.StartTestingAsync(filePicker.FileName);

                MessageBox.Show("Test Process started", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                Frequency = data.Frequency;
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
