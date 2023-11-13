using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProtoDefinitions;

namespace Movies.Client;

public class MoviesClientGrpc : IMoviesClientGrpc
{
    private readonly MoviesApi.MoviesApiClient _client;
    private readonly ILogger<MoviesClientGrpc> _logger;
    private readonly Metadata _metadata;

    public MoviesClientGrpc(MoviesApi.MoviesApiClient client,
                            ILogger<MoviesClientGrpc> logger)
    {
        _client = client;
        _logger = logger;
        _metadata = new Metadata
        {
            { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } //just for test purpose
        };
    }

    public async Task<showResponse> GetById(string id)
    {
        var movie = await _client.GetByIdAsync(new IdRequest() { Id = id }, _metadata);
        movie.Data.TryUnpack<showResponse>(out var data);
        return data;
    }

}