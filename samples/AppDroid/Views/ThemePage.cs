using Android.OS;
using Android.Views;
using Android.Widget;
using App;
using App.ViewModels;
using Panoukos41.Helpers;
using Panoukos41.Helpers.Services;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AppDroid.Views
{
    public class ThemePage : ReactiveUI.AndroidX.ReactiveFragment<ThemeViewModel>
    {
        #region Controls

        public TextView SummaryTextView { get; private set; }

        public TextView SwitchText { get; private set; }

        public RadioGroup ThemeRadioGroup { get; private set; }

        #endregion

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Ioc.Resolve<ThemeViewModel>();

            this.WhenActivated(disposable =>
            {
                SummaryTextView.Text = ViewModel.Summary;

                ThemeRadioGroup.Check(ThemeService.Default.GetCurrentTheme() switch
                {
                    AppTheme.Light => Resource.Id.LightRadio,
                    AppTheme.Dark => Resource.Id.DarkRadio,
                    _ => Resource.Id.DefaultRadio
                });

                ThemeRadioGroup.Events()
                    .CheckedChange
                    .Subscribe(async e =>
                    {
                        await ViewModel.ThemeChange.Execute(e.CheckedId switch
                        {
                            Resource.Id.LightRadio => 1,
                            Resource.Id.DarkRadio => 2,
                            _ => 0
                        });
                    })
                    .DisposeWith(disposable);
            });
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
            this.InflateAndWireUpControls(inflater, container, Resource.Layout.fragment_themepage);
    }
}