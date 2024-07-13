using AutoMapper;
using ProviderTwo.Models;
using SearchApi.Contracts;
using SearchApi.Models;
using Route = SearchApi.Models.Route;

namespace SearchApi.Services;

public class ProviderTwoService(IHttpClientFactory factory, IMapper mapper) : IProviderService
{
    private const string BaseUrl = "http://localhost:5002/api/v1/"; // instead of 'http://provider-two/api/v1/search'
    
    public async Task<IEnumerable<Route>> GetProviderRoutesAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        var httpClient = factory.CreateClient();
        httpClient.BaseAddress = new Uri(BaseUrl);
        
        // Check if provider available
        var checkResponse = await httpClient.GetAsync("ping", cancellationToken);
        if (!checkResponse.IsSuccessStatusCode) return [];
            
        // Search data
        var parsedRequest = mapper.Map<ProviderTwoSearchRequest>(request);
        var response = await httpClient.PostAsJsonAsync("search", parsedRequest, cancellationToken);
        if (!response.IsSuccessStatusCode) return [];

        var result = await response.Content.ReadFromJsonAsync<ProviderTwoSearchResponse>(cancellationToken);
        return result != null && result.Routes.Length != 0 
            ? mapper.Map<Route[]>(result.Routes) : [];
    }
}