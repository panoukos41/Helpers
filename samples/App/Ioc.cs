using App.Shell;
using App.ViewModels;
using Autofac;
using Autofac.Builder;
using System;

namespace App
{
    public static class Ioc
    {
        private static IContainer container;

        public static TService Resolve<TService>()
        {
            return container.Resolve<TService>();
        }

        public static void ConfigureServices(Action<ContainerBuilder> configure)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            configure(containerBuilder);
            container = containerBuilder.Build(ContainerBuildOptions.None);
        }

        public static void AddApplication(this ContainerBuilder builder)
        {
            // Register ViewModels
            builder.RegisterType<HomeViewModel>().AsSelf();
            builder.RegisterType<FilesViewModel>().AsSelf();
            builder.RegisterType<GestureViewModel>().AsSelf();
            builder.RegisterType<TabsViewModel>().AsSelf();
            builder.RegisterType<ThemeViewModel>().AsSelf();
            builder.RegisterInstance(new ShellEvents()).As<IShellEvents>();

            // Other Cross App services.
        }
    }
}