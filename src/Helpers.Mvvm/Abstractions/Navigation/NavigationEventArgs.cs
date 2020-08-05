using System;

namespace Panoukos41.Helpers.Mvvm
{
    /// <summary>
    /// Event arguments for the a NavigationService. for example its used in the
    /// FramgmentNavigation service that misses navigated event.
    /// </summary>
    public class NavigationEventArgs : EventArgs
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="parameter"></param>
        public NavigationEventArgs(string pageKey, string parameter)
        {
            PageKey = pageKey;
            Parameter = parameter;
        }

        /// <summary>
        /// Type of the page.
        /// </summary>
        public string PageKey { get; set; }

        /// <summary>
        /// The parameter passed to the page
        /// </summary>
        public string Parameter { get; set; }
    }
}