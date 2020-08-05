using System;

namespace Panoukos41.Helpers.Mvvm.Navigation
{
    /// <summary>
    /// An interface to use for navigation between pages.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// If the backstack is empty or the Frame Contet is null
        /// this returns an empty string.
        /// </summary>
        string CurrentPageKey { get; }

        /// <summary>
        /// The current parameter passed to the <see cref="NavigateTo(string, string)"/> method for the current page.
        /// </summary>
        string CurrentPageParameter { get; }

        /// <summary>
        /// Indicates if the service can navigate backwards.
        /// </summary>
        bool CanGoBack();

        /// <summary>
        /// If possible, discards the current page and displays the previous page
        /// on the navigation stack.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Displays a new page corresponding to the given key.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page that should be displayed.</param>
        /// <exception cref="ArgumentException">When this method is called for a key that has not been configured earlier.</exception>
        void NavigateTo(string pageKey);

        /// <summary>
        /// Displays a new page corresponding to the given key, and passes a parameter to the new page.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed to the new page.</param>
        /// <exception cref="ArgumentException">When this method is called for a key that has not been configured earlier.</exception>
        void NavigateTo(string pageKey, string parameter);

        /// <summary>
        /// Event raised when fragments are navigated to and from.
        /// </summary>
        event EventHandler<NavigationEventArgs> Navigated;
    }
}