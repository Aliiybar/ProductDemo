namespace ProductDemo.Handlers;

public class CorrelationIdDelegatingHandler : DelegatingHandler
{
    private const string CorrelationIdHeaderName = "X-Correlation-ID";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CorrelationIdDelegatingHandler> _logger;

    public CorrelationIdDelegatingHandler(
        IHttpContextAccessor httpContextAccessor,
        ILogger<CorrelationIdDelegatingHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString();

        if (!string.IsNullOrEmpty(correlationId))
        {
            request.Headers.Add(CorrelationIdHeaderName, correlationId);
            
            _logger.LogInformation(
                "Outgoing HTTP request: {Method} {Uri} | Correlation-ID: {CorrelationId}",
                request.Method,
                request.RequestUri,
                correlationId);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (!string.IsNullOrEmpty(correlationId))
        {
            _logger.LogInformation(
                "Incoming HTTP response: {StatusCode} | Correlation-ID: {CorrelationId}",
                response.StatusCode,
                correlationId);
        }

        return response;
    }
}