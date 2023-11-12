﻿

using Cinema.Domain.Primitives;

namespace Cinema.Domain.Showtime.Repository;

public interface IShowtimesRepository : IRepository
{
    Task<ShowtimeEntity> GetAsync(Guid id, CancellationToken cancel);
    Task<IEnumerable<ShowtimeEntity>> GetAllAsync(Expression<Func<ShowtimeEntity, bool>> filter, CancellationToken cancel);
}