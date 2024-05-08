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

        public async Task AddAsync(TicketEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<TicketEntity> GetAsync(Guid id, CancellationToken cancel)
        {
            return await _context.Tickets.SingleOrDefaultAsync(x => x.Id == id, cancel)
                ?? throw new InvalidOperationException(
                    $"System could not find any {nameof(TicketEntity.Id)} with value {id}"
                );
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
