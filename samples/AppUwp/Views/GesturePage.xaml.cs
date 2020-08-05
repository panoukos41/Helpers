using App;
using App.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace AppUwp.Views
{
    public abstract class GesturePageBase : ReactivePage<GestureViewModel> {/* No code needed here */ }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GesturePage : GesturePageBase
    {
        public GesturePage()
        {
            InitializeComponent();
            ViewModel = Ioc.Resolve<GestureViewModel>();

            this.WhenActivated(disposable =>
            {
                SummaryTextBlock.Text = ViewModel.Summary;

                this.OneWayBind(ViewModel, vm => vm.Block, v => v.SwitchTextBlock.Text, prop => prop ? "Blocked" : "Not Blocked")
                    .DisposeWith(disposable);

                this.Bind(ViewModel, vm => vm.Block, v => v.Switch.IsOn)
                    .DisposeWith(disposable);
            });
        }
    }
}