using AutoMapper;
using VacationRental.Calendar.Domain;
using VacationRental.ServiceBus.IntegrationEvents;

namespace VacationRental.Calendar.Application.Mapper;
public class AppCalendarMapperAccessor : IAppCalendarMapperAccessor
{
    public AppCalendarMapperAccessor()
    {

        AppCalendarMapper = new MapperConfiguration(m =>
        {
            m.CreateMap<BookingConfirmedIntegrationEvent, BookingListMaterializedView>();

        }).CreateMapper();

    }

    public IMapper AppCalendarMapper { get; }

}
