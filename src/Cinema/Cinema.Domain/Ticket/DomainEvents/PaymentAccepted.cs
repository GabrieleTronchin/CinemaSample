using Cinema.Domain.Primitives;

namespace Cinema.Domain.Ticket.Events;

public sealed record class PaymentAccepted(Guid ShowtimeId, IEnumerable<Seat> Seats) : IDomainEvent
{
}
