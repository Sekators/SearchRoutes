using SearchApi.Models;
using Route = SearchApi.Models.Route;

namespace SearchApi.Contracts;

public interface IProviderService
{
    Task<IEnumerable<Route>> GetProviderRoutesAsync(SearchRequest request, CancellationToken cancellationToken);
}