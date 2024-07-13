using SearchApi.Contracts;
using SearchApi.Infrastructure;
using SearchApi.Models;
using Route = SearchApi.Models.Route;

namespace SearchApi.Services;

public class SearchService(IProviderFactory providerFactory, IRoutesRepository routesRepository) : ISearchService
{
    private readonly string[] _providers = [Constants.ProviderOne, Constants.ProviderTwo];
    public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        // Get data from cache
        if (request.Filters is { OnlyCached: true })
            return AggregateRoutes(FilterRoutes(routesRepository.GetAll(), request.Filters));
        
        // Start tasks for get data
        var tasks = new Dictionary<string, Task<IEnumerable<Route>>>();
        foreach (var provider in _providers)
        {
            var providerService = providerFactory.CreateProviderService(provider);
            if (providerService == null) continue;

            tasks[provider] = providerService.GetProviderRoutesAsync(request, cancellationToken);
        }

        // Wait all tasks and get result
        var results = await Task.WhenAll(tasks.Values);
        var allRoutes = results.SelectMany(r => r).ToList();
        
        // Add to cache
        routesRepository.Add(allRoutes);

        return request.Filters != null 
            ? AggregateRoutes(FilterRoutes(allRoutes, request.Filters)) 
            : AggregateRoutes(allRoutes);
    }

    public Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(true); // false for 'BadRequest'
    }

    private IEnumerable<Route> FilterRoutes(IEnumerable<Route> routes, SearchFilters filters)
    {
        return routes.Where(w => 
            (!filters.DestinationDateTime.HasValue || w.DestinationDateTime == filters.DestinationDateTime) &&
            (!filters.MaxPrice.HasValue || w.Price <= filters.MaxPrice.Value) &&
            (!filters.MinTimeLimit.HasValue || w.TimeLimit >= filters.MinTimeLimit));
    }

    private SearchResponse AggregateRoutes(IEnumerable<Route> routes)
    {
        return !routes.Any() ? new SearchResponse() : new SearchResponse
        {
            Routes = routes.ToArray(),
            MinPrice = routes.Min(m => m.Price),
            MaxPrice = routes.Max(m => m.Price),
            MinMinutesRoute = routes.Min(m => (int)Math.Round(m.DestinationDateTime.Subtract(m.OriginDateTime).TotalMinutes)),
            MaxMinutesRoute = routes.Max(m => (int)Math.Round(m.DestinationDateTime.Subtract(m.OriginDateTime).TotalMinutes)),
        };
    }
}