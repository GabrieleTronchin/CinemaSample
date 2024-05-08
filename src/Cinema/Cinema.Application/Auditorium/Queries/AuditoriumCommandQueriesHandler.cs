using Cinema.Application.Auditorium.Queries.Commands;
using Cinema.Application.Auditorium.Queries.Models;
using Cinema.Domain.AuditoriumDefinition.Repository;
using MediatR;

namespace Cinema.Application.Auditorium.Queries;

/// <summary>
/// Just a sample of read model
/// </summary>
public class AuditoriumCommandQueriesHandler
    : IRequestHandler<GetEntitiesCommand, IList<AuditoriumReadModel>>
{
    private readonly IAuditoriumRepository _repository;

    public AuditoriumCommandQueriesHandler(IAuditoriumRepository repository)
    {
        _repository = repository; // using same rep as write model, but it could be different ex.: Dapper
    }

    public async Task<IList<AuditoriumReadModel>> Handle(
        GetEntitiesCommand request,
        CancellationToken cancellationToken
    )
    {
        //in real word scenario the idea is to compose query using TSQL and Dapper
        var allAuditoriums = await _repository.GetAllAsync(null, cancellationToken);

        var readModelAuditorium = new List<AuditoriumReadModel>();

        foreach (var item in allAuditoriums)
        {
            var seats = item.Seats.Select(s => new SeatReadModel()
            {
                Row = s.RowNumber,
                SeatNumber = s.SeatNumber
            });
            readModelAuditorium.Add(new AuditoriumReadModel() { Id = item.Id, Seats = seats });
        }

        return readModelAuditorium;
    }
}
