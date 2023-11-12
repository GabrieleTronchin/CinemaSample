using Cinema.Application.Commands;
using Cinema.Domain.AuditoriumDefinition.Repository;
using Cinema.Domain.Showtime;
using Cinema.Persistence.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Handlers
{
    public class CreateShowtimeCommandHandler : IRequestHandler<CreateShowtimeCommand, CreateShowtimeCommandComplete>
    {
        private readonly ILogger<CreateShowtimeCommandHandler> _logger;
        private readonly IAuditoriumRepository _auditoriumRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateShowtimeCommandHandler(ILogger<CreateShowtimeCommandHandler> logger,
                                            IAuditoriumRepository auditoriumRepository,
                                            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _auditoriumRepository = auditoriumRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateShowtimeCommandComplete> Handle(CreateShowtimeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug("Starting handling an event at '{CommandHandler}'", nameof(CreateShowtimeCommandHandler));

                //Create Movie
                var movie = MovieEntity.Create(request.Movie.Title, request.Movie.Stars, request.Movie.ImdbId, request.Movie.ReleaseDate);


                var auditoriumDefinition = await _auditoriumRepository.GetAsync(request.AuditoriumId, cancellationToken)
                                           ?? throw new InvalidOperationException("Auditorium not exit");

                var showtime = ShowtimeEntity.Create(auditoriumDefinition, movie, request.SessionDate);

                _unitOfWork.Commit();
                return new CreateShowtimeCommandComplete() { Id = showtime.Id };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at {CommandHandler}", nameof(CreateShowtimeCommandHandler));
                throw;
            }
        }
    }


}