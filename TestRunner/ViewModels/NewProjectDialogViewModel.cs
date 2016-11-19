using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using TestRunner.Converters;
using TestRunner.Extensions;

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

        public Project Project { get; private set; }

        public string ProjectName
        {
            get { return Project.Name; }
            set
            {
                if (Project.Name != value)
                {
                    Project.Name = value;
                    OnPropertyChanged("ProjectName");
                }
            }
        }

        public string FullPathToDll
        {
            get
            {
                return Project.FullPathToDll;
            }
            set
            {
                if (Project.FullPathToDll != value)
                {
                    Project.FullPathToDll = value;
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
                return Project.Frequency.ToTestRunFrequency();
            }
            set
            {
                if (Project.Frequency != value.ToTimeSpan())
                {
                    Project.Frequency = value.ToTimeSpan();
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
                return Project.Platform;
            }
            set
            {
                if (Project.Platform != value)
                {
                    Project.Platform = value;
                    OnPropertyChanged("Platform");
                }
            }
        }

        public bool IsConfigurationValid
        {
            get
            {
                return !string.IsNullOrEmpty(FullPathToDll) && !string.IsNullOrEmpty(ProjectSaveLocation) &&
                       File.Exists(FullPathToDll) && Directory.Exists(ProjectSaveLocation);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public NewProjectDialogViewModel()
        {
            Project = new Project();
        }

        public void CreateProject()
        {
            Debug.Assert(IsConfigurationValid, "Configuration is not valid.  Check that the UI is set up correctly so we cannot create without a valid configuration");

            // Don't want to serialize out a name with spaces in it
            Project.FilePath = Path.Combine(ProjectSaveLocation, ProjectName.Replace(" ", "") + Project.FileExtension);
            Project.Save();
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
