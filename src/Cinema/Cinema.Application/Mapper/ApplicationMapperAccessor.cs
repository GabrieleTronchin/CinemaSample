using AutoMapper;

namespace Cinema.Application.Mapper;
public class ApplicationMapperAccessor : IApplicationMapperAccessor
{
    public ApplicationMapperAccessor()
    {

        AppCalendarMapper = new MapperConfiguration(m =>
        {

        }).CreateMapper();

    }

    public IMapper AppCalendarMapper { get; }

}
