﻿using Cinema.Application.Commands;
using Cinema.Application.Mapper;
using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Handlers
{
    public class AssignShowtimeCommandHandler : IRequestHandler<AssignShowtimeCommand, AssignShowtimeCommandComplete>
    {
        private readonly ILogger<AssignShowtimeCommandHandler> _logger;
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IApplicationMapperAccessor _applicationMapperAccessor;

        public AssignShowtimeCommandHandler(ILogger<AssignShowtimeCommandHandler> logger,
                                            IApplicationMapperAccessor applicationMapperAccessor,
                                            IShowtimesRepository showtimesRepository)
        {
            _logger = logger;
            _showtimesRepository = showtimesRepository;
            _applicationMapperAccessor = applicationMapperAccessor;
        }

        public async Task<AssignShowtimeCommandComplete> Handle(AssignShowtimeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug("Starting handling an event at '{CommandHandler}'", nameof(AssignShowtimeCommandHandler));

                cancellationToken.ThrowIfCancellationRequested();

                //var showtimeEntity = _applicationMapperAccessor.AppMapper.Map<ShowtimeEntity>(request);

                //var movie = await _movieRepository.GetByExternalId(showtimeEntity.Movie.ImdbId, cancellationToken);

                //if (movie == null) await _movieRepository.CreateAsync(showtimeEntity.Movie, cancellationToken);

                //var createdShowTime = await _showtimesRepository.CreateShowtime(showtimeEntity, cancellationToken);

                //_logger.LogDebug("A event has been handle on '{CommandHandler}'", nameof(AssignShowtimeCommandHandler));

                return new AssignShowtimeCommandComplete() { Id = Guid.NewGuid() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at {CommandHandler}", nameof(AssignShowtimeCommandHandler));
                throw;
            }
        }
    }


}