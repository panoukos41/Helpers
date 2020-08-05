using Android.Content;
using Android.Database;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panoukos41.Helpers
{
    /// <summary>
    /// Extension methods to use with <see cref="IEnumerable{T}"/> collections.
    /// </summary>
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Returns a <see cref="MatrixCursor"/>, for the collection.
        /// </summary>
        /// <param name="collection">The colletion that will be used to create the Cursor.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="serialize">A way to serialize the object and retrieve it from the third column => [2] as a string.</param>
        /// <returns>A new matrixcursor.</returns>
        public static MatrixCursor ToMatrixCursor<T>(this IEnumerable<T> collection, Func<T, string> text, Func<T, string> serialize = null)
        {
            MatrixCursor cursor = new MatrixCursor(new string[] { "_id", "text", "item" });

            int id = 0;
            foreach (var item in collection)
            {
                cursor.AddRow(new Java.Lang.Object[] { id.ToString(), text?.Invoke(item), serialize?.Invoke(item) });
                id++;
            }
            return cursor;
        }

        /// <summary>
        /// Turn an array of strings into an <see cref="ArrayAdapter{T}"/> of string.
        /// </summary>
        /// <param name="collection">The collection to use.</param>
        /// <param name="context">The contenxt on which the adapter will be created.</param>
        /// <param name="textViewResourceId">Leave 0 to use the default SimpleSpinnerItem.</param>
        /// <returns>A new array adapter.</returns>
        public static ArrayAdapter<string> ToArrayAdapter(
            this IEnumerable<string> collection,
            Context context,
            int textViewResourceId = 0)
        {
            return textViewResourceId == 0
                ? new ArrayAdapter<string>(context, Android.Resource.Layout.SimpleSpinnerItem, collection.ToList())
                : new ArrayAdapter<string>(context, textViewResourceId, collection.ToList());
        }

        /// <summary>
        /// An extension to turn an array of T into an <see cref="ArrayAdapter{T}"/> of string.
        /// </summary>
        /// <param name="collection">The collection to use.</param>
        /// <param name="context">The contenxt on which the adapter will be created.</param>
        /// <param name="selector">A fucn to choose the string title of the item.</param>
        /// <param name="textViewResourceId">Leave 0 to use the default SimpleSpinnerItem.</param>
        /// <returns>A new array adapter.</returns>
        public static ArrayAdapter<string> ToArrayAdapter<T>(
            this IEnumerable<T> collection,
            Context context,
            Func<T, string> selector,
            int textViewResourceId = 0)
        {
            return ToArrayAdapter(collection.Select(selector), context, textViewResourceId);
        }
    }
}