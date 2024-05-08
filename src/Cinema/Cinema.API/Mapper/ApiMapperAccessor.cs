﻿using AutoMapper;
using Cinema.Api.Models.ConfirmReservation;
using Cinema.Api.Models.SeatReservation;
using Cinema.Api.Models.Showtime;
using Cinema.Application.Showtime.Commands;
using Cinema.Application.Ticket.Commands;
using ShowTimeProto;

namespace Cinema.Api.Mapper;

public class ApiMapperAccessor : IApiMapperAccessor
{
    public ApiMapperAccessor()
    {
        ApiMapper = new MapperConfiguration(m =>
        {
            m.CreateMap<CreateShowTimeRequest, CreateShowtimeCommand>();
            m.CreateMap<MovieApiModel, Movie>();

            m.CreateMap<SeatReservationRequest, ReservationCommand>();
            m.CreateMap<Seat, ReservationSeat>().ReverseMap();

            m.CreateMap<ShowtimeCreationRequest, CreateShowtimeCommand>()
                .ForMember(
                    x => x.SessionDate,
                    opts => opts.MapFrom(src => src.SessionDate.ToDateTime())
                );

            m.CreateMap<movieRequest, Movie>()
                .ForMember(
                    x => x.ReleaseDate,
                    opts => opts.MapFrom(src => src.ReleaseDate.ToDateTime())
                );

            m.CreateMap<ReservationComplete, SeatReservationResponse>();
            m.CreateMap<ConfirmReservationRequest, ReservationConfirmationCommand>();
        }).CreateMapper();
    }

    public IMapper ApiMapper { get; }
}
