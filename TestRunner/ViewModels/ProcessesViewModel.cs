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

        private ObservableCollection<string> processes;
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

        public string DefaultFrequency
        {
            get
            {
                return TestRunFrequency.kDaily.ToDisplayString();
            }
        }

        private TestRunnerServiceClient client;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ProcessesViewModel()
        {
            client = new TestRunnerServiceClient();
            GetProcesses();
        }

        public async void GetProcessOutput(ulong processId)
        {
            SelectedProcessOutput = (await client.GetProcessOutputAsync(processId));
        }

        private async void GetProcesses()
        {
            string[] processes = await client.GetAllProcessesAsync();
            foreach (string proc in processes)
            {
                Processes.Add(proc);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
