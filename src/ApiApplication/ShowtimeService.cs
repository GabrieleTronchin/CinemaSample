using Cinema.Domain;
using Cinema.Persistence.Repositories.Abstractions;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ShowTimeProto;
using System;
using System.Threading.Tasks;

namespace Cinema.Api
{

    public class ShowtimeService : ShowTimeApi.ShowTimeApiBase
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly ILogger<ShowtimeService> _logger;

        public ShowtimeService(ILogger<ShowtimeService> logger, IShowtimesRepository showtimesRepository)
        {
            _showtimesRepository = showtimesRepository;
            _logger = logger;
        }

        public override async Task<responseModel> CreateShowTime(ShowtimeCreationRequest request, ServerCallContext context)
        {
            try
            {
                //TODO refactor completly move on a single domain model and add also a API

                ShowtimeEntity showtimeEntity = new()
                {
                    AuditoriumId = request.AuditoriumId,
                    Movie = new MovieEntity()
                    {
                        ImdbId = request.Movie.ImdbId,
                        Title = request.Movie.Title,
                        ReleaseDate = request.Movie.ReleaseDate.ToDateTime(),
                        Stars = request.Movie.Stars,

                    },
                    SessionDate = request.SessionDate.ToDateTime(),
                };
                var t = context.CancellationToken;

                var ret = await _showtimesRepository.CreateShowtime(showtimeEntity, t);

                return new responseModel() { Success = true }; //TODO Fix return
            }
            catch (Exception ex)
            {
                //TODO Fix return

                _logger.LogError(ex, $"An error occurred at {nameof(ShowtimeService)}");
                var errorRetModel = new responseModel() { Success = false };
                errorRetModel.Exceptions.Add(new moviesApiException() { Message = ex.Message, StatusCode = 500 });

                return errorRetModel;
            }

        }


    }

}
