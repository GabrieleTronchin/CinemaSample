using Movies.Client;
using Microsoft.Extensions.DependencyInjection;
using Cinema.Client;

namespace Movies.Aggregator.Domain
{
    public static partial class ServicesExtensions
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            services.AddMoviesClient();
            services.AddCinemaClient();
            services.AddTransient<IShowtimeService, ShowtimeService>();
            return services;
        }
    }
}
