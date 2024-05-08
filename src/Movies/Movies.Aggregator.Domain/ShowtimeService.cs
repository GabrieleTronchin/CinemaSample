using Cinema.Client;
using Microsoft.Extensions.Logging;
using Movies.Aggregator.Domain.Models;
using Movies.Client;
using ServiceCache;

namespace Movies.Aggregator.Domain;

internal class ShowtimeService : IShowtimeService
{
    private readonly ILogger<ShowtimeService> _logger;
    private readonly IMoviesClientGrpc _moviesClient;
    private readonly ICinemaClientGrpc _cinemaClientGrpc;
    private readonly ICacheService _serviceCache;

    public ShowtimeService(
        ILogger<ShowtimeService> logger,
        IMoviesClientGrpc moviesClient,
        ICinemaClientGrpc cinemaClientGrpc,
        ICacheService serviceCache
    )
    {
        _logger = logger;
        _moviesClient = moviesClient;
        _cinemaClientGrpc = cinemaClientGrpc;
        _serviceCache = serviceCache;
    }

    public async Task<CreateShowTimeResponse> Create(CreateShowTime createRequest)
    {
        var movieResponse =
            await GetMovieByImdbId(createRequest.ImdbId)
            ?? throw new ArgumentException(
                $"Unable to find any movie with the provided id:{createRequest.ImdbId}"
            );

        var showReq = new ShowTimeProto.ShowtimeCreationRequest()
        {
            AuditoriumId = createRequest.AuditoriumId,
            SessionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                createRequest.SessionDate
            ),
            Movie = new ShowTimeProto.movieRequest()
            {
                ImdbId = createRequest.ImdbId,
                Title = movieResponse.FullTitle,
                Stars = movieResponse.Crew,
                ReleaseDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
            }
        };

        var cinemaResponse = await _cinemaClientGrpc.CreateShowTime(showReq);

        if (!cinemaResponse.Success)
            throw new InvalidOperationException(
                string.Join(',', cinemaResponse.Exceptions.Select(x => x.Message))
            );

        if (!Guid.TryParse(cinemaResponse.ShotimeId, out var shotimeId))
            throw new InvalidCastException(
                $"{nameof(cinemaResponse.ShotimeId)} is not a valid guid"
            );

        return new CreateShowTimeResponse() { Id = shotimeId };
    }

    private async Task<ProtoDefinitions.showResponse?> GetMovieByImdbId(string imdbId)
    {
        try
        {
            return await _serviceCache.CreateAndSetAsync(
                imdbId,
                () => _moviesClient.GetById(imdbId)
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to retrieve data from movie endpoint.");
        }

        return await _serviceCache.GetOrDefault<ProtoDefinitions.showResponse?>(imdbId, null);
    }
}
