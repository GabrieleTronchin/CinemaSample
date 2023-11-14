using Cinema.Domain.Showtime.Repository;
using Cinema.Domain.Ticket.DomainEvents;

namespace Cinema.Domain.Showtime.EventHandlers;

internal sealed class PaymentAcceptedEventHandler : INotificationHandler<PaymentAccepted>
{
    private readonly IShowtimesRepository _showtimesRepository;

    public PaymentAcceptedEventHandler(IShowtimesRepository showtimesRepository)
    {
        _showtimesRepository = showtimesRepository;
    }

    public async Task Handle(PaymentAccepted notification, CancellationToken cancellationToken)
    {
        var showtime = await _showtimesRepository.GetAsync(notification.ShowtimeId, cancellationToken);

        showtime.HasBeenPurchased(notification.Seats);
        await _showtimesRepository.SaveChangesAsync();
    }
}
