//-----------------------------------------------------------------------
// <copyright file="IMessageExceptionFinder.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Exceptions
{
    /// <summary>
    /// Interface for getting the exception message depending of exception
    /// </summary>
    public interface IMessageExceptionFinder
    {
        /// <summary>
        /// Gets the error message depending of the exception code
        /// </summary>
        /// <typeparam name="T">the type of errors</typeparam>
        /// <param name="exceptionCode">The exception code.</param>
        /// <returns>The text of exception</returns>
        string GetErrorMessage<T>(T exceptionCode);
    }
}