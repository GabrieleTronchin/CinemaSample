namespace Cinema.Domain.Primitives;

public interface IRepository<T> where T : class
{
    Task SaveChangesAsync();

    Task AddAsync(T entity);
}
