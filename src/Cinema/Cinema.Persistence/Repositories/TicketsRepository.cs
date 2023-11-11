using Cinema.Domain.Ticket;
using Cinema.Domain.Ticket.Repository;

namespace Cinema.Persistence.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly CinemaDbContext _context;

        public TicketsRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public Task<TicketEntity> GetAsync(Guid id, CancellationToken cancel)
        {
            return _context.Tickets.FirstOrDefaultAsync(x => x.Id == id, cancel);
        }

        public async Task<IEnumerable<TicketEntity>> GetEnrichedAsync(int showtimeId, CancellationToken cancel)
        {
            return await _context.Tickets
                .Include(x => x.Showtime)
                .Include(x => x.Seats)
                .Where(x => x.ShowtimeId == showtimeId)
                .ToListAsync(cancel);
        }

        public async Task CreateAsync(TicketEntity entity, CancellationToken cancel)
        {
            _context.Tickets.Add(entity);

            await _context.SaveChangesAsync(cancel);
        }

        public async Task<TicketEntity> ConfirmPaymentAsync(TicketEntity ticket, CancellationToken cancel)
        {
            ticket.Paid = true;
            _context.Update(ticket);
            await _context.SaveChangesAsync(cancel);
            return ticket;
        }
    }
}
