using ECommerceITI.Application.DTOs.Settings;
using ECommerceITI.Domain.Interfaces.Caching;
using ECommerceITI.Infrastructure.ExternalServices.CacheServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceITI.Infrastructure.Configurations
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RedisSettings>(config.GetSection("RedisSettings"));

            services.AddStackExchangeRedisCache(opt =>
            {
                var redisSettings = config.GetSection("RedisSettings").Get<RedisSettings>();
                opt.Configuration = $"{redisSettings.Host}:{redisSettings.Port}";
                opt.InstanceName = redisSettings.InstanceName;
            });


            services.AddScoped<IRedisService, RedisService>();
            return services;
        }
    }
}
