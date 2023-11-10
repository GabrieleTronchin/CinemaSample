using AutoMapper;
using Auditorium = Cinema.Application.Queries.Auditorium;
using Showtime = Cinema.Application.Queries.Showtime;
using Cinema.Domain;

namespace Cinema.Application.Mapper;
public class ApplicationMapperAccessor : IApplicationMapperAccessor
{
    public ApplicationMapperAccessor()
    {

        AppMapper = new MapperConfiguration(m =>
        {

            m.CreateMap<AuditoriumEntity, Auditorium.Models.AuditoriumReadModel>();
            m.CreateMap<SeatEntity, Auditorium.Models.SeatReadModel>();

            m.CreateMap<ShowtimeEntity, Auditorium.Models.ShowTimeReadModel>()
             .ForMember(x => x.Title, op => op.MapFrom(s => s.Movie.Title))
             .ForMember(x => x.Stars, op => op.MapFrom(s => s.Movie.Stars));

            m.CreateMap<ShowtimeEntity, Showtime.Models.ShowTimeReadModel>();
            m.CreateMap<MovieEntity, Showtime.Models.MovieReadModel>();

        }).CreateMapper();

    }

    public IMapper AppMapper { get; }

}
