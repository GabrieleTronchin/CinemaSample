using Cinema.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Client;
using ServiceCache;

namespace Movies.Aggregator.Domain;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddDomainLayer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMoviesClient(configuration);
        services.AddCinemaClient(configuration);
        services.AddServiceCache(configuration);
        services.AddTransient<IShowtimeService, ShowtimeService>();
        return services;
    }
}
