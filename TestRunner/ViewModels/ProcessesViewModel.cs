using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using TestRunner.Extensions;
using TestRunner.UserControls;
using TestRunnerLibrary;
using TestRunnerServiceLibrary;

namespace TestRunner
{
    public class ProcessesViewModel : INotifyPropertyChanged
    {
        #region Properties and Fields
        
        public ObservableCollection<string> Processes { get; private set; }

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

        public ObservableCollection<CustomTabItem> Tabs { get; private set; }

        public TestRunFrequency Frequency { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ProcessesViewModel()
        {
            Processes = new ObservableCollection<string>();
            Tabs = new ObservableCollection<CustomTabItem>();

            GetProcesses();
        }

        public void UpdateUIWithProcessData(ulong processId, string clickedProcessName)
        {
            if (Tabs.Any(x => x.Name == (clickedProcessName + "Tab")))
            {
                // Tab already exists so select it and then exit - no need to create a new one
                Tabs.First(x => x.Name == (clickedProcessName + "Tab")).IsSelected = true;
                return;
            }

            // How do we use the name to get the process ID - store a map in the Service - it will have to generate a name if one does not exist;  Or maybe we could enforce a name is provided

            string selectedProcessConfigDataPath = TestRunnerProcessManager.GetProcessConfigFilePath(processId);
            TestRunConfigData data = TestRunConfigData.Deserialize(selectedProcessConfigDataPath);
            Frequency = data.Frequency;

            CustomTabItem newItem = new CustomTabItem();
            newItem.Name = clickedProcessName + "Tab";
            newItem.Header = clickedProcessName;
            newItem.IsSelected = true;
            newItem.BuildFileContents.Text = TestRunnerProcessManager.GetProcessOutput(processId);

            Tabs.Add(newItem);
        }

        private void GetProcesses()
        {
            foreach (string procConfigFile in TestRunnerProcessManager.GetAllConfigFilePaths())
            {
                TestRunConfigData data = TestRunConfigData.Deserialize(procConfigFile);

                Processes.Add(data.ProcessName != null ? data.ProcessName : "Unidentified Test Process");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
