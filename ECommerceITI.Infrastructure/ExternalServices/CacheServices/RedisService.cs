using ECommerceITI.Domain.Interfaces.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace ECommerceITI.Infrastructure.ExternalServices.CacheServices
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache? _cache;

        public RedisService(IDistributedCache? cache)
        {
            _cache = cache;
        }

        public T? GetData<T>(string Key)
        {
            var data = _cache?.GetString(Key);

            if (data is null)
                return default(T);

            return JsonSerializer.Deserialize<T>(data);
        }

        public void SetData<T>(string Key, T Data)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };

            _cache?.SetString(key: Key, value: JsonSerializer.Serialize(Data), options);
        }
    }
}
