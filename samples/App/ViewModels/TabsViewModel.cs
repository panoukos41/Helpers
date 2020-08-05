using App.Shell;
using ReactiveUI;
using System.Reactive.Disposables;

namespace App.ViewModels
{
    public class TabsViewModel : BaseViewModel
    {
        public TabsViewModel(IShellEvents shellEvents)
        {
            this.WhenActivated((CompositeDisposable disposable) =>
            {
                shellEvents.SetTitle("Tabs Example");

                Disposable
                    .Create(() =>
                    {
                        // This is called when the view model is out of view.
                    })
                    .DisposeWith(disposable);
            });

            Summary = "This is an example of PagerTabsManager for android.\n" +
                "We use a viewpager (PagerAdapter) and a tablayout.";
        }
    }
}