using Cinema.Application.Commands;
using Cinema.Persistence.Repositories.Abstractions;
using MediatR;

namespace Cinema.Application.Handlers
{
    public class ReservationCommandHandler : IRequestHandler<ReservationCommand, ReservationComplete>
    {

        private readonly ITicketsRepository _ticketsRepository;
        private readonly IShowtimesRepository _showtimesRepository;

        public ReservationCommandHandler(ITicketsRepository ticketsRepository,
                                             IShowtimesRepository showtimesRepository)
        {
            _ticketsRepository = ticketsRepository;
            _showtimesRepository = showtimesRepository;
        }

        public async Task<ReservationComplete> Handle(ReservationCommand request, CancellationToken cancellationToken)
        {



            //-It should not be possible to reserve the same seats two times in 10 minutes.
            //- It shouldn't be possible to reserve an already sold seat.
            //- All the seats, when doing a reservation, need to be contiguous.

            throw new NotImplementedException();

            //var showtimeWithTicket = await _showtimesRepository.GetWithTicketsByIdAsync(request.ShowtimeId, cancellationToken);

            //var seat2Book = request.Seats.Select(x => new SeatEntity() { Row = x.Row, SeatNumber = x.SeatsNumber, AuditoriumId = request.AuditoriumId });

            //var createdTicket = await _ticketsRepository.CreateAsync(showtimeWithTicket, seat2Book, cancellationToken);

            //return new ReservationComplete()
            //{
            //    AuditoriumId = showtimeWithTicket.AuditoriumId,
            //    MovieTitle = showtimeWithTicket.Movie.Title,
            //    Id = createdTicket.Id,
            //    SeatsNumber = request.Seats
            //};
        }

    }


}