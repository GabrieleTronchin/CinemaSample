﻿using System.Net;
using Grpc.Core;

namespace Polly.Wrapper
{
    public static class StatusManager
    {
        public static StatusCode GetStatusCode(HttpResponseMessage response)
        {
            var headers = response.Headers;

            if (!headers.Contains("grpc-status") && response.StatusCode == HttpStatusCode.OK)
                return StatusCode.OK;

            if (headers.Contains("grpc-status"))
                return (StatusCode)int.Parse(headers.GetValues("grpc-status").First());

            return StatusCode.Unknown;
        }
    }
}
