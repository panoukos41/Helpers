using Android.OS;
using Android.Views;
using Android.Widget;
using App;
using App.ViewModels;
using Panoukos41.Helpers;
using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace AppDroid.Views
{
    public class FilesPage : ReactiveUI.AndroidX.ReactiveFragment<FilesViewModel>
    {
        #region Controls

        public TextView SummaryTextView { get; private set; }

        public EditText SaveEditText { get; private set; }

        public Button SaveButton { get; private set; }

        public TextView ReadTextView { get; private set; }

        public Button ReadButton { get; private set; }

        #endregion

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Ioc.Resolve<FilesViewModel>();

            this.WhenActivated(disposable =>
            {
                SummaryTextView.Text = ViewModel.Summary;

                this.Bind(ViewModel, vm => vm.Text, v => v.SaveEditText.Text)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.Save, v => v.SaveButton)
                    .DisposeWith(disposable);

                ViewModel.Read
                    .Subscribe(result => ReadTextView.Text = result)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.Read, v => v.ReadButton)
                    .DisposeWith(disposable);
            });
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
            this.InflateAndWireUpControls(inflater, container, Resource.Layout.fragment_filepage);
    }
}