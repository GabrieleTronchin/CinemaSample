using Cinema.Domain.DomainEvents.Abstractions;

namespace Cinema.Domain.DomainEvents;

public sealed record class PaymentAccepted(Guid ShowtimeId, IEnumerable<Seat> Seats) : IDomainEvent
{
}
