namespace Beto.Core.Caching
{
    using System;
    using Beto.Core.Helpers;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Configuration;
    using StackExchange.Redis;

    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache distributedCache;

        private readonly ConnectionMultiplexer connectionMultiplexer;

        private readonly IConfiguration configuration;

        public RedisCacheManager(
            IDistributedCache distributedCache,
            ConnectionMultiplexer connectionMultiplexer,
            IConfiguration configuration)
        {
            this.distributedCache = distributedCache;
            this.connectionMultiplexer = connectionMultiplexer;
            this.configuration = configuration;
        }

        public void Clear()
        {
            var server = this.connectionMultiplexer.GetServer(this.configuration["RedisServer"]);
            foreach (var key in server.Keys())
            {
                this.distributedCache.Remove(key);
            }
        }

        public T Get<T>(string key)
        {
            object value = this.distributedCache.Get(key);
            return value == null ? (T)value : default(T);
        }

        public bool IsSet(string key)
        {
            return this.distributedCache.Get(key) != null;
        }

        public void Remove(string key)
        {
            this.distributedCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var server = this.connectionMultiplexer.GetServer(this.configuration["RedisServer"]);
            foreach (var key in server.Keys(pattern: pattern))
            {
                this.distributedCache.Remove(key);
            }
        }

        public void Set(string key, object data, int cacheTime)
        {
            this.distributedCache.Set(key, ReflectionHelper.ObjectToByteArray(data), new DistributedCacheEntryOptions { AbsoluteExpiration = new DateTimeOffset(new DateTime(TimeSpan.FromMinutes(cacheTime).Ticks)) });
        }
    }
}