using Cinema.Application.Auditorium.Queries.Models;
using Cinema.Domain.AuditoriumDefinition.Repository;

namespace Cinema.Application.Auditorium.Queries;

/// <summary>
/// Just a sample of read model
/// </summary>
public class AuditoriumQueries : IAuditoriumQueries
{
    private readonly IAuditoriumRepository _repository;

    public AuditoriumQueries(IAuditoriumRepository repository)
    {
        _repository = repository; // using same rep as write model, but it could be different ex.: Dapper
    }

    public async Task<IEnumerable<AuditoriumReadModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        //in real word scenario the idea is to compose query using TSQL and Dapper
        var allAuditoriums = await _repository.GetAllAsync(null, cancellationToken);

        var readModelAuditorium = new List<AuditoriumReadModel>();

        foreach (var item in allAuditoriums)
        {
            var seats = item.Seats.Select(s => new SeatReadModel() { Row = s.RowNumber, SeatNumber = s.SeatNumber });
            readModelAuditorium.Add(new AuditoriumReadModel() { Id = item.Id, Seats = seats });
        }

        return readModelAuditorium;
    }
}
