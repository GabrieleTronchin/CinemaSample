using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using ProtoDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Client
{
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
            });


            services.AddTransient<IMoviesClientGrpc, MoviesClientGrpc>();
            return services;
        }
    }
}
