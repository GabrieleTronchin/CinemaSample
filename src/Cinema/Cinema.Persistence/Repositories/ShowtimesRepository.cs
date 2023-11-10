using Cinema.Domain;
using Cinema.Domain.NewFolder;
using Cinema.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema.Persistence.Repositories
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;

        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<ShowtimeEntity> GetWithMoviesByIdAsync(int id, CancellationToken cancel)
        {
            return await _context.Showtimes
                .Include(x => x.Movie)
                .FirstOrDefaultAsync(x => x.Id == id, cancel);
        }

        public async Task<ShowtimeEntity> GetWithTicketsByIdAsync(int id, CancellationToken cancel)
        {
            return await _context.Showtimes
                .Include(x => x.Tickets)
                .FirstOrDefaultAsync(x => x.Id == id, cancel);
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetAllAsync(Expression<Func<ShowtimeEntity, bool>> filter, CancellationToken cancel)
        {
            if (filter == null)
            {
                return await _context.Showtimes
                .Include(x => x.Movie)
                .ToListAsync(cancel);
            }
            return await _context.Showtimes
                .Include(x => x.Movie)
                .Where(filter)
                .ToListAsync(cancel);
        }

        public async Task<ShowtimeEntity> CreateShowtime(ShowtimeEntity showtimeEntity, CancellationToken cancel)
        {
            throw new NotImplementedException();
            //var showtime = await _context.Showtimes.AddAsync(showtimeEntity, cancel);
            //await _context.SaveChangesAsync(cancel);
            //return showtime.Entity;
        }
    }
}
