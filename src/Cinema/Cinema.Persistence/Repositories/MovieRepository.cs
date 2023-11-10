using Cinema.Domain;
using Cinema.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Persistence.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaContext _context;

        public MovieRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<MovieEntity> CreateAsync(MovieEntity movie, CancellationToken cancel)
        {
            var entity = await _context.Movies.AddAsync(movie, cancel);
            await _context.SaveChangesAsync(cancel);
            return entity.Entity;
        }


        public async Task<MovieEntity?> GetByExternalId(string imdbId, CancellationToken cancel)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(x => x.ImdbId == imdbId, cancel);
        }

    }
}
