using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.Repository;
using System.Linq.Expressions;

namespace Cinema.Persistence.Repositories;

internal class ShowtimesRepository : IShowtimesRepository
{
    private readonly CinemaDbContext _context;

    public ShowtimesRepository(CinemaDbContext context)
    {
        _context = context;
    }

    public async Task<ShowtimeEntity> GetAsync(Guid id, CancellationToken cancel)
    {
        return await _context.Showtimes
            .Include(x => x.Movie)
             .Include(x => x.Seats)
            .SingleOrDefaultAsync(x => x.Id == id, cancel);
    }


    public async Task<IEnumerable<ShowtimeEntity>> GetAllAsync(Expression<Func<ShowtimeEntity, bool>> filter, CancellationToken cancel)
    {
        if (filter == null)
        {
            return await _context.Showtimes
            .Include(x => x.Movie)
            .Include(x => x.Seats)
            .ToListAsync(cancel);
        }
        return await _context.Showtimes
            .Include(x => x.Movie)
            .Include(x => x.Seats)
            .Where(filter)
            .ToListAsync(cancel);
    }

    public async Task<ShowtimeEntity> CreateShowtime(ShowtimeEntity showtimeEntity, CancellationToken cancel)
    {
        throw new NotImplementedException();
    }
}
