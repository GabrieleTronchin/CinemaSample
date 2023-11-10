using Cinema.Application.Mapper;
using Cinema.Application.Queries.Auditorium;
using Cinema.Application.Queries.Showtime;
using Cinema.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Application
{

    public static class ServicesExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServicesExtensions).Assembly));
            services.AddSingleton<IApplicationMapperAccessor, ApplicationMapperAccessor>();

            services.AddTransient<IShowtimeQueries, ShowtimeQueries>();
            services.AddTransient<IAuditoriumQueries, AuditoriumQueries>();

            services.AddPersistence();
            return services;
        }
    }

}
