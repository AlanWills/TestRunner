using System;
using System.Windows.Input;
using TestRunner.Views;

namespace TestRunner.Commands
{
    public class NewProjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NewProjectDialogView newDialog = new NewProjectDialogView();
            bool? result = newDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                ProjectManager.OpenProjectCommand.Execute(newDialog.NewProject.Project.FilePath);
            }
        }
    }
}
