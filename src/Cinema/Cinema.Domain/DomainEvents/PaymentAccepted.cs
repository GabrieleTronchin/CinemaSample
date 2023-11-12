using Cinema.Domain.DomainEvents.Abstractions;

namespace Cinema.Domain.DomainEvents;

public sealed record class PaymentAccepted(int ShowtimeId, IEnumerable<Seat> Seats):IDomainEvent
{
}
