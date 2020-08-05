using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Panoukos41.Helpers.Mvvm.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CommandAsync<T> : ICommandAsyncGeneric<T>
    {
        /// <summary>
        ///
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;

        /// <summary>
        /// Initialize a new instance of <see cref="CommandAsync{T}"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public CommandAsync(Func<T, Task> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="Command{T}"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public CommandAsync(Func<T, bool> canExecute, Func<T, Task> execute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Indicates if the task can execute.
        /// </summary>
        /// <returns></returns>
        public bool CanExecute(T parameter)
            => !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);

        /// <summary>
        /// Executes the task if <see cref="CanExecute(T)"/> returns true.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        bool ICommand.CanExecute(object parameter)
            => CanExecute((T)parameter);

        async void ICommand.Execute(object parameter)
            => await ExecuteAsync((T)parameter);
    }
}