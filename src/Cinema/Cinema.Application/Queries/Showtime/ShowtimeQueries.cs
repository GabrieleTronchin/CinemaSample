using Cinema.Application.Mapper;
using Cinema.Application.Queries.Showtime.Models;
using Cinema.Domain.Showtime.Repository;

namespace Cinema.Application.Queries.Showtime
{
    public class ShowtimeQueries : IShowtimeQueries
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IApplicationMapperAccessor _applicationMapperAccessor;

        public ShowtimeQueries(IShowtimesRepository showtimesRepository,
                               IApplicationMapperAccessor applicationMapperAccessor)
        {
            _showtimesRepository = showtimesRepository; // using same rep as write model, but it could be different ex.: Dapper
            _applicationMapperAccessor = applicationMapperAccessor;
        }

        public async Task<IEnumerable<ShowTimeReadModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var showtimes = await _showtimesRepository.GetAllAsync(null, cancellationToken);
            return _applicationMapperAccessor.AppMapper.Map<IEnumerable<ShowTimeReadModel>>(showtimes);
        }

        public async Task<ShowTimeReadModel?> GetSingleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var showtime = await _showtimesRepository.GetAsync(id, cancellationToken);

            return _applicationMapperAccessor.AppMapper.Map<ShowTimeReadModel>(showtime);
        }
    }
}
