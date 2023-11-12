using Cinema.Domain.Primitives;

namespace Cinema.Persistence;



internal class UnitOfWork : IUnitOfWork //draft implementation
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


    public void Dispose()
    {
        _context.Dispose();
    }
}
