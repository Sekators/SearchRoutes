using ProviderOne.Contracts;
using ProviderOne.Models;
using Shared.Models.Providers.ProviderOne;

namespace ProviderOne.Services;

public class DataService : IDataService
{
    private readonly Random _random = new();
    public ProviderOneSearchResponse Generate(ProviderOneSearchRequest request)
    {
        var routeCollection = new List<ProviderOneRoute>();
        var randomRoutesCount = new Random().Next(3, 15);
        
        // Generate response model
        for (var i = 0; i < randomRoutesCount; i++)
        {
            routeCollection.Add(new ProviderOneRoute
            {
                From = request.From,
                To = request.To,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo ?? request.DateFrom.AddHours(_random.Next(1, 5)),
                Price = _random.Next(1, request.MaxPrice is >= 1 ? (int)request.MaxPrice : 1000),
                TimeLimit = request.DateFrom.AddHours(-12),
            });
        }

        return new ProviderOneSearchResponse { Routes = routeCollection.ToArray() };
    }
}