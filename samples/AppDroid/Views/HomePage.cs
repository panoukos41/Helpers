using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using App;
using App.ViewModels;
using AppDroid.Adapters;
using Panoukos41.Helpers;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AppDroid.Views
{
    public class HomePage : ReactiveUI.AndroidX.ReactiveFragment<HomeViewModel>
    {
        public RecyclerView DestinationsRv { get; private set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Ioc.Resolve<HomeViewModel>();

            this.WhenActivated(disposable =>
            {
                var adapter = new DestinationsAdapter(ViewModel.Destinations);
                adapter.DisposeWith(disposable);
                adapter.WhenItemSelected
                    .Subscribe(x =>
                    {
                        System.Diagnostics.Debug.WriteLine(x);
                        ViewModel.Navigate.Execute(x).Subscribe();
                    })
                    .DisposeWith(disposable);

                DestinationsRv.SetLayoutManager(new LinearLayoutManager(Context));
                DestinationsRv.SetAdapter(adapter);
            });
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
            this.InflateAndWireUpControls(inflater, container, Resource.Layout.fragment_home);
    }
}