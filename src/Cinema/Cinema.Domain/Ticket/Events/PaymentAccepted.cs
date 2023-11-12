using Cinema.Domain.Primitives;

namespace Cinema.Domain.Ticket.Events;

internal sealed record class PaymentAccepted(Guid ShowtimeId, IEnumerable<Seat> Seats) : IDomainEvent
{
}
