using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TestRunner.Commands;
using TestRunner.UserControls;
using TestRunner.ViewModels;

namespace TestRunner
{
    public class ProcessesViewModel : INotifyPropertyChanged
    {
        #region Properties and Fields
        
        public ObservableCollection<TreeItemProjectViewModel> Projects { get; private set; }
        
        public ObservableCollection<CustomTabItem> Tabs { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ProcessesViewModel()
        {
            Projects = new ObservableCollection<TreeItemProjectViewModel>();
            Tabs = new ObservableCollection<CustomTabItem>();

            OpenProjectCommand.ProjectLoaded += ProjectLoaded;
        }

        private void ProjectLoaded(Project projectLoaded)
        {
            projectLoaded.ProjectChanged += ProjectChanged;
            Projects.Add(new TreeItemProjectViewModel(projectLoaded));
        }

        private void ProjectChanged(ProjectChangedEventArgs args)
        {
            for (int i = 0; i < Projects.Count; ++i)
            {
                if (Projects[i].Project == args.ChangedProject)
                {
                    Projects[i].RefreshOnProjectChanged(args);
                }
            }

            OnPropertyChanged("Projects");
        }

        public void UpdateUIWithTestResult(ulong processId, TreeItemTestResultViewModel testResult)
        {
            // Names cannot have spaces in
            string tabName = testResult.Name.Replace(" ", "") + "Tab";
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
                tabItem.Header = testResult.Name;
                tabItem.ToolTip = testResult.Name;
                tabItem.MouseRightButtonDown += CloseTab;

                Tabs.Add(tabItem);
            }

            tabItem.IsSelected = true;
            tabItem.UpdateUIWithTestResult(testResult);
        }

        private void CloseTab(object sender, MouseButtonEventArgs e)
        {
            Tabs.Remove(sender as CustomTabItem);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
