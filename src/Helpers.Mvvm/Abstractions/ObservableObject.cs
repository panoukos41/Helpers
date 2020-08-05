using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Panoukos41.Helpers.Mvvm
{
    /// <summary>
    /// Object that implements the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the <see cref="PropertyChanged"/> with the provided name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the new value if its different from the old and raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="field">Field that stores the value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>True if the value was set.</returns>
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}