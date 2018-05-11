//-----------------------------------------------------------------------
// <copyright file="BaseFilterModelValidatorTests.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Tests.Api
{
    using Beto.Core.Web.Tests.Fakes;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// Base Filter Model Validator tests
    /// </summary>
    [TestFixture]
    public class BaseFilterModelValidatorTests
    {
        /// <summary>
        /// The model
        /// </summary>
        private BaseFilterModelFake model;

        /// <summary>
        /// The validator
        /// </summary>
        private BaseFilterModalValidatorFake validator;

        /// <summary>
        /// Called when [validate correct order by no error].
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        [Test]
        [TestCase("a")]
        [TestCase("")]
        public void OnValidate_CorrectOrderBy_NoError(string orderBy)
        {
            this.model.OrderBy = orderBy;

            this.validator.ShouldNotHaveValidationErrorFor(c => c.OrderBy, this.model);
        }

        /// <summary>
        /// Called when [validate wrong page page error].
        /// </summary>
        [Test]
        public void OnValidate_WrongPage_PageError()
        {
            this.model.Page = -1;

            this.validator.ShouldHaveValidationErrorFor(c => c.Page, this.model);
        }

        /// <summary>
        /// Called when [validate wrong page size page size error].
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        [Test]
        [TestCase(0)]
        [TestCase(6)]
        public void OnValidate_WrongPageSize_PageSizeError(int pageSize)
        {
            this.model.PageSize = pageSize;

            this.validator.ShouldHaveValidationErrorFor(c => c.PageSize, this.model);
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.model = new BaseFilterModelFake(5, new string[] { "a", "b", "c" });
            this.validator = new BaseFilterModalValidatorFake();
        }
    }
}