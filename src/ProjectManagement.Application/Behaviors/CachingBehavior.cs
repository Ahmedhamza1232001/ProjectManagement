using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace ProjectManagement.Application.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IMemoryCache _cache;

    public CachingBehavior(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var key = request.GetHashCode().ToString();

        if (_cache.TryGetValue(key, out TResponse? cachedResponse))
        {
            return cachedResponse!;
        }

        var response = await next();
        _cache.Set(key, response, TimeSpan.FromMinutes(5));

        return response;
    }
}
