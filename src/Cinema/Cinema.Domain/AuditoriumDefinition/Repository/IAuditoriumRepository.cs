using System.Linq.Expressions;

namespace Cinema.Domain.AuditoriumDefinition.Repository
{
    public interface IAuditoriumRepository
    {
        Task<AuditoriumEntity> GetAsync(int auditoriumId, CancellationToken cancel);
        Task<IEnumerable<AuditoriumEntity>> GetAllAsync(Expression<Func<AuditoriumEntity, bool>> filter, CancellationToken cancel);
        Task<IEnumerable<AuditoriumEntity>> GetAllWithAllDependecyAsync(Expression<Func<AuditoriumEntity, bool>> filter, CancellationToken cancel);
    }
}