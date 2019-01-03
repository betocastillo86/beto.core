//-----------------------------------------------------------------------
// <copyright file="MemoryCacheManagerTests.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Tests.Caching
{
    using Beto.Core.Caching;
    using Microsoft.Extensions.Caching.Memory;
    using NUnit.Framework;

    /// <summary>
    /// Memory Cache Manager Tests
    /// </summary>
    [TestFixture]
    public class MemoryCacheManagerTests
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private MemoryCacheManager cacheManager;

        /// <summary>
        /// Clears the when call remove all keys.
        /// </summary>
        [Test]
        public void Clear_WhenCall_RemoveAllKeys()
        {
            this.cacheManager.Set("key", "value", 1);
            this.cacheManager.Set("key2", "value", 1);

            this.cacheManager.Clear();

            Assert.IsFalse(this.cacheManager.IsSet("key"));
            Assert.IsFalse(this.cacheManager.IsSet("key2"));
        }

        /// <summary>
        /// Gets the existent key returns value.
        /// </summary>
        [Test]
        public void Get_ExistentKey_ReturnsValue()
        {
            this.cacheManager.Set("key", "value", 10);

            var value = this.cacheManager.Get<string>("key");

            Assert.That(value, Is.EqualTo("value"));
        }

        /// <summary>
        /// Gets the non existent key returns default value.
        /// </summary>
        [Test]
        public void Get_NonExistentKey_ReturnsDefaltValue()
        {
            this.cacheManager.Set("key", "value", 10);

            var value = this.cacheManager.Get<string>("key2");

            Assert.IsNull(value);
        }

        /// <summary>
        /// Determines whether [is set when call correct value] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isSet">if set to <c>true</c> [is set].</param>
        [Test]
        [TestCase("key", true)]
        [TestCase("key1", false)]
        public void IsSet_WhenCall_CorrectValue(string key, bool isSet)
        {
            this.cacheManager.Set("key", "value", 1);

            Assert.That(this.cacheManager.IsSet(key), Is.EqualTo(isSet));
        }

        /// <summary>
        /// Removes the by pattern remove key removed key.
        /// </summary>
        [Test]
        public void RemoveByPattern_RemoveKey_RemovedKey()
        {
            this.cacheManager.Set("group1.name", "a", 10);
            this.cacheManager.Set("group2.name", "b", 10);
            this.cacheManager.Set("group2.name1", "c", 10);
            this.cacheManager.Set("group3.name", "d", 10);

            this.cacheManager.RemoveByPattern("group2");

            Assert.IsFalse(this.cacheManager.IsSet("group2.name"));
            Assert.IsFalse(this.cacheManager.IsSet("group2.name1"));
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.cacheManager = new MemoryCacheManager(new MemoryCache(new MemoryCacheOptions()));
        }
    }
}