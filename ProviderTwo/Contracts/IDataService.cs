using ProviderTwo.Models;

namespace ProviderTwo.Contracts;

public interface IDataService
{
    ProviderTwoSearchResponse Generate(ProviderTwoSearchRequest request);
}