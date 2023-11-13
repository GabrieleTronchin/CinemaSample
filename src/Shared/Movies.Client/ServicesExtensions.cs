using Microsoft.Extensions.DependencyInjection;
using Polly.Wrapper;
using ProtoDefinitions;

namespace Movies.Client;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddMoviesClient(this IServiceCollection services)
    {
        services.AddGrpcClient<MoviesApi.MoviesApiClient>((services, options) =>
        {
            options.Address = new Uri("https://localhost:7443"); //TODO move to app settings
        }).ConfigureChannel(o =>
        {
            o.HttpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
        }).AddPolicyHandler(PollySettings.DefaultRetryPolicy());


        services.AddTransient<IMoviesClientGrpc, MoviesClientGrpc>();
        return services;
    }
}
