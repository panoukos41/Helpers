using AndroidX.Fragment.App;
using Google.Android.Material.Tabs;
using Panoukos41.Helpers.AndroidX.Adapters;
using System;
using System.Collections.Generic;

namespace Panoukos41.Helpers.AndroidX.Tabs
{
    /// <summary>
    /// Optional interface to implement when using <see cref="StateAdapter"/>
    /// </summary>
    public interface IStateTabs
    {
        /// <summary>
        /// The tab's that will be used by the <see cref="StateAdapter"/>
        /// </summary>
        List<(Action<TabLayout.Tab> tab, Fragment)> Tabs { get; }
    }
}