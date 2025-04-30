using System.Windows.Input;

namespace Desktop.Commands
{
    public class RelayCommand: ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canex;

        public RelayCommand (Action<object> execute, Func<object, bool> canex = null)
        {
            _execute = execute;
            _canex = canex;
        }
        public bool CanExecute(object parameter) => _canex == null || _canex(parameter);
        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
