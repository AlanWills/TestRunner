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

        private ObservableCollection<ulong> processes;
        public ObservableCollection<ulong> Processes
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
            GetProcessOutput(null);
            GetProcesses();
        }

        private async void GetProcessOutput(object state)
        {
            SelectedProcessOutput = (await client.GetProcessOutputAsync(0));
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
