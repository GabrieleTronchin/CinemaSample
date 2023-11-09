using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using ShowTimeProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Client
{
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
            });


            services.AddTransient<ICinemaClientGrpc, CinemaClientGrpc>();
            return services;
        }
    }
}
