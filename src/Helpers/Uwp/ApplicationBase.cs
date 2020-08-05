using Panoukos41.Helpers.Services;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Panoukos41.Helpers
{
    /// <summary>
    /// Provides the base class for your Universal Windows Platform application.
    /// Takes care of the creation and initialization when the application starts.
    /// If the app was terminated and it's launched from the main app tile then
    /// the class will try to restore the navigation state of the <see cref="RootFrame"/>
    /// depending on the <see cref="RestoreNavigationStateOnResume"/> value.
    /// </summary>
    public abstract class ApplicationBase : Application
    {
        #region Properties/Fields

        private const string NavigationDataProtectionProvider = "LOCAL=user";
        private const string NavigationStateFileName = "_NavigationState.xml";

        /// <summary>
        /// Get or set the <see cref="Window.Content"/> of the <see cref="Window.Current"/>.
        /// </summary>
        protected static UIElement WindowContent
        {
            get => Window.Current.Content;
            set => Window.Current.Content = value;
        }

        /// <summary>
        /// Gets the Application object for the current application.
        /// </summary>
        public static new ApplicationBase Current { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the application is suspending.
        /// </summary>
        public bool IsSuspending { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the application is resuming.
        /// </summary>
        public bool IsResuming { get; private set; }

        /// <summary>
        /// Gets or sets whether the application triggers navigation restore on app resume.
        /// Default = true.
        /// </summary>
        protected virtual bool RestoreNavigationStateOnResume { get; set; } = true;

        /// <summary>
        /// Gets the shell user interface, which is the <see cref="Window.Content"/>
        /// </summary>
        protected UIElement Shell { get; private set; }

        /// <summary>
        /// A frame to use for the application.
        /// </summary>
        protected Frame RootFrame { get; set; }

        /// <summary>
        /// Default GestureService. Used to provide Backwards and Forwards navigation callbacks.
        /// Use <see cref="OnCreateGestureService"/> to inject your own.
        /// </summary>
        protected IGestureService GestureService { get; private set; }

        #endregion

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        protected ApplicationBase()
        {
            Current = this;
            Suspending += OnSuspending;
            Resuming += OnResuming;
        }

        /// <summary>
        /// Invoke the <see cref="Window.Activate"/> of the <see cref="Window.Current"/>.
        /// </summary>
        protected static void WindowActivate()
            => Window.Current.Activate();

        /// <summary>
        /// Ovveride with logic as to what happens when <see cref="IGestureService.GoForwardRequested"/> is fired,
        /// by default it will call <see cref="Frame.GoForward()"/> if its possible.
        /// </summary>
        protected virtual void OnGoForwardRequested(object sender, GestureEventArgs e)
        {
            if (!e.Handled && RootFrame.CanGoForward)
            {
                RootFrame.GoForward();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Ovveride with logic as to what happens when <see cref="IGestureService.GoBackRequested"/> is fired,
        /// by default it will call <see cref="Frame.GoBack()"/> if its possible.
        /// </summary>
        protected virtual void OnGoBackRequested(object sender, GestureEventArgs e)
        {
            if (!e.Handled && RootFrame.CanGoBack)
            {
                RootFrame.GoBack();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Here you can initialize services etc. Invoked only if the Window.Current.Content
        /// is null, before any other system call.<br/>
        /// <see href="https://docs.microsoft.com/en-us/windows/uwp/launch-resume/activate-an-app#remarks">Read more!</see>
        /// </summary>
        protected virtual Task OnInitializeAsync(IActivatedEventArgs args)
            => Task.CompletedTask;

        /// <summary>
        /// Invoked when the application is suspending.
        /// </summary>
        protected virtual Task OnSuspendingApplicationAsync()
            => Task.CompletedTask;

        /// <summary>
        /// Invoked whenever the app is resuming from suspend and terminate states.
        /// </summary>
        protected virtual Task OnResumeApplicationAsync()
            => Task.CompletedTask;

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            IsSuspending = true;
            try
            {
                var deferral = e.SuspendingOperation.GetDeferral();
                await OnSuspendingApplicationAsync();

                if (RestoreNavigationStateOnResume)
                {
                    try
                    {
                        await SaveNavigationStateAsync();
                    }
                    catch
                    {
                    }
                }
                deferral.Complete();
            }
            finally
            {
                IsSuspending = false;
            }
        }

        private async void OnResuming(object sender, object e)
        {
            IsResuming = true;
            try
            {
                await OnResumeApplicationAsync().ConfigureAwait(false);
            }
            finally
            {
                IsResuming = false;
            }
        }

        #region OnCreate

        /// <summary>
        /// Create your own rootframe that will be passed to the <see cref="OnCreateShell(Frame)"/>
        /// </summary>
        /// <returns></returns>
        protected virtual Frame OnCreateRootFrame()
            => RootFrame = new Frame();

        /// <summary>
        /// Creates your shell, this will be the current <see cref="Window.Content"/>
        /// If you don't provide a shell <see cref="RootFrame"/> becomes the <see cref="Window.Content"/>
        /// </summary>
        /// <param name="rootFrame">The <see cref="RootFrame"/></param>
        protected virtual UIElement OnCreateShell(Frame rootFrame)
            => rootFrame;

        /// <summary>
        /// Initialize your own <see cref="GestureService"/>, invoked
        /// just before <see cref="OnInitializeAsync(IActivatedEventArgs)"/>
        /// </summary>
        protected virtual IGestureService OnCreateGestureService()
            => new GestureService();

        #endregion

        #region Activation

        /// <summary>
        /// Invoked when the application is launched. Override this method to perform application
        /// initialization and to display initial content in the associated Window.
        /// </summary>
        /// <param name="args">Event data for the event.</param>
        sealed protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (WindowContent == null)
            {
                OnCreateRootFrame();
                GestureService = OnCreateGestureService();
                GestureService.GoBackRequested += OnGoBackRequested;
                GestureService.GoForwardRequested += OnGoForwardRequested;
                await OnInitializeAsync(args);
                WindowContent = OnCreateShell(RootFrame);
            }

            // Default state
            if (args.Kind == ActivationKind.Launch)
            {
                await OnLaunchedAsync(args);
            }

            // Try to restore state
            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated
                && args.TileId == "App"
                && RestoreNavigationStateOnResume
                && await CanRestoreNavigationStateAsync())
            {
                try
                {
                    await RestoreNavigationStateAsync();
                    await OnResumeApplicationAsync();
                }
                catch
                {
                }
            }
            WindowActivate();
        }

        /// <summary>
        /// Invoked when the application ActivationKind is <see cref="ActivationKind.Launch"/>. <br/>
        /// This means this is executed everytime the user launches the application from the tile.
        /// </summary>
        /// <param name="args">Event data for the event.</param>
        protected abstract Task OnLaunchedAsync(LaunchActivatedEventArgs args);

        #endregion

        #region RestoreNavigation

        private async Task<bool> CanRestoreNavigationStateAsync()
            => await ApplicationData.Current.LocalFolder.TryGetItemAsync(NavigationStateFileName) != null;

        private async Task RestoreNavigationStateAsync()
        {
            // Get the input stream for the SessionState file
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(NavigationStateFileName);
            using var inStream = await file.OpenSequentialReadAsync();

            var memoryStream = new MemoryStream();
            var provider = new DataProtectionProvider(NavigationDataProtectionProvider);

            // Decrypt the prevously saved session data.
            await provider.UnprotectStreamAsync(inStream, memoryStream.AsOutputStream());
            // Deserialize the Session State
            var data = new DataContractSerializer(typeof(string)).ReadObject(memoryStream);
            RootFrame.SetNavigationState((string)data);
        }

        private async Task SaveNavigationStateAsync()
        {
            MemoryStream sessionData = new MemoryStream();
            var session = RootFrame.GetNavigationState();
            new DataContractSerializer(typeof(string)).WriteObject(sessionData, session);

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(NavigationStateFileName, CreationCollisionOption.ReplaceExisting);
            using var outStream = await file.OpenAsync(FileAccessMode.ReadWrite);

            var provider = new DataProtectionProvider(NavigationDataProtectionProvider);
            // Encrypt the session data and write it to disk.
            await provider.ProtectStreamAsync(sessionData.AsInputStream(), outStream);
            await outStream.FlushAsync();
        }

        #endregion
    }
}