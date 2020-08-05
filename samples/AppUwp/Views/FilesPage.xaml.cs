using App;
using App.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AppUwp.Views
{
    public abstract class FilesPageBase : ReactivePage<FilesViewModel> {/* No code needed here */ }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilesPage : FilesPageBase
    {
        public FilesPage()
        {
            InitializeComponent();
            ViewModel = Ioc.Resolve<FilesViewModel>();

            this.WhenActivated(disposable =>
            {
                SummaryTextBlock.Text = ViewModel.Summary;

                this.Bind(ViewModel, vm => vm.Text, v => v.SaveTextBox.Text, text => text ?? "", text => text ?? "")
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.Save, v => v.SaveButton)
                    .DisposeWith(disposable);

                ViewModel.Read
                    .Subscribe(result => ReadTextBox.Text = result)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.Read, v => v.ReadButton)
                    .DisposeWith(disposable);
            });
        }
    }
}