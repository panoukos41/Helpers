using Android.Views;
using AndroidX.Fragment.App;
using ReactiveUI.AndroidX;
using static ReactiveUI.ControlFetcherMixin;

namespace Panoukos41.Helpers
{
    public static class FragmentExtensions
    {
        public static View InflateAndWireUpControls(
            this Fragment fragment,
            LayoutInflater inflater,
            ViewGroup viewGroup,
            int layout,
            ResolveStrategy resolveMembers = ResolveStrategy.Implicit,
            bool attachToRoot = false)
        {
            var view = inflater.Inflate(layout, viewGroup, attachToRoot);
            ControlFetcherMixin.WireUpControls(fragment, view, resolveMembers);
            return view;
        }
    }
}