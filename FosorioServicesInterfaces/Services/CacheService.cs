using FosorioServicesInterfaces.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FosorioServicesInterfaces.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly MemoryCache _cache;

        public MemoryCacheService()
            => _cache = new MemoryCache(new MemoryCacheOptions());

        public async Task<T> GetAsync<T>(string key)
            => await Task.FromResult(_cache.Get<T>(key));

        public async Task SetAsync<T>(string key, T data, TimeSpan? expiry = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.High);
            if (expiry.HasValue)
                cacheEntryOptions.SetAbsoluteExpiration(expiry.Value);

            _cache.Set(key, data, cacheEntryOptions);

            await Task.CompletedTask;
        }
    }
}