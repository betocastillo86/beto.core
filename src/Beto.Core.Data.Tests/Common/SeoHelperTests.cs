//-----------------------------------------------------------------------
// <copyright file="SeoHelperTests.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using Beto.Core.Data.Common;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Seo Helper tests
    /// </summary>
    [TestFixture]
    public class SeoHelperTests
    {
        /// <summary>
        /// The query entities
        /// </summary>
        private IQueryable<ISeoEntity> queryEntities;

        /// <summary>
        /// The seo helper
        /// </summary>
        private SeoHelper seoHelper;

        /// <summary>
        /// Generates the friendly name maximum length exceeded string cut.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="expected">The expected.</param>
        [Test]
        [TestCase("abcdef", "abcdef")]
        [TestCase("abc d ef gh", "abc")]
        [TestCase("abcd ef", "abcd")]
        [TestCase("a _.1b$%&/(-#/AB mas", "a-1bab")]
        [TestCase("0123456789012345678901234567890", "012345678901234567890123")]
        public void GenerateFriendlyName_MaxLengthExceeded_StringCut(string name, string expected)
        {
            var response = this.seoHelper.GenerateFriendlyName(name, maxLength: 4);

            Assert.AreEqual(expected, response);
        }

        /// <summary>
        /// Generates the name of the friendly name not any in query same.
        /// </summary>
        [Test]
        public void GenerateFriendlyName_NotAnyInQuery_SameName()
        {
            var response = this.seoHelper.GenerateFriendlyName("b", this.queryEntities);

            Assert.That(response, Is.EqualTo("b"));
        }

        /// <summary>
        /// Generates the friendly name with any in query friendly with random.
        /// </summary>
        [Test]
        public void GenerateFriendlyName_WithAnyInQuery_FriendlyWithRandom()
        {
            var response = this.seoHelper.GenerateFriendlyName("a", this.queryEntities);

            Assert.That(response, Does.StartWith("a-"));
        }

        /// <summary>
        /// Generates the name of the friendly name without query friendly.
        /// </summary>
        [Test]
        public void GenerateFriendlyName_WithoutQuery_FriendlyName()
        {
            var name = "a _.1b$%&/(-#/AB";

            var response = this.seoHelper.GenerateFriendlyName(name);

            Assert.AreEqual("a-1bab", response);
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.seoHelper = new SeoHelper();

            var seoEntity = new Mock<ISeoEntity>();
            seoEntity.SetupGet(c => c.FriendlyName).Returns("a");

            this.queryEntities = new List<ISeoEntity> { seoEntity.Object }.AsQueryable();
        }
    }
}