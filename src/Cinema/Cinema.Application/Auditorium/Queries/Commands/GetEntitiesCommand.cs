using MediatR;

namespace Cinema.Application.Auditorium.Queries.Commands
{
    public class GetEntitiesCommand : IRequest<IList<AuditoriumReadModel>> { }
}
