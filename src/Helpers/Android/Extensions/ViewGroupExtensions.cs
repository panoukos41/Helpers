using Android.Views;

namespace Panoukos41.Helpers
{
    /// <summary>
    /// Extension methods to use with <see cref="ViewGroup"/>.
    /// </summary>
    public static partial class ViewGroupExtensions
    {
        /// <summary>
        /// Inflate a view for this viewGroup, using LayoutInflater.From().
        /// </summary>
        /// <param name="viewGroup">The viewGroup that will be used.</param>
        /// <param name="layout">The layout id.</param>
        /// <param name="attachToRoot">Whether the inflated hierarchy should be attached to the root parameter? If false,
        /// root is only used to create the correct subclass of LayoutParams for the root view in the XML.</param>
        /// <returns>A new View inflated from the provided resource.</returns>
        public static View Inflate(this ViewGroup viewGroup, int layout, bool attachToRoot = false) =>
            LayoutInflater.From(viewGroup.Context).Inflate(layout, viewGroup, attachToRoot);
    }
}