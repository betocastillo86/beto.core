//-----------------------------------------------------------------------
// <copyright file="PagingListTests.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Paging
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    /// <summary>
    /// Paging List Tests
    /// </summary>
    [TestFixture]
    public class PagingListTests
    {
        /// <summary>
        /// The list
        /// </summary>
        private IQueryable<int> list;

        /// <summary>
        /// Constructors the when call same range of values.
        /// </summary>
        [Test]
        public void Constructor_WhenCall_SameRangeOfValues()
        {
            var pageSize = 2;

            var paged = new PagedList<int>(this.list.AsQueryable(), 1, pageSize);

            Assert.AreEqual(paged.Count, pageSize);
            Assert.AreEqual(this.list.ToArray()[2], paged[0]);
            Assert.AreEqual(this.list.ToArray()[3], paged[1]);
        }

        /// <summary>
        /// Constructors the when call valid values.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="expectedPages">The expected pages.</param>
        /// <param name="expectedNextPage">if set to <c>true</c> [expected next page].</param>
        [Test]
        [TestCase(0, 2, 3, true)]
        [TestCase(0, 5, 1, false)]
        [TestCase(3, 2, 3, false)]
        [TestCase(1, 2, 3, true)]
        public void Constructor_WhenCall_ValidValues(int page, int pageSize, int expectedPages, bool expectedNextPage)
        {
            var paged = new PagedList<int>(this.list.AsQueryable(), page, pageSize);

            Assert.AreEqual(expectedPages, paged.TotalPages);
            Assert.AreEqual(this.list.Count(), paged.TotalCount);
            Assert.AreEqual(expectedNextPage, paged.HasNextPage);
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.list = new List<int> { 1, 2, 3, 4, 5 }.AsQueryable();
        }
    }
}