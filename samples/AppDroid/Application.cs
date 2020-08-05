using Android.App;
using Android.Runtime;
using App;
using AppDroid.Views;
using Autofac;
using Panoukos41.Helpers.Mvvm.Navigation;
using Panoukos41.Helpers.Services;
using System;

namespace AppDroid
{
    [Application]
    public class Application : Android.App.Application
    {
        protected Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Ioc.ConfigureServices(builder =>
            {
                builder.AddApplication();

                builder.RegisterInstance(NewNavigationService)
                    .As<INavigationService>().AsSelf();

                builder.RegisterType<ThemeService>()
                    .As<IThemeService>().AsSelf();

                builder.RegisterInstance(new GestureService())
                    .As<IGestureService>().AsSelf();

                builder.RegisterInstance(new FileService())
                    .As<IFileService>().AsSelf();

                builder.RegisterInstance(new PermissionsService())
                    .As<IPermissionsService>().AsSelf();
            });
        }

        NavigationService NewNavigationService => new NavigationService()
            .Configure(PageKeys.HomePage, typeof(HomePage))
            .Configure(PageKeys.FilesPage, typeof(FilesPage))
            .Configure(PageKeys.GesturePage, typeof(GesturePage))
            .Configure(PageKeys.TabsPage, typeof(TabsPage))
            .Configure(PageKeys.ThemePage, typeof(ThemePage));
    }
}