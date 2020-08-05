using Android.OS;
using AndroidX.Fragment.App;
using System;

namespace Panoukos41.Helpers.Mvvm.Navigation
{
    /// <summary>
    /// A navigation service to use with AndroidX fragments, it implements <see cref="INavigationService"/>.
    /// </summary>
    public partial class NavigationService : INavigationService
    {
        private FragmentManager currentManager;
        private int fragmentContainerId;

        private const string BACKSTACK_TAG = "fragment_number_";

        private Fragment LastFragment =>
            CurrentManager.FindFragmentByTag(BACKSTACK_TAG + (CurrentManager.BackStackEntryCount - 1));

        /// <summary>
        /// Key to store the parameter in a fragment. If it does not implement <see cref="INavigationFragment"/>.
        /// </summary>
        public const string NavigationParameterKey = "Fragment_Navigation_Service_Parameter_Key";

        /// <summary>
        /// Gets or sets the FragmentManager that should be used for the navigation.
        /// </summary>
        public virtual FragmentManager CurrentManager
        {
            get => currentManager ?? throw new NullReferenceException($"{nameof(CurrentManager)} was not set! Please set it before using the service.");
            set
            {
                if (currentManager != null)
                {
                    currentManager.BackStackChanged -= FragmentManager_BackStackChanged;
                }
                currentManager = value;
                currentManager.BackStackChanged += FragmentManager_BackStackChanged;
            }
        }

        /// <summary>
        /// Gets or sets the FragmentContainerId that should be used for the navigation.
        /// </summary>
        public virtual int FragmentContainerId
        {
            get => fragmentContainerId != 0
                ? fragmentContainerId
                : throw new NullReferenceException($"{nameof(FragmentContainerId)} was not set! Please set it before using the service.");
            set => fragmentContainerId = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NavigationService"/>.
        /// </summary>
        public NavigationService()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NavigationService"/> with
        /// values for the containerId and the fragmentmanager.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="fragmentContainerId"></param>
        public NavigationService(FragmentManager manager, int fragmentContainerId)
        {
            CurrentManager = manager;
            FragmentContainerId = fragmentContainerId;
        }

        /// <summary>
        /// </summary>
        /// <param name="fragmentManager"></param>
        /// <returns></returns>
        public virtual NavigationService SetFragmentManager(FragmentManager fragmentManager)
        {
            if (currentManager == null || !Equals(currentManager, fragmentManager))
            {
                CurrentManager = fragmentManager;
            }
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="fragmentContainerId"></param>
        /// <returns></returns>
        public virtual NavigationService SetFragmentContainerId(int fragmentContainerId)
        {
            if (this.fragmentContainerId == 0 || !Equals(this.fragmentContainerId, FragmentContainerId))
            {
                FragmentContainerId = fragmentContainerId;
            }
            return this;
        }

        /// <summary>
        /// The key corresponding to the currently displayed page. If the backstack is empty or the
        /// Frame Contet is null this returns an empty string.
        /// </summary>
        private string PlatformCurrentPageKey => PlatformCanGoBack()
            ? GetKeyForPage(LastFragment.GetType())
            : string.Empty;

        /// <summary>
        /// The current parameter passed to the <see cref="NavigateTo(string, string)"/> method for
        /// the current page.
        /// </summary>
        private string PlatformCurrentPageParameter
        {
            get
            {
                if (!PlatformCanGoBack()) return string.Empty;

                var frag = LastFragment;
                return frag is INavigationFragment navFrag
                    ? navFrag.NavigationParameter
                    : frag.Arguments.GetString(NavigationParameterKey, string.Empty);
            }
        }

        /// <summary>
        /// Indicates if the <see cref="CurrentManager"/> can navigate backwards.
        /// </summary>
        private bool PlatformCanGoBack()
            => CurrentManager.BackStackEntryCount > 1;

        /// <summary>
        /// If possible, discards the current page and displays the previous page on the navigation stack.
        /// </summary>
        private void PlatformGoBack()
        {
            if (PlatformCanGoBack())
            {
                CurrentManager.PopBackStack();
            }
        }

        /// <summary>
        /// Indicates if the <see cref="CurrentManager"/> can navigate forward.
        /// </summary>
        private bool PlatformCanGoForward()
            => false;

        /// <summary>
        /// NOT IMPLEMENTED YET.
        /// Check if the CurrentFrame can navigate forward, and if yes, performs a forward navigation.
        /// </summary>
        private void PlatformGoForward()
        {
            throw new PlatformNotSupportedException("GoForward() is not implemented yet for android!");
        }

        /// <summary>
        /// Displays a new page corresponding to the given key, and passes a parameter to the new
        /// page. Make sure to call the <see cref="Configure"/> method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed to the new page.</param>
        /// <exception cref="ArgumentException">
        /// When this method is called for a key that has not been configured earlier.
        /// </exception>
        private void PlatformNavigateTo(string pageKey, string parameter)
        {
            if (!PlatformCanGoBack())
            {
                navigate();
                return;
            }

            // Check that we do not navigate to the same page. If it's the same page (key) with a
            // different parameter it is not the same page.
            if ((CurrentPageKey, CurrentPageParameter) == (pageKey, parameter))
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
                    Fragment fragment = (Fragment)Activator.CreateInstance(pagesByKey[pageKey]);
                    if (fragment is INavigationFragment navFragment)
                    {
                        navFragment.NavigationParameter = parameter;
                    }
                    else
                    {
                        Bundle bundle = new Bundle();
                        bundle.PutString(NavigationParameterKey, parameter);
                        fragment.Arguments = bundle;
                    }

                    CurrentManager
                        .BeginTransaction()
                        .AddToBackStack(BACKSTACK_TAG + CurrentManager.BackStackEntryCount)
                        .Replace(FragmentContainerId, fragment, BACKSTACK_TAG + CurrentManager.BackStackEntryCount)
                        .Commit();
                }
            }
        }

        private void FragmentManager_BackStackChanged(object sender, EventArgs e) =>
            RaiseNavigated(CurrentPageKey, CurrentPageParameter);
    }
}