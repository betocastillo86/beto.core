//-----------------------------------------------------------------------
// <copyright file="IPictureResizerService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Files
{
    using Beto.Core.Data.Entities;

    /// <summary>
    /// Interface for resizing Images
    /// </summary>
    public interface IPictureResizerService
    {
        /// <summary>
        /// Resizes the picture.
        /// </summary>
        /// <param name="resizedPath">The resized path.</param>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void ResizePicture(string resizedPath, IFileEntity file, int width, int height);
    }
}