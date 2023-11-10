using Cinema.Application.Queries.Models;

namespace Cinema.Application.Queries
{
    public interface IShowtimeQueries
    {
        Task<IEnumerable<ShowTimeReadModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ShowTimeReadModel?> GetSingleAsync(int id, CancellationToken cancellationToken = default);
    }
}