//-----------------------------------------------------------------------
// <copyright file="FileEntityFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Fakes
{
    using Beto.Core.Data.Entities;

    /// <summary>
    /// File Entity Fake
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Entities.IFileEntity" />
    public class FileEntityFake : IFileEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the MIME.
        /// </summary>
        /// <value>
        /// The type of the MIME.
        /// </value>
        public string MimeType { get; set; }
    }
}