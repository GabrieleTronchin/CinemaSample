using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Primitives;

public interface IRepository<T> where T : class
{
    Task SaveChangesAsync();

    Task AddAsync(T entity);
}
