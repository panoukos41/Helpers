using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Panoukos41.Helpers.Mvvm.Navigation
{
    /// <summary>
    /// A service used to navigate between pages.
    /// </summary>
    public partial class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> pagesByKey = new Dictionary<string, Type>();

        /// <summary>
        /// Event raised when pages are navigated to and from.
        /// </summary>
        public event EventHandler<NavigationEventArgs> Navigated;

        /// <summary>
        /// Get the key for the current displayed page.
        /// </summary>
        public virtual string CurrentPageKey =>
            PlatformCurrentPageKey;

        /// <summary>
        /// Get the parameter that was passed for the current displayed page.
        /// </summary>
        public virtual string CurrentPageParameter =>
            PlatformCurrentPageParameter;

        /// <summary>
        /// True if the service can navigate backwards.
        /// </summary>
        public virtual bool CanGoBack() =>
            PlatformCanGoBack();

        /// <summary>
        /// Tells the service to navigate backwards.
        /// </summary>
        public virtual void GoBack() =>
            PlatformGoBack();

        /// <summary>
        /// True if the service can navigate forward.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanGoForward() =>
            PlatformCanGoForward();

        /// <summary>
        /// Tells the service to navigate forward.
        /// </summary>
        public virtual void GoForward() =>
            PlatformGoForward();

        /// <summary>
        /// Tells the service to navigate to the specified key.
        /// Don't forget to Configure the service with key/page pairs first.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        public virtual void NavigateTo(string pageKey) =>
            NavigateTo(pageKey, null);

        /// <summary>
        /// Tells the service to navigate to the specified key and pass a parameter.
        /// Don't forget to Configure the service with key/page pairs first.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <param name="parameter">The parameter that will be passed.</param>
        public virtual void NavigateTo(string pageKey, string parameter) =>
            PlatformNavigateTo(pageKey, parameter);

        /// <summary>
        /// Adds a key/page pair to the navigation service.
        /// </summary>
        /// <param name="key">The key that will be used later
        /// in the <see cref="NavigateTo(string)"/> or <see cref="NavigateTo(string, string)"/> methods.</param>
        /// <param name="pageType">The type of the page corresponding to the key.</param>
        public NavigationService Configure(string key, Type pageType)
        {
            lock (pagesByKey)
            {
                if (pagesByKey.ContainsKey(key))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }

                if (pagesByKey.Any(p => p.Value == pageType))
                {
                    throw new ArgumentException(
                        "This type is already configured with key " + pagesByKey.First(p => p.Value == pageType).Key);
                }

                pagesByKey.Add(key, pageType);
            }
            return this;
        }

        /// <summary>
        /// Gets the key corresponding to a given page type.
        /// </summary>
        /// <param name="page">The type of the page for which the key must be returned.</param>
        /// <returns>The key corresponding to the page type.</returns>
        public virtual string GetKeyForPage(Type page)
        {
            lock (pagesByKey)
            {
                return pagesByKey.ContainsValue(page)
                    ? pagesByKey.FirstOrDefault(p => p.Value == page).Key
                    : throw new ArgumentException($"The page '{page.Name}' is unknown by the NavigationService");
            }
        }

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It is used in the platform specific code.")]
        private void RaiseNavigated(string pageKey, string parameter)
            => Navigated?.Invoke(this, new NavigationEventArgs(pageKey, parameter));
    }
}