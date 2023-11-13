using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceCache;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddServiceCache(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddOptions<CacheOptions>().Bind(configuration.GetSection("Cache")).ValidateDataAnnotations();

        if (!string.IsNullOrEmpty(configuration.GetSection("RedisCache:Configuration").Value))
        {
            services.AddStackExchangeRedisCache(options =>
            {
                configuration.Bind("RedisCache", options);
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }

        services.AddTransient<ICacheService, CacheService>();
        return services;

    }

}
