namespace Beto.Core.Caching
{
    using System;
    using System.Text.Json;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache distributedCache;

        private readonly IConnectionMultiplexer connectionMultiplexer;

        public RedisCacheManager(
            IDistributedCache distributedCache,
            IConnectionMultiplexer connectionMultiplexer)
        {
            this.distributedCache = distributedCache;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public void Clear()
        {
            foreach (var endpoint in this.connectionMultiplexer.GetEndPoints())
            {
                //TODO: Improve this
                var server = this.connectionMultiplexer.GetServer(endpoint);
                foreach (var key in server.Keys())
                {
                    this.distributedCache.Remove(key);
                }
            }
        }

        public T Get<T>(string key)
        {
            var value = this.distributedCache.GetString(key);
            return !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<T>(value) : default(T);
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
            foreach (var endpoint in this.connectionMultiplexer.GetEndPoints())
            {
                //TODO: Improve this
                var server = this.connectionMultiplexer.GetServer(endpoint);
                foreach (var key in server.Keys(pattern: pattern + "*"))
                {
                    this.distributedCache.Remove(key);
                }
            }
        }

        public void Set(string key, object data, int cacheTime)
        {
            this.distributedCache.SetString(key, JsonSerializer.Serialize(data), new DistributedCacheEntryOptions { AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(cacheTime)) });
        }
    }
}