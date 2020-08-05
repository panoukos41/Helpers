using App;
using App.ViewModels;
using ReactiveUI;

namespace AppUwp.Views
{
    public abstract class TabsPageBase : ReactivePage<TabsViewModel> {/* No code needed here */ }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TabsPage : TabsPageBase
    {
        public TabsPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Resolve<TabsViewModel>();

            this.WhenActivated(disposable =>
            {
            });
        }
    }
}