using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Panoukos41.Helpers
{
    /// <summary>
    /// Extension methods to be used with <see cref="ObservableCollection{T}"/> collections.
    /// </summary>
    public static partial class ObservableCollectionExtensions
    {
        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key or a specified comparer,
        /// it performs a remove and insert to move items around.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="collection">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An System.Collections.Generic.IComparer`1 to compare keys.</param>
        public static void OrderByForView<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector, IComparer<TKey> comparer = null)
        {
            var sorted = comparer == null
                ? collection.OrderBy(keySelector)
                : collection.OrderBy(keySelector, comparer);
            int count = sorted.Count();

            for (int i = 0; i < count; ++i)
            {
                TSource item = sorted.ElementAt(i);
                int actualItemIndex = collection.IndexOf(item);

                if (actualItemIndex != i)
                {
                    collection.RemoveAt(actualItemIndex);
                    collection.Insert(i, item);
                }
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key or a specified comparer,
        /// it performs a remove and insert to move items around.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="collection">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An System.Collections.Generic.IComparer`1 to compare keys.</param>
        public static void OrderByDescendingForView<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector, IComparer<TKey> comparer = null)
        {
            var sorted = comparer == null
                ? collection.OrderByDescending(keySelector)
                : collection.OrderByDescending(keySelector, comparer);
            int count = sorted.Count();

            for (int i = 0; i < count; ++i)
            {
                TSource item = sorted.ElementAt(i);
                int actualItemIndex = collection.IndexOf(item);

                if (actualItemIndex != i)
                {
                    collection.RemoveAt(actualItemIndex);
                    collection.Insert(i, item);
                }
            }
        }
    }
}