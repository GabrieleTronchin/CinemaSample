using Cinema.Domain.Ticket;
using Cinema.Domain.Ticket.Repository;

namespace Cinema.Persistence.Repositories
{
    internal class TicketsRepository : ITicketsRepository
    {
        private readonly CinemaDbContext _context;

        public TicketsRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<TicketEntity> GetAsync(Guid id, CancellationToken cancel)
        {
            return await _context.Tickets.SingleAsync(x => x.Id == id, cancel);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
