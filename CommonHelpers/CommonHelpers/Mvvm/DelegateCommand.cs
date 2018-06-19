using System;
using System.Windows.Input;

namespace CommonHelpers.Mvvm
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action execute, Func<bool> canExecute)
            : base(_ => execute(), _ => canExecute())
        {

            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        public DelegateCommand(Action execute)
            : this(execute, () => true)
        {
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public DelegateCommand(Action<T> execute)
            : this(execute, _ => true)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (!(parameter is T) && parameter != (object)default(T))
                return false;

            return canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }

        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
