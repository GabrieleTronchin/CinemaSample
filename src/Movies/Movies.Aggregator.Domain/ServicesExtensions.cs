using Movies.Client;
using Microsoft.Extensions.DependencyInjection;


namespace Movies.Domain
{
    public static partial class ServicesExtensions
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            services.AddMoviesClient();
            return services;
        }
    }
}
