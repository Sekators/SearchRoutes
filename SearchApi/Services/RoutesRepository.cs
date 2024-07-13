using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SearchApi.Contracts;
using SearchApi.Models.Settings;
using Route = SearchApi.Models.Route;

namespace SearchApi.Services;

public class RoutesRepository(IMemoryCache memoryCache, IOptions<AppSettings> appSettings) : IRoutesRepository
{
    private const string RoutesCacheKey = "ProviderRoutes";

    public IEnumerable<Route> GetAll()
    {
        return memoryCache.Get<HashSet<Route>>(RoutesCacheKey) ?? [];
    }

    public void Add(IEnumerable<Route> routes)
    {
        var cachedRoutes = memoryCache.Get<HashSet<Route>>(RoutesCacheKey) ?? [];
        memoryCache.Set(RoutesCacheKey, new HashSet<Route>(cachedRoutes.Concat(routes)), 
            TimeSpan.FromMinutes(appSettings.Value.CacheExpirationMinutes));
    }
}