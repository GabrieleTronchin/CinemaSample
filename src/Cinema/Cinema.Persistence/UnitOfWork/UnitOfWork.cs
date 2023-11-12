namespace Cinema.Persistence.UnitOfWork;

internal class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Rollback()
    {
        // Rollback changes if needed
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
