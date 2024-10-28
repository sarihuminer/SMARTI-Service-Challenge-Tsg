using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Demo.Application.DTOs;
using Demo.Application.Interfaces.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;
namespace Demo.Application.Implementations.Proxies
{
    public class GitHubProxy : BaseProxy, IGitHubProxy
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GitHubProxy> _logger;
        private readonly string _baseUrl;
        private readonly string _className;

        public GitHubProxy() : base() { }

        public GitHubProxy(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<GitHubProxy> logger)
        : base(httpClient, httpContextAccessor)
        {
            _httpClient = httpClient;
            _logger = logger;
            //can be removed because of program.cs

            //// Derive the name of the class, e.g., "GitHubProxy"
            //_className = nameof(GitHubProxy);

            //// Look up the route that matches "IGitHubProxy"
            //var route = configuration.GetSection("routing")
            //                         .GetChildren()
            //                         .FirstOrDefault(r => r.GetValue<string>("name") == $"I{_className}");

            //if (route != null)
            //{
            //    _baseUrl = route.GetValue<string>("BaseUrl");
            //    _httpClient.BaseAddress = new Uri(_baseUrl); // Set the base address
            //}
            //else
            //{
            //    _logger.LogError($"Route configuration for I{_className} not found.");
            //    throw new Exception($"Route configuration for I{_className} not found.");
            //}
        }
        public IServiceCollection Configure(IServiceCollection services, string baseUrl)
        {
            return Configure<IGitHubProxy, GitHubProxy>(services, baseUrl);
        }

        public async Task<GitHubSearchResponse> SearchRepositoriesAsync(string text)
        {
            try
            {
                var response = await GetAsync<GitHubSearchResponse>($"/search/repositories?q={text}");

                if (response != null )
                {
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while searching GitHub repositories: {ex.Message}");
                throw;
            }
        }

    }
}
