using Cinema.Domain.AuditoriumDefinition.Repository;
using System.Linq.Expressions;

namespace Cinema.Persistence.Repositories
{
    internal class AuditoriumsRepository : IAuditoriumRepository
    {
        private readonly CinemaDbContext _context;

        public AuditoriumsRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditoriumEntity>> GetAllAsync(Expression<Func<AuditoriumEntity, bool>> filter, CancellationToken cancel)
        {
            if (filter == null)
            {
                return await _context.Auditoriums
                                .ToListAsync(cancel);
            }

            return await _context.Auditoriums
                                .Where(filter)
                                .ToListAsync(cancel);
        }



        public async Task<AuditoriumEntity> GetAsync(int auditoriumId, CancellationToken cancel)
        {
            return await _context.Auditoriums
                .Include(x => x.Seats)
                .SingleAsync(x => x.Id == auditoriumId, cancel);
        }


    }
}
