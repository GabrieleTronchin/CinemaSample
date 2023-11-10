using AutoMapper;
using Cinema.Application.Queries.Auditorium.Models;
using Cinema.Domain.AuditoriumDefinition;
using Auditorium = Cinema.Application.Queries.Auditorium.Models;
using Showtime = Cinema.Application.Queries.Showtime.Models;

namespace Cinema.Application.Mapper;
public class ApplicationMapperAccessor : IApplicationMapperAccessor
{
    public ApplicationMapperAccessor()
    {

        AppMapper = new MapperConfiguration(m =>
        {


        }).CreateMapper();

    }

    public IMapper AppMapper { get; }

}
