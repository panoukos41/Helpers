using AndroidX.Fragment.App;
using Panoukos41.Helpers.AndroidX.Adapters;
using System.Collections.Generic;

namespace Panoukos41.Helpers.AndroidX.Tabs
{
    /// <summary>
    /// Optional interface to implement when using <see cref="PagerAdapter"/>
    /// </summary>
    public interface IPagerTabs
    {
        /// <summary>
        /// The tab's that will be used by the <see cref="PagerAdapter"/>
        /// </summary>
        List<(string, Fragment)> Tabs { get; }
    }
}