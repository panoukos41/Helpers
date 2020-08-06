using App.Shell;
using Panoukos41.Helpers.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace App.ViewModels
{
    public class GestureViewModel : BaseViewModel
    {
        [Reactive] public bool Block { get; set; }

        public ReactiveCommand<Unit, Unit> Switch { get; }

        public GestureViewModel(IGestureService gestureService, IShellEvents shellEvents)
        {
            Switch = ReactiveCommand.Create(() => { Block = !Block; });

            this.WhenActivated((CompositeDisposable disposable) =>
            {
                shellEvents.SetTitle("Gesture Example");

                Observable.FromEventPattern<GestureEventArgs>(
                    h => gestureService.GoBackRequested += h,
                    h => gestureService.GoBackRequested -= h)
                    .Subscribe(e =>
                    {
                        if (!e.EventArgs.Handled && Block)
                        {
                            e.EventArgs.Handled = true;
                        }
                    })
                    .DisposeWith(disposable);

                Disposable
                    .Create(() =>
                    {
                        // This is called when the view model is out of view.
                    })
                    .DisposeWith(disposable);
            });

            Summary = "This sample shows the GesstureService.\nWhen the switch is ON navigation will not work.\n" +
                "This is because we handle it inside the viewmodel and we use the service in our main activity for back navigation.";
        }
    }
}