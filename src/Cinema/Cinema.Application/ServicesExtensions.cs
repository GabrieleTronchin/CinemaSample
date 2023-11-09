using Cinema.Persistence;
using Microsoft.Extensions.DependencyInjection;


namespace Cinema.Application
{

    public static class ServicesExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddPersistence();
            return services;
        }
    }

}
