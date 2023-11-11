using Cinema.Application.Queries.Showtime.Models;

namespace Cinema.Application.Queries.Showtime
{
    public interface IShowtimeQueries
    {
        Task<IEnumerable<ShowTimeReadModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ShowTimeReadModel?> GetSingleAsync(Guid id, CancellationToken cancellationToken = default);
    }
}