using Cinema.Application.Ticket.Commands;
using Cinema.Domain;
using Cinema.Domain.Showtime.Repository;
using Cinema.Domain.Ticket;
using Cinema.Domain.Ticket.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Ticket;

public class ReservationCommandHandler : IRequestHandler<ReservationCommand, ReservationComplete>
{
    private readonly ILogger<ReservationCommandHandler> _logger;
    private readonly IShowtimesRepository _showtimesRepository;
    private readonly ITicketsRepository _ticketsRepository;

    public ReservationCommandHandler(ILogger<ReservationCommandHandler> logger,
                                     IShowtimesRepository showtimesRepository,
                                     ITicketsRepository ticketsRepository)
    {
        _logger = logger;
        _showtimesRepository = showtimesRepository;
        _ticketsRepository = ticketsRepository;
    }

    public async Task<ReservationComplete> Handle(ReservationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var showtime = await _showtimesRepository.GetAsync(request.ShowtimeId, cancellationToken);

            var seatsToReserve = request.Seats.Select(x => new Seat(x.Row, x.SeatsNumber));
            showtime.ReserveSeats(seatsToReserve);

            var newTicket = TicketEntity.Create(seatsToReserve, showtime.Id, showtime.Movie.Title);
            // TODO manage expired reservation for already create tickets
            await _ticketsRepository.AddAsync(newTicket);

            await _showtimesRepository.SaveChangesAsync();

            return new ReservationComplete
            {
                Id = newTicket.Id,
                AuditoriumId = showtime.AuditoriumId,
                MovieTitle = showtime.Movie.Title,
                Seats = request.Seats
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred at {CommandHandler}", nameof(ReservationCommandHandler));
            throw;
        }

    }
}