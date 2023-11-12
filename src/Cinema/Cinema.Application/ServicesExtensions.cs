using Cinema.Application.Auditorium.Queries;
using Cinema.Persistence;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Application;


public static class ServicesExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServicesExtensions).Assembly));
        services.AddTransient<IAuditoriumQueries, AuditoriumQueries>();
        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddPersistence();
        return services;
    }
}
