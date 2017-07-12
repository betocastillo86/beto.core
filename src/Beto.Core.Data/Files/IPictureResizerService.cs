//-----------------------------------------------------------------------
// <copyright file="IPictureResizerService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Files
{
    /// <summary>
    /// Interface for resizing Images
    /// </summary>
    public interface IPictureResizerService
    {
        /// <summary>
        /// Resizes the picture.
        /// </summary>
        /// <param name="resizedPath">The resized path.</param>
        /// <param name="fullPath">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void ResizePicture(string resizedPath, string fullPath, int width, int height);
    }
}