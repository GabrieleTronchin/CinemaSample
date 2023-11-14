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
                .SingleOrDefaultAsync(x => x.Id == auditoriumId, cancel) ??
                    throw new InvalidOperationException($"System could not find any {nameof(auditoriumId)} with value {auditoriumId}");
        }

        public async Task AddAsync(AuditoriumEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
