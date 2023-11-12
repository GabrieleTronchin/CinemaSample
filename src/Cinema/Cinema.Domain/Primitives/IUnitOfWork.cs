namespace Cinema.Domain.Primitives;

public interface IUnitOfWork : IDisposable
{
    void Commit();
}
