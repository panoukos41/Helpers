using System;
using System.Windows.Input;

namespace Panoukos41.Helpers.Mvvm.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        ///
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Initialize a new instance of <see cref="Command"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public Command(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="Command"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public Command(Func<bool> canExecute, Action execute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter">Parameter is always ignored</param>
        /// <returns></returns>
        public bool CanExecute(object parameter = null)
            => !_isExecuting && (_canExecute?.Invoke() ?? true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter">Parameter is always ignored</param>
        public void Execute(object parameter = null)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    _execute?.Invoke();
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
    }
}