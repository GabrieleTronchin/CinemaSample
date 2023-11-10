﻿using Cinema.Application.Mapper;
using Cinema.Application.Queries.Auditorium.Models;
using Cinema.Persistence.Repositories.Abstractions;

namespace Cinema.Application.Queries.Auditorium
{
    public class AuditoriumQueries : IAuditoriumQueries
    {
        private readonly IAuditoriumsRepository _repository;
        private readonly IApplicationMapperAccessor _mapper;

        public AuditoriumQueries(IAuditoriumsRepository repository,
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
            var allAuditoriums = await _repository.GetAllAsync(null, cancellationToken);
            var auditoriums = _mapper.AppMapper.Map<List<AuditoriumReadModel>>(allAuditoriums);

            //this is a workaround to fill seat ( in real word scenario the idea is to compose query using TSQL and Dapper
            foreach (var auditorium in auditoriums)
            {
                foreach (var showtime in auditorium.Showtimes)
                {
                    //showtime.Seats
                }
            }


            return auditoriums;
        }
    }
}
