using Android.OS;
using Android.Views;
using Android.Widget;
using App;
using App.ViewModels;
using Panoukos41.Helpers;
using ReactiveUI;
using System.Reactive.Disposables;

namespace AppDroid.Views
{
    public class GesturePage : ReactiveUI.AndroidX.ReactiveFragment<GestureViewModel>
    {
        #region Controls

        public TextView SummaryTextView { get; private set; }

        public TextView SwitchText { get; private set; }

        public Switch Switch { get; private set; }

        #endregion

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Ioc.Resolve<GestureViewModel>();

            this.WhenActivated(disposable =>
            {
                SummaryTextView.Text = ViewModel.Summary;

                this.OneWayBind(ViewModel, vm => vm.Block, v => v.SwitchText.Text, prop => prop ? "Blocked" : "Not Blocked")
                    .DisposeWith(disposable);

                this.Bind(ViewModel, vm => vm.Block, v => v.Switch.Checked)
                    .DisposeWith(disposable);
            });
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
            this.InflateAndWireUpControls(inflater, container, Resource.Layout.fragment_gesturepage);
    }
}