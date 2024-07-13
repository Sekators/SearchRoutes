using ProviderOne.Models;

namespace Shared.Models.Providers.ProviderOne;

public class ProviderOneSearchResponse
{
    // Mandatory
    // Array of routes
    public ProviderOneRoute[] Routes { get; set; }
}