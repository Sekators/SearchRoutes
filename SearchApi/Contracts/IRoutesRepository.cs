namespace SearchApi.Contracts;
using Route = SearchApi.Models.Route;

public interface IRoutesRepository
{
    IEnumerable<Route> GetAll();
    void Add(IEnumerable<Route> routes);
}