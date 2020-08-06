using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.DrawerLayout.Widget;
using App;
using App.Shell;
using Google.Android.Material.AppBar;
using Panoukos41.Helpers.AndroidX.Extensions;
using Panoukos41.Helpers.Mvvm.Navigation;
using Panoukos41.Helpers.Services;
using ReactiveUI;
using ReactiveUI.AndroidX;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AppDroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.App", MainLauncher = true)]
    public class MainActivity : ReactiveAppCompatActivity<object>
    {
        #region Controls

        public DrawerLayout Drawer { get; private set; }

        public MaterialToolbar Toolbar { get; private set; }

        #endregion

        private NavigationService NavigationService { get; set; }

        private GestureService GestureService { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ThemeService.FirstRun();

            SetContentView(Resource.Layout.activity_main);
            this.WireUpControls();
            this.SetToolbarWithDrawer(Toolbar, Drawer, 0, 0);

            Ioc.Resolve<PermissionsService>().SetActivity(this);
            Ioc.Resolve<FileService>().SetActivity(this);

            NavigationService = Ioc.Resolve<NavigationService>()
                .SetFragmentManager(SupportFragmentManager)
                .SetFragmentContainerId(Resource.Id.FragmentContainerView);

            GestureService = Ioc.Resolve<GestureService>();

            this.WhenActivated((CompositeDisposable disposable) =>
            {
                Observable.FromEventPattern<GestureEventArgs>(
                    h => GestureService.GoBackRequested += h,
                    h => GestureService.GoBackRequested -= h)
                    .Subscribe(x =>
                    {
                        if (!x.EventArgs.Handled && NavigationService.CanGoBack())
                        {
                            NavigationService.GoBack();
                            x.EventArgs.Handled = true;
                        }
                    })
                    .DisposeWith(disposable);

                Ioc.Resolve<IShellEvents>()
                    .WhenTitleSet()
                    .Throttle(TimeSpan.FromMilliseconds(150))
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(title => Toolbar.Title = title)
                    .DisposeWith(disposable);

                Disposable
                    .Create(() =>
                    {
                    })
                    .DisposeWith(disposable);
            });

            NavigationService.NavigateTo(PageKeys.HomePage);
        }

        protected override void OnActivityResult(int requestCode, Result result, Intent data)
        {
            base.OnActivityResult(requestCode, result, data);

            Ioc.Resolve<FileService>().OnActivityResult(requestCode, result, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Ioc.Resolve<PermissionsService>().OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (!GestureService.RaiseGoBackRequested(this))
            {
                // base.OnBackPressed(); This will actually remove our fragment instead of closing.
                Finish();
            }
        }
    }
}