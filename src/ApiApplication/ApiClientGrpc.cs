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

            var all = await client.GetAllAsync(new Empty());
            all.Data.TryUnpack<showListResponse>(out var data);
            return data;
        }
    }
}