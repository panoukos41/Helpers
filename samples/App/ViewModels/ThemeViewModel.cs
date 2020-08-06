using App.Shell;
using Panoukos41.Helpers.Services;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;

namespace App.ViewModels
{
    public class ThemeViewModel : BaseViewModel
    {
        public ReactiveCommand<int, Unit> ThemeChange { get; private set; }

        public ThemeViewModel(IThemeService themeService, IShellEvents shellEvents)
        {
            ThemeChange = ReactiveCommand.Create<int>(
                code => themeService.SetTheme((AppTheme)code));

            this.WhenActivated((CompositeDisposable disposable) =>
            {
                shellEvents.SetTitle("Theme Example");

                Disposable
                    .Create(() =>
                    {
                        // This is called when the view model is out of view.
                    })
                    .DisposeWith(disposable);
            });

            Summary = "This sample shows the ThemeService.\n" +
                "When the switch is ON navigation will not work.\nThis is because we handle it inside the viewmodel.";
        }
    }
}