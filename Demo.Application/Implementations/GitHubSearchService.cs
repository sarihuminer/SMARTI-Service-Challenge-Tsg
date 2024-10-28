using Demo.Application.DTOs;
using Demo.Application.Implementations.Proxies;
using Demo.Application.Interfaces;
using Demo.Application.Interfaces.Proxies;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Implementations
{
    public class GitHubSearchService : IGitHubSearchService
    {
        private readonly IGitHubProxy _gitHubProxy;
        private readonly IPrioritySettingsService _prioritySettingsService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<GitHubProxy> _logger;
        public GitHubSearchService(IGitHubProxy gitHubProxy, HttpClient httpClient, ILogger<GitHubProxy> logger)
        {
            _gitHubProxy = gitHubProxy;
            _httpClient = httpClient;
            _logger = logger;
        }

        //public Task<object> SearchGithubRepo(string text)
        //{
        //    var url = _gitHubProxy.GetRequestUri($"/search/repositories?q=YOUR_SEARCH_KEYWORD={text}");
        //    return null;
        //}

        public async Task<GitHubSearchResponse> SearchRepositoriesAsync(string text)
        {
            try
            {
                var r = await _gitHubProxy.SearchRepositoriesAsync(text);
                return r;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while searching GitHub repositories: {ex.Message}");
                throw;
            }
        }
    }
}
