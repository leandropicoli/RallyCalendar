using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace RallyCalendar.Core.Repositories.Implementation
{
    public abstract class CacheHttpRepository : BaseHttpRepository
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromDays(1);
        private readonly HttpClient httpClient;

        protected CacheHttpRepository(IMemoryCache memoryCache) : base()
        {
            _cache = memoryCache;
            this.httpClient = Client;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string endpoint, string cacheKey)
        {
            if (_cache.TryGetValue(cacheKey, out TResponse cachedResponse))
            {
                return cachedResponse;
            }

            var response = await Client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var externalResponse = JsonSerializer.Deserialize<TResponse>(responseBody);

            _cache.Set(cacheKey, externalResponse, _cacheExpiration);

            return externalResponse;
        }
    }
}
