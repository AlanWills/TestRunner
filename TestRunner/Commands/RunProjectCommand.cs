using System;
using System.Windows.Input;

namespace TestRunner.Commands
{
    public class RunProjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            (parameter as Project).Run();
        }
    }
}
