using AutoMapper;
using Cinema.Application.Events.Models;
using VacationRental.Calendar.Domain;

namespace Cinema.Application.Mapper;
public class ApplicationMapperAccessor : IApplicationMapperAccessor
{
    public ApplicationMapperAccessor()
    {

        AppCalendarMapper = new MapperConfiguration(m =>
        {
            m.CreateMap<BookingConfirmedIntegrationEvent, BookingListMaterializedView>();

        }).CreateMapper();

    }

    public IMapper AppCalendarMapper { get; }

}
