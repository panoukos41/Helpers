using App.Shell;
using Panoukos41.Helpers.Mvvm.Navigation;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;

namespace App.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<string> Destinations { get; }

        public ReactiveCommand<string, Unit> Navigate { get; private set; }

        public HomeViewModel(IShellEvents shellEvents, INavigationService navigationService)
        {
            Destinations = new ObservableCollection<string>();

            foreach (string str in PageKeys.GetValues())
                Destinations.Add(str);

            Navigate = ReactiveCommand.Create<string>(
                page => navigationService.NavigateTo(page));

            this.WhenActivated((CompositeDisposable disposable) =>
            {
                shellEvents.SetTitle("Home");

                Disposable
                    .Create(() =>
                    {
                        // This is called when the view model is out of view.
                    })
                    .DisposeWith(disposable);
            });
        }
    }
}