using Microsoft.AspNetCore.Mvc;
using ProviderOne.Contracts;
using ProviderOne.Models;
using Shared.Models.Providers.ProviderOne;

namespace ProviderOne.Controllers;

[ApiController]
[Route("api/v1")]
public class SearchController(IDataService dataService) : ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok(); // Or 'BadRequest()' if service disable
    }

    [HttpPost("search")]
    public IActionResult Search([FromBody] ProviderOneSearchRequest request)
    {
        var response = dataService.Generate(request);
        return Ok(response);
    }
}