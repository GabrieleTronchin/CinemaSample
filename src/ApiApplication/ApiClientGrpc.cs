using Grpc.Core;
using Grpc.Net.Client;
using ProtoDefinitions;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication
{
    public class ApiClientGrpc
    {
        public async Task<showListResponse> GetAll()
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel =
                GrpcChannel.ForAddress("https://localhost:7443", new GrpcChannelOptions()
                {
                    HttpHandler = httpHandler
                });
            var client = new MoviesApi.MoviesApiClient(channel);

            var metadata = new Metadata
            {
                { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" }
            };

            var all = await client.GetAllAsync(new Empty(), metadata);
            all.Data.TryUnpack<showListResponse>(out var data);
            return data;
        }
    }
}