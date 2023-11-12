using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Domain;


public static class ServicesExtensions
{
    public static IServiceCollection AddDomainNotification(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServicesExtensions).Assembly));
        return services;
    }
}
