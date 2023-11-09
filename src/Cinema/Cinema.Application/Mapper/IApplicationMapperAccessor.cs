using AutoMapper;

namespace Cinema.Application.Mapper;
public interface IApplicationMapperAccessor
{
    IMapper AppCalendarMapper { get; }
}
