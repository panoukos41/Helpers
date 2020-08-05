using App;
using AppUwp.Views;
using Autofac;
using Panoukos41.Helpers;
using Panoukos41.Helpers.Mvvm.Navigation;
using Panoukos41.Helpers.Services;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppUwp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : ApplicationBase
    {
        private NavigationService NavigationService { get; }

        public App() : base()
        {
            NavigationService = new NavigationService();
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            NavigationService
                .SetFrame(RootFrame)
                .Configure(PageKeys.HomePage, typeof(HomePage))
                .Configure(PageKeys.FilesPage, typeof(FilesPage))
                .Configure(PageKeys.GesturePage, typeof(GesturePage))
                .Configure(PageKeys.TabsPage, typeof(TabsPage))
                .Configure(PageKeys.ThemePage, typeof(ThemePage));

            Ioc.ConfigureServices(builder =>
            {
                builder.AddApplication();

                builder.RegisterInstance(NavigationService)
                    .As<INavigationService>().AsSelf();

                builder.RegisterType<ThemeService>()
                    .As<IThemeService>().AsSelf();

                builder.RegisterInstance(GestureService)
                    .As<IGestureService>().AsSelf();

                builder.RegisterType<FileService>()
                    .As<IFileService>().AsSelf();

                builder.RegisterType<PermissionsService>()
                    .As<IPermissionsService>().AsSelf();
            });

            GestureService.GoBackRequested += (s, e) =>
            {
                if (!e.Handled && NavigationService.CanGoBack())
                {
                    NavigationService.GoBack();
                    e.Handled = true;
                }
            };

            GestureService.GoForwardRequested += (s, e) =>
            {
                if (!e.Handled && NavigationService.CanGoForward())
                {
                    NavigationService.GoForward();
                    e.Handled = true;
                }
            };

            return Task.CompletedTask;
        }

        protected override Task OnLaunchedAsync(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Suspended)
            {
                NavigationService.NavigateTo(PageKeys.HomePage);
            }

            return Task.CompletedTask;
        }

        protected override UIElement OnCreateShell(Frame rootFrame)
        {
            return new Shell(rootFrame);
        }
    }
}