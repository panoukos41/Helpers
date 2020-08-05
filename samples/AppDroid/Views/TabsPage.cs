using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.ViewPager.Widget;
using App;
using App.ViewModels;
using Google.Android.Material.Tabs;
using Panoukos41.Helpers;
using Panoukos41.Helpers.AndroidX.Tabs;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;

namespace AppDroid.Views
{
    public class TabsPage : ReactiveUI.AndroidX.ReactiveFragment<TabsViewModel>, IPagerTabs
    {
        #region Controls

        public ViewPager ViewPager { get; private set; }

        public TabLayout TabLayout { get; private set; }

        #endregion

        // List needed by the PagerTabsManager.
        public List<(string, Fragment)> Tabs { get; } =
            new List<(string, Fragment)>
            {
                ("First", FooFragment.New("First")),
                ("Second", FooFragment.New("Second")),
                ("Third", FooFragment.New("Third")),
                ("Fourth", FooFragment.New("Fourth")),
                ("Fifth", FooFragment.New("Fifth")),
            };

        // Register reactive observables.
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Ioc.Resolve<TabsViewModel>();

            this.WhenActivated(disposable =>
            {
                // create a class that will combine the viewpager with the tablayout (we can even use findviewbyid).
                // then get the first child so that we can set a text on it (that's why we cast it back to its original class.
                var firstView = new PagerTabsManager()
                    .SetViewPager(ViewPager)
                    .SetTabLayout(TabLayout)
                    .Attach(ChildFragmentManager, Tabs)
                    .GetTabContent(0)
                    .Cast<FooFragment>();

                // When the first fragment is activated we set its text property to the summary.
                firstView.WhenActivated(dis =>
                {
                    firstView.TextView.Text = ViewModel.Summary;
                })
                .DisposeWith(disposable);
            });
        }

        // InflateAndWireUpControls combines the view inflation and WireUpControls of reactiveui.
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) =>
            this.InflateAndWireUpControls(inflater, container, Resource.Layout.fragment_tabspage);

        // Fragment that will be used with ViewPager/
        private class FooFragment : ReactiveUI.AndroidX.ReactiveFragment<object>
        {
            // Text to display
            private string Text { get; set; }

            public TextView TextView { get; private set; }

            // Register reactiveui observables.
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                this.WhenActivated(disposable =>
                {
                    // this is called when the tab is in view.

                    Disposable.Create(() =>
                    {
                        // this is called when the tab is not in view.
                    })
                    .DisposeWith(disposable);
                });
            }

            // Create a TextView and set it as the view.
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                TextView = new TextView(container.Context);
                TextView.Text = Text;
                return TextView;
            }

            // Create classes that store the provided text fast.
            public static FooFragment New(string text)
            {
                var foo = new FooFragment();
                foo.Text = text;
                return foo;
            }
        }
    }
}