using Cinema.Domain.Primitives;

namespace Cinema.Domain.Ticket.DomainEvents;

public sealed record class PaymentAccepted(Guid ShowtimeId, IEnumerable<Seat> Seats)
    : IDomainEvent { }
