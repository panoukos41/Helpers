using System;

namespace Panoukos41.Helpers
{
    /// <summary>
    /// Extension methods to be used with all objects.
    /// </summary>
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// Cast an object to Type T.
        /// This method will use an explicit cast.
        /// <para><see href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions#explicit-conversions">Read more here.</see></para>
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <param name="obj">The object that will be casted.</param>
        /// <returns>Obj casted to T</returns>
        /// <exception cref="InvalidCastException">When the cast can't be performed.</exception>
        public static T Cast<T>(this object obj)
            => (T)obj;

        /// <summary>
        /// Cast an object to Type T.
        /// This method will use the "as" operator to cast the object.
        /// <para><see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#as-operator">Read more here</see></para>
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <param name="obj">The object that will be casted.</param>
        /// <returns>Obj casted to T or null if cast fails</returns>
        public static T CastAs<T>(this object obj) where T : class
            => obj as T;
    }
}