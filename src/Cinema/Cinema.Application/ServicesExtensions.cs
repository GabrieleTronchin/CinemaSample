using Cinema.Domain;
using Cinema.Persistence;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Cinema.Application;


public static class ServicesExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServicesExtensions).Assembly));
        services.AddDomainNotification();
        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });


        services.AddQuartz(cfg =>
        {
            var jobKey = new JobKey(nameof(OutboxMessageProcessorJob));

            cfg.AddJob<OutboxMessageProcessorJob>(jobKey)
               .AddTrigger(t => t.ForJob(jobKey)
                                .WithSimpleSchedule(s => s.WithIntervalInSeconds(10).RepeatForever()));
        });

        services.AddQuartzHostedService();

        services.AddPersistence();
        return services;
    }
}
