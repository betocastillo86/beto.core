//-----------------------------------------------------------------------
// <copyright file="ILoggerService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Exceptions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface of general logger service. It Has to have an implementation with the project that used it.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Inserts the specified short message.
        /// </summary>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="fullMessage">The full message.</param>
        void Insert(string shortMessage, string fullMessage = "");

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="fullMessage">The full message.</param>
        /// <returns>the task</returns>
        Task InsertAsync(string shortMessage, string fullMessage = "");
    }
}