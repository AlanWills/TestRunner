using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TestRunner.Commands;
using TestRunner.UserControls;

namespace TestRunner
{
    public class ProcessesViewModel : INotifyPropertyChanged
    {
        #region Properties and Fields
        
        public ObservableCollection<Project> Projects { get; private set; }
        
        public ObservableCollection<CustomTabItem> Tabs { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ProcessesViewModel()
        {
            Projects = new ObservableCollection<Project>();
            Tabs = new ObservableCollection<CustomTabItem>();

            OpenProjectCommand.ProjectLoaded += ProjectLoaded;
        }

        private void ProjectLoaded(Project projectLoaded)
        {
            Projects.Add(projectLoaded);
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
            //tabItem.BuildFileContents.Text = TestRunnerProcessManager.GetProcessOutput(processId);
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
