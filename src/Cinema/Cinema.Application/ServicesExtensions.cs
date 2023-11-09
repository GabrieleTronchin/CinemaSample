using Cinema.Persistence;
using Microsoft.Extensions.DependencyInjection;
using VacationRental.Calendar.Application.Mapper;

namespace Cinema.Application
{

    public static class ServicesExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServicesExtensions).Assembly));
            services.AddSingleton<IApplicationMapperAccessor, ApplicationMapperAccessor>();
            services.AddPersistence();
            return services;
        }
    }

}
