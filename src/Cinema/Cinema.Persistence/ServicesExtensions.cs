using Cinema.Domain.AuditoriumDefinition.Repository;
using Cinema.Domain.Primitives;
using Cinema.Domain.Showtime.Repository;
using Cinema.Domain.Ticket.Repository;
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

            services.AddDbContext<CinemaDbContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });


            return services;

        }
    }
}
