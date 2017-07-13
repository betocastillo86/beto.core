//-----------------------------------------------------------------------
// <copyright file="ICorePictureResizerService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Files
{
    /// <summary>
    /// Interface for resizing Images
    /// </summary>
    public interface ICorePictureResizerService
    {
        /// <summary>
        /// Resizes the picture.
        /// </summary>
        /// <param name="resizedPath">The resized path.</param>
        /// <param name="originalPath">The original path.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void ResizePicture(string resizedPath, string originalPath, int width, int height);

        /// <summary>
        /// Resizes the picture.
        /// </summary>
        /// <param name="contentFile">The content file.</param>
        /// <param name="resizedPath">The resized path.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void ResizePicture(byte[] contentFile, string resizedPath, int width, int height);
    }
}