using Shared.Models.Providers.ProviderOne;

namespace ProviderOne.Contracts;

public interface IDataService
{
    ProviderOneSearchResponse Generate(ProviderOneSearchRequest request);
}