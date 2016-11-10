using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using TestRunner.Converters;
using TestRunner.Extensions;
using TestRunnerLibrary;

namespace TestRunner
{
    public class NewProjectDialogViewModel : INotifyPropertyChanged
    {
        #region Properties and Fields

        private string projectSaveLocation;
        public string ProjectSaveLocation
        {
            get { return projectSaveLocation; }
            set
            {
                if (projectSaveLocation != value)
                {
                    projectSaveLocation = value;
                    OnPropertyChanged("ProjectSaveLocation");
                }
            }
        }

        private TestRunConfigData Data { get; set; }

        public string ProjectName
        {
            get { return Data.ProjectName; }
            set
            {
                if (Data.ProjectName != value)
                {
                    Data.ProjectName = value;
                    OnPropertyChanged("ProjectName");
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
                if (Data.FullPathToDll != value)
                {
                    Data.FullPathToDll = value;
                    OnPropertyChanged("FullPathToDll");
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
                if (Data.Frequency != value)
                {
                    Data.Frequency = value;
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
                if (Data.Platform != value)
                {
                    Data.Platform = value;
                    OnPropertyChanged("Platform");
                }
            }
        }

        public bool IsConfigurationValid
        {
            get
            {
                return !string.IsNullOrEmpty(FullPathToDll) && !string.IsNullOrEmpty(ProjectSaveLocation);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public NewProjectDialogViewModel()
        {
            Data = new TestRunConfigData();
        }

        public void CreateProject()
        {
            // Don't want to serialize out a name with spaces in it
            Data.Serialize(Path.Combine(ProjectSaveLocation, ProjectName.Replace(" ","") + TestRunConfigData.FileExtension));
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
