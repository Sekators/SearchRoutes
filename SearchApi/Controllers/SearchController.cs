using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SearchApi.Contracts;
using SearchApi.Models;
using SearchApi.Models.Settings;

namespace SearchApi.Controllers;

[ApiController]
[Route("api/v1")]
public class SearchController(ISearchService searchService, IOptions<AppSettings> appSettings) : ControllerBase
{
    private readonly CancellationTokenSource _tokenSource = new();
    
    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        return await searchService.IsAvailableAsync(_tokenSource.Token) 
            ? Ok() 
            : BadRequest();
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchRequest request)
    {
        // Implementation timeout after one minute
        //_tokenSource.CancelAfter(1000 * appSettings.Value.TimeoutSeconds);
        
        var response = await searchService.SearchAsync(request, _tokenSource.Token);
        return Ok(response);
    }
}