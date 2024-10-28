using Demo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces.Proxies
{
    public interface IGitHubProxy
    {
        //  Task<List<GitHubRepositoryDto>> SearchRepositoriesAsync(string keyword);

         string GetRequestUri(string endpoint);
    }
}
