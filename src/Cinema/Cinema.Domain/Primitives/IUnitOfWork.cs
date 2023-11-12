namespace Cinema.Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void Commit();
}
