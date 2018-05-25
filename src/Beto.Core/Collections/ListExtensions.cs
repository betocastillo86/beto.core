//-----------------------------------------------------------------------
// <copyright file="ListExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// List extension methods
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// For sending the index as a parameter
        /// </summary>
        /// <typeparam name="T">the type of the list</typeparam>
        /// <param name="list">the list</param>
        /// <param name="action">the method to execute</param>
        public static void For<T>(this IEnumerable<T> list, Action<T, int> action)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                action(list.ElementAt(i), i);
            }
        }

        /// <summary>
        /// Foreaches the specified function with a new list generated from the foreach.
        /// </summary>
        /// <typeparam name="T">the element tyep</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="function">The function.</param>
        /// <returns>another filtered list</returns>
        public static IEnumerable<TResult> Foreach<T, TResult>(this IEnumerable<T> list, Func<T, TResult> function)
        {
            foreach (var item in list)
            {
                yield return function(item);
            }
        }

        /// <summary>
        /// Foreaches the specified action.
        /// </summary>
        /// <typeparam name="T">the element Type</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="action">The action.</param>
        public static void Foreach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

        /// <summary>
        /// Foreaches the return.
        /// </summary>
        /// <typeparam name="T">the type of the list</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="action">The action.</param>
        /// <returns>the original list</returns>
        public static IEnumerable<T> ForeachReturn<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }

            return list;
        }
    }
}