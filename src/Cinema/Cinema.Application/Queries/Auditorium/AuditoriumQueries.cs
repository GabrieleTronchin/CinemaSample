using Cinema.Application.Mapper;
using Cinema.Application.Queries.Auditorium.Models;
using Cinema.Domain.AuditoriumDefinition.Repository;

namespace Cinema.Application.Queries.Auditorium
{
    /// <summary>
    /// Just a sample of read model
    /// </summary>
    public class AuditoriumQueries : IAuditoriumQueries
    {
        private readonly IAuditoriumRepository _repository;
        private readonly IApplicationMapperAccessor _mapper;

        public AuditoriumQueries(IAuditoriumRepository repository,
                               IApplicationMapperAccessor applicationMapperAccessor)
        {
            _repository = repository; // using same rep as write model, but it could be different ex.: Dapper
            _mapper = applicationMapperAccessor;
        }

        public async Task<AuditoriumReadModel?> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        {
            var showtime = await _repository.GetAsync(id, cancellationToken);

            return _mapper.AppMapper.Map<AuditoriumReadModel>(showtime);
        }


        public async Task<IEnumerable<AuditoriumReadModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            //in real word scenario the idea is to compose query using TSQL and Dapper
            var allAuditoriums = await _repository.GetAllAsync(null, cancellationToken);

            var readModelAuditorium = new List<AuditoriumReadModel>();

            foreach (var item in allAuditoriums)
            {
                var seats = item.Seats.Select(s => new SeatReadModel() { Row = s.RowNumber, SeatNumber = s.SeatNumber });
                readModelAuditorium.Add(new AuditoriumReadModel() { Id = item.Id, Seats = seats });
            }

            return readModelAuditorium;
        }
    }
}
