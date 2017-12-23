//-----------------------------------------------------------------------
// <copyright file="IFilesHelper.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Files
{
    using System;
    using Beto.Core.Data.Entities;

    /// <summary>
    /// Interface of file helpers
    /// </summary>
    public interface IFilesHelper
    {
        /// <summary>
        /// Gets the name of the content type by file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>the content type</returns>
        string GetContentTypeByFileName(string fileName);

        /// <summary>
        /// Gets the name of the folder.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filesByFolder">The files by folder.</param>
        /// <returns>the folder where the file is</returns>
        string GetFolderName(IFileEntity file, int filesByFolder = 50);

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="forceResize">forces the resize</param>
        /// <returns>the path</returns>
        string GetFullPath(IFileEntity file, Func<string, string> contentUrlFunction = null, int width = 0, int height = 0, bool forceResize = false);

        /// <summary>
        /// Gets the physical path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the physical path</returns>
        string GetPhysicalPath(IFileEntity file, int width = 0, int height = 0);

        /// <summary>
        /// Determines whether [is image extension] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <c>true</c> if [is image extension] [the specified file name]; otherwise, <c>false</c>.
        /// </returns>
        bool IsImageExtension(string fileName);

        /// <summary>
        /// Saves the file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="bytes">The bytes.</param>
        /// <param name="resizeWidth">If the file is an image is the resize width</param>
        /// <param name="resizeHeight">If the file is an image is the resize height</param>
        void SaveFile(IFileEntity file, byte[] bytes, int resizeWidth = 500, int resizeHeight = 500);
    }
}