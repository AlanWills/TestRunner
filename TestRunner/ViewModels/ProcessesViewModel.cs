using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestRunner.TestRunnerService;

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

        private Timer outputTimer;
        private TestRunnerServiceClient client;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ProcessesViewModel()
        {
            //outputTimer = new Timer(GetProcessOutput, null, 0, 5000);
            client = new TestRunnerServiceClient();
            GetProcesses();
        }

        public async void GetProcessOutput(ulong processId)
        {
            SelectedProcessOutput = (await client.GetProcessOutputAsync(processId));
        }

        private async void GetProcesses()
        {
            Processes = await client.GetAllProcessesAsync();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
