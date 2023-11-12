using Cinema.Application.Commands;
using Cinema.Domain;
using Cinema.Domain.Showtime.Repository;
using Cinema.Domain.Ticket.Repository;
using Cinema.Persistence.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Handlers
{
    public class ReservationCommandHandler : IRequestHandler<ReservationCommand, ReservationComplete>
    {
        private readonly ILogger<ReservationCommandHandler> _logger;
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationCommandHandler(ILogger<ReservationCommandHandler> logger,
            IUnitOfWork unitOfWork,
                                             IShowtimesRepository showtimesRepository)
        {
            _logger = logger;
            _showtimesRepository = showtimesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationComplete> Handle(ReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //-It should not be possible to reserve the same seats two times in 10 minutes.
                //- It shouldn't be possible to reserve an already sold seat.
                //- All the seats, when doing a reservation, need to be contiguous.
                var showtime = await _showtimesRepository.GetAsync(request.ShowtimeId, cancellationToken);

                var seatsToReserve = request.seats.Select(x => new Seat(x.Row, x.SeatsNumber));
                showtime.ReserveSeats(seatsToReserve);

                _unitOfWork.Commit();

                return new ReservationComplete
                {
                    AuditoriumId = showtime.AuditoriumId,
                    MovieTitle = showtime.Movie.Title,
                    SeatsNumber = request.seats
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at {CommandHandler}", nameof(ReservationCommandHandler));
                _unitOfWork.Rollback();
                throw;
            }

        }
    }
}