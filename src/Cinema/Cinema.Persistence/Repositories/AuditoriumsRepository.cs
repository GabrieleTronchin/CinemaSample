using Cinema.Domain;
using Cinema.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema.Persistence.Repositories
{
    public class AuditoriumsRepository : IAuditoriumsRepository
    {
        private readonly CinemaContext _context;

        public AuditoriumsRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditoriumEntity>> GetAllWithAllDependecyAsync(Expression<Func<AuditoriumEntity, bool>> filter, CancellationToken cancel)
        {
            if (filter == null)
            {
                return await _context.Auditoriums
                                .Include(x => x.Seats)
                                .Include(x => x.Showtimes)
                                .ThenInclude(x => x.Movie)
                                 .Include(x => x.Showtimes)
                                 .ThenInclude(x => x.Tickets)
                                .ToListAsync(cancel);
            }

            return await _context.Auditoriums
                                .Include(x => x.Seats)
                                .Include(x => x.Showtimes)
                                .ThenInclude(x => x.Movie)
                                 .Include(x => x.Showtimes)
                                 .ThenInclude(x => x.Tickets)
                                .Where(filter)
                                .ToListAsync(cancel);
        }


        public async Task<IEnumerable<AuditoriumEntity>> GetAllAsync(Expression<Func<AuditoriumEntity, bool>> filter, CancellationToken cancel)
        {
            if (filter == null)
            {
                return await _context.Auditoriums
                                .Include(x => x.Seats)
       
                                .ToListAsync(cancel);
            }

            return await _context.Auditoriums
                                .Include(x => x.Seats)
                                .Where(filter)
                                .ToListAsync(cancel);
        }

        public async Task<AuditoriumEntity> GetAsync(int auditoriumId, CancellationToken cancel)
        {
            return await _context.Auditoriums
                .Include(x => x.Showtimes)
                .Include(x => x.Seats)
                .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancel);
        }


    }
}
