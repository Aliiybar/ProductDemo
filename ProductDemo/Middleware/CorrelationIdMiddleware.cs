// ProductDemo/Middleware/CorrelationIdMiddleware.cs
using System.Diagnostics;

namespace ProductDemo.Middleware;

public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeaderName = "X-Correlation-ID";
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;

    public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get or generate Correlation-ID
        var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault()
            ?? Guid.NewGuid().ToString();

        // Add to response headers
        context.Response.Headers.Append(CorrelationIdHeaderName, correlationId);

        // Store in HttpContext for access throughout the request
        context.Items["CorrelationId"] = correlationId;

        // Add to Activity for distributed tracing
        Activity.Current?.SetTag("CorrelationId", correlationId);

        _logger.LogInformation("Incoming request: {Method} {Path} | Correlation-ID: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            correlationId);

        await _next(context);

        _logger.LogInformation("Outgoing response: {StatusCode} | Correlation-ID: {CorrelationId}",
            context.Response.StatusCode,
            correlationId);
    }
}