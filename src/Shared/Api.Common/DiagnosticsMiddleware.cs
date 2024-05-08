using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Common;

public class DiagnosticsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DiagnosticsMiddleware> _logger;

    public DiagnosticsMiddleware(ILogger<DiagnosticsMiddleware> logger, RequestDelegate next)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        Stopwatch sw = Stopwatch.StartNew();

        await _next(context);

        sw.Stop();

        _logger.LogInformation(
            "Method execution complete.Method:{Method} - {Path} - ElapsedTime:{ElapsedTime} ms.",
            context.Request.Method,
            context.Request.Path.Value,
            sw.ElapsedMilliseconds
        );
    }
}
