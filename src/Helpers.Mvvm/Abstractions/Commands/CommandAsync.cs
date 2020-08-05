using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Panoukos41.Helpers.Mvvm.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CommandAsync : ICommandAsync
    {
        /// <summary>
        ///
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Initialize a new instance of <see cref="CommandAsync"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public CommandAsync(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="CommandAsync"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public CommandAsync(Func<bool> canExecute, Func<Task> execute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Indicates if the task can execute.
        /// </summary>
        /// <returns></returns>
        public bool CanExecute()
            => !_isExecuting && (_canExecute?.Invoke() ?? true);

        /// <summary>
        /// Executes the task if <see cref="CanExecute"/> returns true.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
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
            => CanExecute();

        async void ICommand.Execute(object parameter)
            => await ExecuteAsync();
    }
}