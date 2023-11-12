using Cinema.Domain.AuditoriumDefinition.Repository;
using Cinema.Domain.Showtime.Repository;
using Cinema.Domain.Ticket.Repository;
using Cinema.Persistence.Interceptors;
using Cinema.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cinema.Persistence
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddTransient<ITicketsRepository, TicketsRepository>();
            services.AddTransient<IAuditoriumRepository, AuditoriumsRepository>();

            services.AddSingleton<TicketDomainEventInterceptor>();

            var tcInterceptor = services.BuildServiceProvider().GetRequiredService<TicketDomainEventInterceptor>();

            services.AddDbContext<CinemaDbContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .AddInterceptors(tcInterceptor)
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            return services;

        }
    }
}
