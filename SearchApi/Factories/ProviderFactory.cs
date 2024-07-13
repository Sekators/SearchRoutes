using SearchApi.Contracts;
using SearchApi.Infrastructure;
using SearchApi.Services;

namespace SearchApi.Factories;

public class ProviderFactory(IServiceProvider serviceProvider) : IProviderFactory
{
    public IProviderService? CreateProviderService(string providerName)
    {
        return providerName switch
        {
            Constants.ProviderOne => serviceProvider.GetRequiredService<ProviderOneService>(),
            Constants.ProviderTwo => serviceProvider.GetRequiredService<ProviderTwoService>(),
            _ => null
        };
    }
}