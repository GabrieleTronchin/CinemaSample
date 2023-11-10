using Cinema.Domain;
using System.Linq.Expressions;

namespace Cinema.Persistence.Repositories.Abstractions
{
    public interface IAuditoriumsRepository
    {
        Task<AuditoriumEntity> GetAsync(int auditoriumId, CancellationToken cancel);

        Task<IEnumerable<AuditoriumEntity>> GetAllAsync(Expression<Func<AuditoriumEntity, bool>> filter, CancellationToken cancel);

    }
}