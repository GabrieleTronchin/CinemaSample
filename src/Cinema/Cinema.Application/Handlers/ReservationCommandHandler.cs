using Cinema.Application.Commands;
using Cinema.Domain;
using Cinema.Domain.Showtime.Repository;
using Cinema.Domain.Ticket.Repository;
using MediatR;

namespace Cinema.Application.Handlers
{
    public class ReservationCommandHandler : IRequestHandler<ReservationCommand, ReservationComplete>
    {
        private readonly IShowtimesRepository _showtimesRepository;

        public ReservationCommandHandler(ITicketsRepository ticketsRepository,
                                             IShowtimesRepository showtimesRepository)
        {
            _showtimesRepository = showtimesRepository;
        }

        public async Task<ReservationComplete> Handle(ReservationCommand request, CancellationToken cancellationToken)
        {
            //-It should not be possible to reserve the same seats two times in 10 minutes.
            //- It shouldn't be possible to reserve an already sold seat.
            //- All the seats, when doing a reservation, need to be contiguous.
            var showtime = await _showtimesRepository.GetAsync(request.ShowtimeId, cancellationToken);

            var seatsToReserve = request.seats.Select(x => new Seat(x.Row, x.SeatsNumber));
            showtime.ReserveSeats(seatsToReserve);

            return new ReservationComplete
            {
                AuditoriumId = showtime.AuditoriumId,
                MovieTitle = showtime.Movie.Title,
                SeatsNumber = request.seats
            };
        }
    }
}