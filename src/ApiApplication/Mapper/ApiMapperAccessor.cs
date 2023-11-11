using AutoMapper;
using Cinema.Api.Models.ConfirmReservation;
using Cinema.Api.Models.SeatReservation;
using Cinema.Application.Commands;
using Cinema.Application.Queries.Auditorium.Models;
using Cinema.Domain.AuditoriumDefinition;
using System;
using System.Linq;

namespace Cinema.Api.Mapper;
public class ApiMapperAccessor : IApiMapperAccessor
{
    public ApiMapperAccessor()
    {

        ApiMapper = new MapperConfiguration(m =>
        {
            m.CreateMap<SeatReservationRequest, ReservationCommand>()
             .ForMember(x => x.Id, op => op.MapFrom(s => Guid.NewGuid()));

            m.CreateMap<ConfirmReservationRequest, ReservationConfirmationCommand>();

        }).CreateMapper();

    }

    public IMapper ApiMapper { get; }

}
