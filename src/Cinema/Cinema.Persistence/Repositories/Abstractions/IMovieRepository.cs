using Cinema.Domain.Showtime;

namespace Cinema.Persistence.Repositories.Abstractions
{
    public interface IMovieRepository
    {
        Task<MovieEntity?> GetByExternalId(string imdbId, CancellationToken cancel);

        Task<MovieEntity> CreateAsync(MovieEntity movie, CancellationToken cancel);

    }
}