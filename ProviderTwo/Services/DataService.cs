using ProviderTwo.Contracts;
using ProviderTwo.Models;

namespace ProviderTwo.Services;

public class DataService : IDataService
{
    private readonly Random _random = new();
    
    public ProviderTwoSearchResponse Generate(ProviderTwoSearchRequest request)
    {
        var routeCollection = new List<ProviderTwoRoute>();
        var randomRoutesCount = new Random().Next(3, 15);
        
        // Generate response model
        for (var i = 0; i < randomRoutesCount; i++)
        {
            routeCollection.Add(new ProviderTwoRoute
            {
                Departure = new ProviderTwoPoint {Point = request.Departure, Date = request.DepartureDate},
                Arrival = new ProviderTwoPoint {Point = request.Arrival, Date = request.DepartureDate.AddHours(_random.Next(1, 5))},
                Price = _random.Next(100, 1000),
                TimeLimit = request.DepartureDate.AddHours(-12),
            });
        }

        return new ProviderTwoSearchResponse { Routes = routeCollection.ToArray() };
    }
}