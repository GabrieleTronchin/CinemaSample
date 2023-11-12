using Cinema.Domain.Primitives;

namespace Cinema.Domain.Ticket.Repository;

public interface ITicketsRepository : IRepository<TicketEntity>
{
    Task<TicketEntity> GetAsync(Guid id, CancellationToken cancel);
}