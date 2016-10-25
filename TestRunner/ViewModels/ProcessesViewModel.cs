using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestRunner.Extensions;
using TestRunner.TestRunnerService;
using TestRunnerLibrary;

namespace TestRunner
{
    public class ProcessesViewModel : INotifyPropertyChanged
    {
        #region Properties and Fields

        private string selectedProcessOutput;
        public string SelectedProcessOutput
        {
            get { return selectedProcessOutput; }
            private set
            {
                selectedProcessOutput = value;
                OnPropertyChanged("SelectedProcessOutput");
            }
        }

        private ObservableCollection<string> processes = new ObservableCollection<string>();
        public ObservableCollection<string> Processes
        {
            get { return processes; }
            private set
            {
                processes = value;
                OnPropertyChanged("Processes");
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
        
        private TestRunnerServiceClient client;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ProcessesViewModel()
        {
            client = new TestRunnerServiceClient();
            GetProcesses();
        }

        public void UpdateUIWithProcessData(ulong processId)
        {
            // How do we use the name to get the process ID - store a map in the Service - it will have to generate a name if one does not exist;  Or maybe we could enforce a name is provided
            SelectedProcessOutput = client.GetProcessOutput(processId);

            string selectedProcessConfigDataPath = client.GetProcessConfigFilePath(processId);
            TestRunConfigData data = TestRunConfigData.Deserialize(selectedProcessConfigDataPath);
            Frequency = data.Frequency;
        }

        private void GetProcesses()
        {
            string[] processesFiles = client.GetAllConfigFilePaths();
            foreach (string procConfigFile in processesFiles)
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
