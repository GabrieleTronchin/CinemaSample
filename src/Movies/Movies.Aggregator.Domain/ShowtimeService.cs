using Cinema.Client;
using Microsoft.Extensions.Logging;
using Movies.Aggregator.Domain.Models;
using Movies.Client;

namespace Movies.Aggregator.Domain
{
    internal class ShowtimeService : IShowtimeService
    {
        private readonly ILogger<ShowtimeService> _logger;
        private readonly IMoviesClientGrpc _moviesClient;
        private readonly ICinemaClientGrpc _cinemaClientGrpc;

        public ShowtimeService(ILogger<ShowtimeService> logger,
                               IMoviesClientGrpc moviesClient,
                               ICinemaClientGrpc cinemaClientGrpc)
        {
            _logger = logger;
            _moviesClient = moviesClient;
            _cinemaClientGrpc = cinemaClientGrpc;
        }

        public async Task Create(CreateShowTime createRequest)
        {

            //TODO Add polly & add automapper
            var response = await _moviesClient.GetById(createRequest.ImdbId);

            var showReq = new ShowTimeProto.ShowtimeCreationRequest()
            {
                AuditoriumId = createRequest.AuditoriumId,
                SessionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(createRequest.SessionDate),
                Movie = new ShowTimeProto.movieRequest()
                {
                    ImdbId = createRequest.ImdbId,
                    Title = response.FullTitle,
                    Stars = response.Crew,
                    ReleaseDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow) //TODO Fix with automapper
                }
            };

            await _cinemaClientGrpc.CreateShowTime(showReq);

        }


    }
}
