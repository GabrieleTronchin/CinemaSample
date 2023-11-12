using Cinema.Application.Auditorium.Queries.Models;

namespace Cinema.Application.Auditorium.Queries;

public interface IAuditoriumQueries
{
    Task<IEnumerable<AuditoriumReadModel>> GetAllAsync(CancellationToken cancellationToken = default);
}