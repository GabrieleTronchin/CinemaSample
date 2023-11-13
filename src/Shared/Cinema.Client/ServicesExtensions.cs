using Microsoft.Extensions.DependencyInjection;
using Polly.Wrapper;
using ShowTimeProto;

namespace Cinema.Client;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddCinemaClient(this IServiceCollection services)
    {
        services.AddGrpcClient<ShowTimeApi.ShowTimeApiClient>((services, options) =>
        {
            options.Address = new Uri("https://localhost:7629"); //TODO move to app settings
        }).ConfigureChannel(o =>
        {
            o.HttpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
        }).AddPolicyHandler(PollySettings.DefaultRetryPolicy());


        services.AddTransient<ICinemaClientGrpc, CinemaClientGrpc>();
        return services;
    }
}
