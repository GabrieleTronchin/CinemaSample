using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.Wrapper;
using ProtoDefinitions;

namespace Movies.Client;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddMoviesClient(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var endpoint =
            configuration.GetSection("MovieGrpc:Endpoint").Value
            ?? throw new MissingFieldException("MovieGrpc:Endpoint");

        services
            .AddGrpcClient<MoviesApi.MoviesApiClient>(
                (services, options) =>
                {
                    options.Address = new Uri(endpoint);
                }
            )
            .ConfigureChannel(o =>
            {
                o.HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            })
            .AddPolicyHandler(PollySettings.DefaultRetryPolicy());

        services.AddTransient<IMoviesClientGrpc, MoviesClientGrpc>();
        return services;
    }
}
