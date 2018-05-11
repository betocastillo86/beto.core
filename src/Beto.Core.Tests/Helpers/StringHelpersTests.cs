//-----------------------------------------------------------------------
// <copyright file="StringHelpersTests.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Tests.Helpers
{
    using Beto.Core.Helpers;
    using NUnit.Framework;

    /// <summary>
    /// String Helpers tests
    /// </summary>
    [TestFixture]
    public class StringHelpersTests
    {
        /// <summary>
        /// To the MD5 when call correct value.
        /// </summary>
        [Test]
        public void ToMd5_WhenCall_CorrectValue()
        {
            var response = StringHelpers.ToMd5("123456");
            Assert.AreEqual("e10adc3949ba59abbe56e057f20f883e", response);
        }

        /// <summary>
        /// To the sha1 when call correct value.
        /// </summary>
        [Test]
        public void ToSha1_WhenCall_CorrectValue()
        {
            var response = StringHelpers.ToSha1("123456");
            Assert.AreEqual("7c4a8d09ca3762af61e59520943dc26494f8941b", response);
        }

        /// <summary>
        /// To the sha1 with salt correct value.
        /// </summary>
        [Test]
        public void ToSha1_WithSalt_CorrectValue()
        {
            var response = StringHelpers.ToSha1("123456", "123");
            Assert.AreEqual("619651c6cb5dc394373a5bb75fea9d4314f3564b", response);
        }

        /// <summary>
        /// To the XXS filtered string no XXS characters same string.
        /// </summary>
        [Test]
        public void ToXXSFilteredString_NoXXSCharacters_SameString()
        {
            var html = "prueba de .html";
            var response = StringHelpers.ToXXSFilteredString(html);
            Assert.AreEqual(html, response);
        }

        /// <summary>
        /// To the XXS filtered string with XXS characters clean string.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="validHtml">The valid HTML.</param>
        [Test]
        [TestCase("<script>prueba de usuario</script>", "scriptprueba de usuarioscript")]
        [TestCase("<script>function() { prueba de usuario }</script>", "scriptfunction { prueba de usuario }script")]
        [TestCase("<a src='http://prueba.com'>prueba de usuario</a>", "a src='httpprueba.com'prueba de usuarioa")]
        public void ToXXSFilteredString_WithXXSCharacters_CleanString(string html, string validHtml)
        {
            var response = StringHelpers.ToXXSFilteredString(html);
            Assert.AreEqual(validHtml, response);
        }
    }
}