using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_New.Command
{
    class RelayCommand : ICommand
    {
        Action<object> executeMethod;
        Func<object, bool> canExecute;

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecute)
        {
            this.canExecute = canExecute;
            this.executeMethod = executeMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }
    }
}
