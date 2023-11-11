namespace Cinema.Domain.Ticket.Repository
{
    public interface ITicketsRepository
    {
        Task<TicketEntity> GetAsync(Guid id, CancellationToken cancel);
    }
}