

namespace Cinema.Domain.Showtime.Repository;

public interface IShowtimesRepository
{
    Task<ShowtimeEntity> GetAsync(int id, CancellationToken cancel);
    Task<IEnumerable<ShowtimeEntity>> GetAllAsync(Expression<Func<ShowtimeEntity, bool>> filter, CancellationToken cancel);
    Task<ShowtimeEntity> CreateShowtime(ShowtimeEntity showtimeEntity, CancellationToken cancel);

}