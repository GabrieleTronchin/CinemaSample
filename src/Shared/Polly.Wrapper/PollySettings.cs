using Grpc.Core;
using Movies.Aggregator.Domain;
using System.Net;

namespace Polly.Wrapper
{
    public static class PollySettings
    {
        private static HttpStatusCode[] serverErrors = new HttpStatusCode[] {
                HttpStatusCode.BadGateway,
                HttpStatusCode.GatewayTimeout,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.InternalServerError,
                HttpStatusCode.TooManyRequests,
                HttpStatusCode.RequestTimeout
            };

        private static StatusCode[] gRpcErrors = new StatusCode[] {
                StatusCode.DeadlineExceeded,
                StatusCode.Internal,
                StatusCode.NotFound,
                StatusCode.ResourceExhausted,
                StatusCode.Unavailable,
                StatusCode.Unknown
            };


        public static Func<HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> DefaultRetryPolicy()
        {
            return (request) =>
            {
                return Policy.HandleResult<HttpResponseMessage>(r =>
                {

                    var grpcStatus = StatusManager.GetStatusCode(r);
                    var httpStatusCode = r.StatusCode;
                    return (grpcStatus == null && serverErrors.Contains(httpStatusCode)) || // if the server send an error before gRPC pipeline
                           (httpStatusCode == HttpStatusCode.OK && gRpcErrors.Contains(grpcStatus.Value)); // if gRPC pipeline handled the request (gRPC always answers OK)
                })
                .WaitAndRetryAsync(3, (input) => TimeSpan.FromSeconds(3 + input), (result, timeSpan, retryCount, context) =>
                {
                    var grpcStatus = StatusManager.GetStatusCode(result.Result);
                });
            };
        }


    }
}