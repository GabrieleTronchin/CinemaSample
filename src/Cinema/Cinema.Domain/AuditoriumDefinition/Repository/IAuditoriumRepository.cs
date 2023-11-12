using Cinema.Domain.Primitives;
using System.Linq.Expressions;

namespace Cinema.Domain.AuditoriumDefinition.Repository;

public interface IAuditoriumRepository : IRepository<AuditoriumEntity>
{
    Task<AuditoriumEntity> GetAsync(int auditoriumId, CancellationToken cancel);
    Task<IEnumerable<AuditoriumEntity>> GetAllAsync(Expression<Func<AuditoriumEntity, bool>> filter, CancellationToken cancel);
}