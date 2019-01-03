//-----------------------------------------------------------------------
// <copyright file="FilesHelperTests.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Files
{
    using System;
    using Beto.Core.Data.Files;
    using Beto.Core.Data.Tests.Fakes;
    using Microsoft.AspNetCore.Hosting;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Files Helper Tests
    /// </summary>
    [TestFixture]
    public class FilesHelperTests
    {
        /// <summary>
        /// The file
        /// </summary>
        private FileEntityFake file;

        /// <summary>
        /// The files helper
        /// </summary>
        private FilesHelper filesHelper;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private Mock<IHostingEnvironment> hostingEnvironment;

        /// <summary>
        /// The picture resizer service
        /// </summary>
        private Mock<ICorePictureResizerService> pictureResizerService;

        /// <summary>
        /// Gets the name of the folder name when call valid.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filesByFolder">The files by folder.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Test]
        [TestCase(1, 5, "000001")]
        [TestCase(6, 5, "000002")]
        public void GetFolderName_WhenCall_ValidName(int id, int filesByFolder, string expectedValue)
        {
            this.file.Id = id;

            var response = this.filesHelper.GetFolderName(this.file, filesByFolder);
            Assert.AreEqual(expectedValue, response);
        }

        /// <summary>
        /// Gets the full path force resize call resize method.
        /// </summary>
        [Test]
        public void GetFullPath_ForceResize_CallResizeMethod()
        {
            var width = 1;
            var height = 2;

            var response = this.filesHelper.GetFullPath(this.file, forceResize: true, width: width, height: height);

            this.pictureResizerService.Verify(c => c.ResizePicture(It.IsAny<string>(), It.IsAny<string>(), width, height, ResizeMode.Crop));
        }

        /// <summary>
        /// Gets the full path simple call full route.
        /// </summary>
        [Test]
        public void GetFullPath_SimpleCall_FullRoute()
        {
            var response = this.filesHelper.GetFullPath(this.file);

            Assert.AreEqual("/img/content/000001/1.jpg", response);
        }

        /// <summary>
        /// Gets the full path with content URL function full route.
        /// </summary>
        [Test]
        public void GetFullPath_WithContentUrlFuncion_FullRoute()
        {
            Func<string, string> urlFunction = (route) => { return $"<{route}>"; };

            var response = this.filesHelper.GetFullPath(this.file, contentUrlFunction: urlFunction);

            Assert.AreEqual("</img/content/000001/1.jpg>", response);
        }

        /// <summary>
        /// Gets the full path with size full route.
        /// </summary>
        [Test]
        public void GetFullPath_WithSize_FullRoute()
        {
            var width = 1;
            var height = 2;

            var response = this.filesHelper.GetFullPath(this.file, width: width, height: height);

            Assert.AreEqual($"/img/content/000001/{file.Id}_filename_{width}x{height}.jpg", response);
        }

        /// <summary>
        /// Gets the physical path when call full path.
        /// </summary>
        [Test]
        public void GetPhysicalPath_WhenCall_FullPath()
        {
            var response = this.filesHelper.GetPhysicalPath(this.file);

            Assert.AreEqual("c://img/content/000001/1.jpg", response);
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.pictureResizerService = new Mock<ICorePictureResizerService>();

            this.hostingEnvironment = new Mock<IHostingEnvironment>();
            this.hostingEnvironment.SetupGet(c => c.WebRootPath).Returns("c:/");

            this.file = new FileEntityFake { Id = 1, Name = "name", FileName = "filename.jpg", MimeType = "image/jpg" };

            this.filesHelper = new FilesHelper(this.pictureResizerService.Object, this.hostingEnvironment.Object);
        }
    }
}