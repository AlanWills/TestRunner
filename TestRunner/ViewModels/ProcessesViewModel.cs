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
        
        public ObservableCollection<CustomTabItem> Tabs { get; private set; }

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
            string tabName = clickedProcessName.Replace(' ', '_') + "Tab";
            CustomTabItem tabItem = null;

            if (Tabs.Any(x => x.Name == tabName))
            {
                // Tab already exists so select it and then exit - no need to create a new one
                tabItem = Tabs.First(x => x.Name == tabName);
            }
            else
            {
                tabItem = new CustomTabItem();
                tabItem.Name = tabName;
                tabItem.Header = clickedProcessName;

                Tabs.Add(tabItem);
            }

            tabItem.IsSelected = true;
            tabItem.BuildFileContents.Text = TestRunnerProcessManager.GetProcessOutput(processId);
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
