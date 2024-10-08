using System.Diagnostics;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger
        // ICurrentUserService currentUserService,
        // IIdentityService identityService
        )
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer = new Stopwatch();
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            // var userId = _currentUserService.UserId ?? string.Empty;
            var userName = string.Empty;

            // if (!string.IsNullOrEmpty(userId))
            // {
            //     userName = await _identityService.GetUserNameAsync(userId);
            // }

            _logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, "Framework", userName, request);
        }

        return response;
    }
}
