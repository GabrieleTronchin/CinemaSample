using Cinema.Application.Mapper;
using Cinema.Application.Queries.Models;
using Cinema.Persistence.Repositories.Abstractions;

namespace Cinema.Application.Queries
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

        public async Task<IEnumerable<ShowTimeReadModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var showtimes = await _showtimesRepository.GetAllAsync(null, cancellationToken);
            return _applicationMapperAccessor.AppMapper.Map<IEnumerable<ShowTimeReadModel>>(showtimes);
        }

        public async Task<ShowTimeReadModel?> GetSingleAsync(int id, CancellationToken cancellationToken)
        {
            var showtime = await _showtimesRepository.GetWithMoviesByIdAsync(id, cancellationToken);

            return _applicationMapperAccessor.AppMapper.Map<ShowTimeReadModel>(showtime);
        }
    }
}
