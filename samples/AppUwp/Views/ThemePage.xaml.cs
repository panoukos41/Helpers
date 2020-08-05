using App;
using App.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Panoukos41.Helpers.Services;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;

namespace AppUwp.Views
{
    public abstract class ThemePageBase : ReactivePage<ThemeViewModel> {/* No code needed here */ }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThemePage : ThemePageBase
    {
        public ThemePage()
        {
            InitializeComponent();
            ViewModel = Ioc.Resolve<ThemeViewModel>();

            this.WhenActivated(disposable =>
            {
                SummaryTextBlock.Text = ViewModel.Summary;

                ThemeRadios.SelectedIndex = ThemeService.Default.GetCurrentTheme() switch
                {
                    AppTheme.Light => 1,
                    AppTheme.Dark => 2,
                    _ => 0
                };

                Observable.FromEventPattern<SelectionChangedEventHandler, SelectionChangedEventArgs>(
                    h => ThemeRadios.SelectionChanged += h,
                    h => ThemeRadios.SelectionChanged -= h)
                    .Subscribe(async e =>
                    {
                        var rb = e.Sender as RadioButtons;
                        await ViewModel.ThemeChange.Execute(rb.SelectedIndex switch
                        {
                            1 => 1, // this seems weird but in case we switch the order this would be necessary so we leave it like this.
                            2 => 2,
                            _ => 0
                        });
                    })
                    .DisposeWith(disposable);
            });
        }
    }
}