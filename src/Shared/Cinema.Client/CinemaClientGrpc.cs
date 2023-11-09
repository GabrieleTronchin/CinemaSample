using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using ShowTimeProto;

namespace Cinema.Client
{
    public class CinemaClientGrpc : ICinemaClientGrpc
    {
        private readonly ShowTimeApi.ShowTimeApiClient _client;
        private readonly ILogger<CinemaClientGrpc> _logger;
        private readonly Metadata _metadata;

        public CinemaClientGrpc(ShowTimeApi.ShowTimeApiClient client,
                                ILogger<CinemaClientGrpc> logger)
        {
            _client = client;
            _logger = logger;
            _metadata = new Metadata
            {
                { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } //just for test purpose
            };
        }
        //TODO Fix async
        public async Task<responseModel> CreateShowTime(ShowtimeCreationRequest request)
        {
           return await  _client.CreateShowTimeAsync(request, _metadata);
        }

    }
}