using Android.Content;
using Android.Widget;

namespace Panoukos41.Helpers
{
    public static partial class ContextExtensions
    {
        /// <summary>
        /// Returns a SimpleCursorAdapter with a MatrixCursor as the cursor.
        /// </summary>
        /// <param name="context">The context on which the cursor will be created.</param>
        /// <param name="layout">The layout to use to display the property.</param>
        /// <param name="to">Id of the view for the displayed property.</param>
        /// <param name="cursorAdapterFlags">Adapter falgs.</param>
        public static SimpleCursorAdapter CreateSimpleCursorAdapter(this Context context, int layout, int to, CursorAdapterFlags cursorAdapterFlags = CursorAdapterFlags.None)
        {
            return new SimpleCursorAdapter(context, layout, null, new string[] { "text" }, new int[] { to }, cursorAdapterFlags);
        }

        /// <summary>
        /// Returns an AndroidX SimpleCursorAdapter that will store one property.
        /// </summary>
        /// <param name="context">The context on which the cursor will be created.</param>
        /// <param name="layout">The layout to use to display the property.</param>
        /// <param name="to">Id of the view for the displayed property.</param>
        /// <param name="cursorAdapterFlags">Adapter falgs found in AndroidX SimpleCursorAdapter as constant values.</param>
        public static AndroidX.CursorAdapter.Widget.SimpleCursorAdapter CreateSimpleCursorAdapterX(this Context context, int layout, int to, int cursorAdapterFlags = 0)
        {
            return new AndroidX.CursorAdapter.Widget.SimpleCursorAdapter(context, layout, null, new string[] { "text" }, new int[] { to }, cursorAdapterFlags);
        }
    }
}