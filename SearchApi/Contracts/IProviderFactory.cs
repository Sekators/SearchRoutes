namespace SearchApi.Contracts;

public interface IProviderFactory
{
    IProviderService? CreateProviderService(string providerName);
}