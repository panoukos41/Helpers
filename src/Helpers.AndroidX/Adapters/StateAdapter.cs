using AndroidX.Fragment.App;
using AndroidX.ViewPager2.Adapter;
using Google.Android.Material.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panoukos41.Helpers.AndroidX.Adapters
{
    // todo
    /// <summary>
    /// A simple <see cref="FragmentStateAdapter"/> implementation.
    /// </summary>
    public class StateAdapter : FragmentStateAdapter
    {
        /// <summary>
        /// Item count.
        /// </summary>
        public override int ItemCount => Fragments == null ? 0 : Fragments.Count;

        /// <summary>
        /// Same as ItemCount
        /// </summary>
        public int Count => ItemCount;

        private List<Fragment> Fragments { get; }

        /// <summary>
        /// Initialize a new instance of <see cref="StateAdapter"/>
        /// That hosts a fixed number of fragments.
        /// </summary>
        /// <param name="fragment">The <see cref="Fragment"/> that hosts the adapter.</param>
        /// <param name="fragments">The fragments that will be hosted.</param>
        public StateAdapter(Fragment fragment, List<Fragment> fragments) : base(fragment)
        {
            Fragments = fragments;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="StateAdapter"/>
        /// That hosts a fixed number of fragments.
        /// </summary>
        /// <param name="fragment">The <see cref="Fragment"/> that hosts the adapter.</param>
        /// <param name="tabs">The fragments that will be hosted.</param>
        public StateAdapter(Fragment fragment, List<(Action<TabLayout.Tab> tab, Fragment fragment)> tabs) : base(fragment)
        {
            Fragments = tabs
                .Select(x => x.fragment)
                .ToList();
        }

        /// <summary>
        /// </summary>
        public override Fragment CreateFragment(int position)
            => Fragments[position];
    }
}