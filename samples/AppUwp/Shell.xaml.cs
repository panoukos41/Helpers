using App;
using App.Shell;
using Microsoft.UI.Xaml.Controls;
using Panoukos41.Helpers.Mvvm;
using Panoukos41.Helpers.Mvvm.Navigation;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using MUXC = Microsoft.UI.Xaml.Controls;

namespace AppUwp
{
    public abstract class ShellBase : ReactiveUserControl<object> { /* No code needed here */ }

    public sealed partial class Shell : ShellBase
    {
        private NavigationService NavigationService { get; }

        public Shell(Frame rootFrame)
        {
            InitializeComponent();
            Presenter.Content = rootFrame;

            NavigationService = Ioc.Resolve<NavigationService>();

            this.WhenActivated(disposable =>
            {
                Ioc.Resolve<IShellEvents>()
                    .WhenTitleSet()
                    .Throttle(TimeSpan.FromMilliseconds(150))
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(title => NavigationView.Header = title)
                    .DisposeWith(disposable);

                Observable.FromEventPattern<NavigationEventArgs>(
                    h => NavigationService.Navigated += h,
                    h => NavigationService.Navigated -= h)
                    .Subscribe(e =>
                    {
                        NavigationView.IsBackEnabled = NavigationService.CanGoBack();
                    })
                    .DisposeWith(disposable);

                Observable.FromEventPattern<TypedEventHandler<MUXC.NavigationView, MUXC.NavigationViewBackRequestedEventArgs>, 
                    MUXC.NavigationViewBackRequestedEventArgs>(
                    h => NavigationView.BackRequested += h,
                    h => NavigationView.BackRequested -= h)
                    .Subscribe(e =>
                    {
                        NavigationService.GoBack();
                    })
                    .DisposeWith(disposable);
            });
        }
    }
}