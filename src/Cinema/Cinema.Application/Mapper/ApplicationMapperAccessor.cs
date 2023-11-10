using AutoMapper;
using Auditorium = Cinema.Application.Queries.Auditorium.Models;
using Showtime = Cinema.Application.Queries.Showtime.Models;
using Cinema.Domain;
using Cinema.Application.Queries.Auditorium.Models;

namespace Cinema.Application.Mapper;
public class ApplicationMapperAccessor : IApplicationMapperAccessor
{
    public ApplicationMapperAccessor()
    {

        AppMapper = new MapperConfiguration(m =>
        {

            m.CreateMap<AuditoriumEntity, Auditorium.AuditoriumReadModel>();
            m.CreateMap<SeatEntity, Auditorium.SeatReadModel>()
             .ForMember(x => x.SeatStatus, op => op.MapFrom(s => SeatStatus.Free)); //set all free then parse on the Query model before return

            m.CreateMap<ShowtimeEntity, Auditorium.ShowTimeReadModel>()
             .ForMember(x => x.Title, op => op.MapFrom(s => s.Movie.Title))
             .ForMember(x => x.Stars, op => op.MapFrom(s => s.Movie.Stars));

            m.CreateMap<ShowtimeEntity, Showtime.ShowTimeReadModel>();
            m.CreateMap<MovieEntity, Showtime.MovieReadModel>();

        }).CreateMapper();

    }

    public IMapper AppMapper { get; }

}
