//-----------------------------------------------------------------------
// <copyright file="CoreException.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Exceptions
{
    using System;

    /// <summary>
    /// Core exceptions for projects
    /// </summary>
    /// <typeparam name="T">The type of errors</typeparam>
    /// <seealso cref="System.Exception" />
    public class CoreException<T> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException{T}"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public CoreException(string error) : base(error)
        {
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public T Code { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public string Target { get; set; }
    }
}