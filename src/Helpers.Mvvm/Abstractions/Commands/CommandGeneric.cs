using System;
using System.Windows.Input;

namespace Panoukos41.Helpers.Mvvm.Commands
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Command<T> : ICommandGeneric<T>
    {
        /// <summary>
        ///
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        /// <summary>
        /// Initialize a new instance of <see cref="Command{T}"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public Command(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="Command{T}"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public Command(Func<T, bool> canExecute, Action<T> execute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(T parameter)
            => !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    _execute?.Invoke(parameter);
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

        void ICommand.Execute(object parameter)
            => Execute((T)parameter);
    }
}