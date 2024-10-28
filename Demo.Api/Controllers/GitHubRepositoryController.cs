using Demo.Application.Implementations;
using Demo.Application.Interfaces;
using Demo.Domain.Model.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubRepositoryController : ControllerBase
    {
        private readonly IGitHubSearchService _gitHubService;

        public GitHubRepositoryController(IGitHubSearchService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRepositories([FromQuery] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return BadRequest("Keyword must be provided.");
            }

            var repositories = await _gitHubService.SearchRepositoriesAsync(text);
            return Ok(repositories);
        }
    }
}
