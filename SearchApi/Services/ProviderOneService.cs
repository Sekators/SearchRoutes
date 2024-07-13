using AutoMapper;
using ProviderOne.Models;
using SearchApi.Contracts;
using SearchApi.Models;
using Shared.Models.Providers.ProviderOne;
using Route = SearchApi.Models.Route;

namespace SearchApi.Services;

public class ProviderOneService(IHttpClientFactory factory, IMapper mapper) : IProviderService
{
    private const string BaseUrl = "http://localhost:5001/api/v1/"; // instead of 'http://provider-one/api/v1/search'

    public async Task<IEnumerable<Route>> GetProviderRoutesAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        var httpClient = factory.CreateClient();
        httpClient.BaseAddress = new Uri(BaseUrl);

        // Check if provider available
        var checkResponse = await httpClient.GetAsync("ping", cancellationToken);
        if (!checkResponse.IsSuccessStatusCode) return [];
            
        // Search data
        var parsedRequest = mapper.Map<ProviderOneSearchRequest>(request);
        var response = await httpClient.PostAsJsonAsync("search", parsedRequest, cancellationToken);
        if (!response.IsSuccessStatusCode) return [];

        var result = await response.Content.ReadFromJsonAsync<ProviderOneSearchResponse>(cancellationToken);
        return result != null && result.Routes.Length != 0 
            ? mapper.Map<Route[]>(result.Routes) : [];
    }
}