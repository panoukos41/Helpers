using App;
using App.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace AppUwp.Views
{
    public abstract class HomePageBase : ReactivePage<HomeViewModel> { /* No code needed here */ }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : HomePageBase
    {
        public HomePage()
        {
            InitializeComponent();
            ViewModel = Ioc.Resolve<HomeViewModel>();

            this.WhenActivated(disposable =>
            {
                DestinationsList.ItemsSource = ViewModel.Destinations;

                DestinationsList.Events().Tapped.Subscribe(e =>
                {
                    var a = (string)((FrameworkElement)e.OriginalSource).DataContext;
                    ViewModel.Navigate.Execute(a).Subscribe();
                })
                .DisposeWith(disposable);
            });
        }
    }
}