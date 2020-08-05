using System;
using Windows.UI.Xaml.Controls;

namespace Panoukos41.Helpers.Mvvm.Navigation
{
    /// <summary>
    /// A navigation service to use with UWP Frame, it implements <see cref="INavigationService"/>.
    /// </summary>
    public partial class NavigationService : INavigationService
    {
        private Frame currentFrame;

        /// <summary>
        /// Gets or sets the Frame that should be used for the navigation.
        /// </summary>
        public Frame CurrentFrame
        {
            get => currentFrame ?? throw new NullReferenceException($"{nameof(CurrentFrame)} was not set! Please set it before using the service.");
            set
            {
                if (currentFrame != null)
                {
                    currentFrame.Navigated -= Frame_Navigated;
                }
                currentFrame = value;
                currentFrame.Navigated += Frame_Navigated;
            }
        }

        /// <summary>
        /// Initialize a new <see cref="NavigationService"/>
        /// </summary>
        public NavigationService() { }

        /// <summary>
        /// Initialize a new <see cref="NavigationService"/> providing a <see cref="Frame"/> to use for navigation.
        /// </summary>
        /// <param name="rootframe"></param>
        public NavigationService(Frame rootframe) => CurrentFrame = rootframe;

        /// <summary>
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public NavigationService SetFrame(Frame frame)
        {
            CurrentFrame = frame;
            return this;
        }

        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// If the backstack is empty or the Frame Contet is null
        /// this returns an empty string.
        /// </summary>
        private string PlatformCurrentPageKey
        {
            get
            {
                lock (pagesByKey)
                {
                    if (CurrentFrame.Content == null
                        || CurrentFrame.BackStackDepth == 0)
                    {
                        return string.Empty;
                    }

                    var currentType = CurrentFrame.CurrentSourcePageType;
                    if (currentType == null)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return GetKeyForPage(currentType);
                    }
                }
            }
        }

        /// <summary>
        /// The current parameter passed to the <see cref="NavigateTo(string, string)"/> method for the current page.
        /// </summary>
        private string PlatformCurrentPageParameter { get; set; }

        /// <summary>
        /// Indicates if the <see cref="CurrentFrame"/> can navigate backwards.
        /// </summary>
        private bool PlatformCanGoBack()
            => CurrentFrame.CanGoBack;

        /// <summary>
        /// If possible, discards the current page and displays the previous page
        /// on the navigation stack.
        /// </summary>
        private void PlatformGoBack()
        {
            if (CurrentFrame.CanGoBack)
            {
                CurrentFrame.GoBack();
            }
        }

        /// <summary>
        /// Indicates if the <see cref="CurrentFrame"/> can navigate forward.
        /// </summary>
        private bool PlatformCanGoForward()
            => CurrentFrame.CanGoForward;

        /// <summary>
        /// Check if the CurrentFrame can navigate forward, and if yes, performs
        /// a forward navigation.
        /// </summary>
        private void PlatformGoForward()
        {
            if (CurrentFrame.CanGoForward)
            {
                CurrentFrame.GoForward();
            }
        }

        /// <summary>
        /// Displays a new page corresponding to the given key, and passes a parameter to the new page.
        /// Make sure to call the <see cref="Configure"/> method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed to the new page.</param>
        /// <exception cref="ArgumentException">When this method is called for a key that has not been configured earlier.</exception>
        private void PlatformNavigateTo(string pageKey, string parameter)
        {
            Type Page = CurrentFrame.CurrentSourcePageType;
            string key;

            // If this is null then this is our first navigation to a Root page.
            if (Page == null)
            {
                navigate();
                return;
            }
            // Else this is not a root page navigation so we load the key.
            else
            {
                key = GetKeyForPage(Page);
            }

            // Check that we do not navigate to the same page.
            // If it's the same page (key) with a different parameter it is not the same page.
            if ((key, CurrentPageParameter) == (pageKey, parameter))
            {
                return;
            }
            else
            {
                navigate();
            }

            void navigate()
            {
                lock (pagesByKey)
                {
                    if (!pagesByKey.ContainsKey(pageKey))
                    {
                        throw new ArgumentException($"No such key '{pageKey}'. Did you forget to call NavigationService.Configure?", nameof(pageKey));
                    }
                    CurrentFrame.Navigate(pagesByKey[pageKey], parameter);
                }
            }
        }

        private void Frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            var str = e.Parameter as string;
            if (e.Parameter != null && str == null)
            {
                throw new ArgumentException("Navigation parameter must be a string!", "parameter");
            }
            else
            {
                PlatformCurrentPageParameter = str;
                RaiseNavigated(GetKeyForPage(e.SourcePageType), str);
            }
        }
    }
}