using Microsoft.AspNetCore.Mvc;
using ProviderTwo.Contracts;
using ProviderTwo.Models;

namespace ProviderTwo.Controllers;

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
    public IActionResult Search([FromBody] ProviderTwoSearchRequest request)
    {
        var response = dataService.Generate(request);
        return Ok(response);
    }
}