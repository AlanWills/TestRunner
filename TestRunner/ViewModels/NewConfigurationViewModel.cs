using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using TestRunner.Converters;
using TestRunner.Extensions;
using TestRunner.TestRunnerService;
using TestRunnerLibrary;
using TestRunnerServiceLibrary;

namespace TestRunner
{
    public class NewConfigurationViewModel : INotifyPropertyChanged
    {
        #region Properties and Fields

        private TestRunConfigData Data { get; set; }

        public string ProcessName
        {
            get { return Data.ProcessName; }
            set
            {
                bool changed = Data.ProcessName != value;
                Data.ProcessName = value;

                if (changed)
                {
                    OnPropertyChanged("ProcessName");
                }
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
                bool changed = Data.FullPathToDll != value;
                Data.FullPathToDll = value;

                if (changed)
                {
                    OnPropertyChanged("FullPathToDll");
                }
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
                bool changed = Data.OutputFileFullPath != value;
                Data.OutputFileFullPath = value;

                if (changed)
                {
                    OnPropertyChanged("OutputFileFullPath");
                }
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
                bool changed = Data.ErrorFileFullPath != value;
                Data.ErrorFileFullPath = value;

                if (changed)
                {
                    OnPropertyChanged("ErrorFileFullPath");
                }
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

        public TestRunFrequency Frequency
        {
            get
            {
                return Data.Frequency;
            }
            set
            {
                bool changed = Data.Frequency != value;
                Data.Frequency = value;

                if (changed)
                {
                    OnPropertyChanged("Frequency");
                }
            }
        }

        public List<string> Platforms
        {
            get
            {
                DefaultEnumConverter converter = new DefaultEnumConverter();
                List<string> platformList = new List<string>();

                foreach (Platform platform in Enum.GetValues(typeof(Platform)))
                {
                    platformList.Add(converter.Convert(platform, typeof(string), null, null) as string);
                }


                return platformList;
            }
        }

        public Platform Platform
        {
            get
            {
                return Data.Platform;
            }
            set
            {
                bool changed = Data.Platform != value;
                Data.Platform = value;

                if (changed)
                {
                    OnPropertyChanged("Platform");
                }
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
            Frequency = TestRunFrequency.Daily;
        }

        public void CreateTestRunConfiguration()
        {
            SaveFileDialog filePicker = new SaveFileDialog();
            filePicker.CreatePrompt = true;
            filePicker.OverwritePrompt = true;
            filePicker.DefaultExt = ".xml";

            bool? result = filePicker.ShowDialog();

            if (result.HasValue && result.Value)
            {
                Data.Serialize(filePicker.FileName);
                TestRunnerProcessManager.CreateProcess(filePicker.FileName);

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
                Platform = data.Platform;
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
