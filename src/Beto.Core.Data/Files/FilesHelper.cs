//-----------------------------------------------------------------------
// <copyright file="FilesHelper.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Files
{
    using System;
    using Beto.Core.Data.Entities;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Files Helper service
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Files.IFilesHelper" />
    public class FilesHelper : IFilesHelper
    {
        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The picture resizer service
        /// </summary>
        private readonly ICorePictureResizerService pictureResizerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesHelper"/> class.
        /// </summary>
        /// <param name="pictureResizerService">The picture resizer service.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public FilesHelper(
            ICorePictureResizerService pictureResizerService,
            IHostingEnvironment hostingEnvironment)
        {
            this.pictureResizerService = pictureResizerService;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Gets the name of the content type by file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// the content type
        /// </returns>
        public string GetContentTypeByFileName(string fileName)
        {
            var contentType = string.Empty;
            var fileExtension = System.IO.Path.GetExtension(fileName);

            switch (fileExtension)
            {
                case ".bmp":
                    contentType = "image/bmp";
                    break;

                case ".gif":
                    contentType = "image/gif";
                    break;

                case ".jpeg":
                case ".jpg":
                case ".jpe":
                case ".jfif":
                case ".pjpeg":
                case ".pjp":
                    contentType = "image/jpeg";
                    break;

                case ".png":
                    contentType = "image/png";
                    break;

                case ".tiff":
                case ".tif":
                    contentType = "image/tiff";
                    break;

                default:
                    break;
            }

            return contentType;
        }

        /// <summary>
        /// Gets the name of the folder.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filesByFolder">The files by folder.</param>
        /// <returns>
        /// the folder where the file is
        /// </returns>
        public string GetFolderName(IFileEntity file, int filesByFolder = 50)
        {
            var folder = Math.Ceiling((decimal)file.Id / filesByFolder);
            return folder.ToString("000000");
        }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>
        /// the path
        /// </returns>
        public string GetFullPath(IFileEntity file, Func<string, string> contentUrlFunction = null, int width = 0, int height = 0)
        {
            var fileName = $"/img/content/{this.GetFolderName(file)}/{this.GetFileNameWithSize(file, width, height)}";

            if (contentUrlFunction != null)
            {
                return contentUrlFunction(fileName);
            }
            else
            {
                return fileName;
            }
        }

        /// <summary>
        /// Gets the physical path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>
        /// the physical path
        /// </returns>
        public string GetPhysicalPath(IFileEntity file, int width = 0, int height = 0)
        {
            var relativePath = this.GetFullPath(file, null, width, height);
            return string.Concat(this.hostingEnvironment.WebRootPath, relativePath);
        }

        /// <summary>
        /// Determines whether [is image extension] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// <c>true</c> if [is image extension] [the specified file name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsImageExtension(string fileName)
        {
            var extension = System.IO.Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(extension))
            {
                extension = string.Empty;
            }

            extension = extension.ToLower();
            bool result = false;
            switch (extension)
            {
                case ".gif":
                case ".jpeg":
                case ".jpg":
                case ".png":
                    result = true;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Saves the file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="bytes">The bytes.</param>
        /// <param name="resizeWidth">If the file is an image is the resize width</param>
        /// <param name="resizeHeight">If the file is an image is the resize height</param>
        /// <exception cref="System.ArgumentException">
        /// The file does not contain a valid id
        /// or
        /// The bytes does not have content
        /// </exception>
        public void SaveFile(IFileEntity file, byte[] bytes, int resizeWidth = 500, int resizeHeight = 500)
        {
            if (file.Id <= 0)
            {
                throw new ArgumentException("The file does not contain a valid id");
            }
            else if (bytes.Length == 0)
            {
                throw new ArgumentException("The bytes does not have content");
            }

            var fullPath = this.GetPhysicalPath(file);

            var directory = System.IO.Path.GetDirectoryName(fullPath);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            if (this.IsImageExtension(fullPath))
            {
                // Resizes the image with the same name
                this.pictureResizerService.ResizePicture(bytes, fullPath, resizeWidth, resizeHeight, ResizeMode.Pad);
            }
            else
            {
                System.IO.File.WriteAllBytes(fullPath, bytes);
            }
        }

        /// <summary>
        /// Gets the size of the file name with.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the file name</returns>
        private string GetFileNameWithSize(IFileEntity file, int width = 0, int height = 0)
        {
            var nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
            var extension = System.IO.Path.GetExtension(file.FileName);

            if (width != 0 && height != 0)
            {
                return $"{file.Id}_{nameWithoutExtension}_{width}x{height}{extension}";
            }
            else
            {
                return $"{file.Id}{extension}";
            }
        }
    }
}