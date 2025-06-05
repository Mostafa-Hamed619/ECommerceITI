using ECommerceITI.Application.DTOs.Settings;
using ECommerceITI.Domain.Interfaces.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace ECommerceITI.Infrastructure.ExternalServices.CacheServices
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache? _cache;
        private readonly RedisSettings _settings;

        public RedisService(IDistributedCache? cache, IOptions<RedisSettings> options)
        {
            _cache = cache;
            _settings = options.Value;
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
                AbsoluteExpirationRelativeToNow = _settings.TimoutHS
            };

            _cache?.SetString(key: Key, value: JsonSerializer.Serialize(Data), options);
        }
    }
}
