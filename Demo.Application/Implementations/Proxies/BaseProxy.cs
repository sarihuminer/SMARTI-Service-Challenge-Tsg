using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Implementations.Proxies
{
    public abstract class BaseProxy
    {
        private readonly string _baseUrl;
        protected readonly HttpClient _httpClient;

        protected readonly IHttpContextAccessor _httpContextAccessor;



        private bool UseOldSerialize;
        protected BaseProxy(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _baseUrl = Path.Combine(_httpClient.BaseAddress.AbsoluteUri); ;
        }

        public BaseProxy()
        {
        }

        public BaseProxy(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, bool isJwtAuthentication)
            : this(httpClient, httpContextAccessor)
        {
            //if (isJwtAuthentication)
            //{
            //    _httpClient.AddJwtAuthorizationHeader(_httpContextAccessor);
            //}
        }

        public IServiceCollection Configure<Tinterface, Timplementation>(IServiceCollection services, string baseUrl) where Tinterface : class where Timplementation : class, Tinterface
        {
            services.AddHttpClient<Tinterface, Timplementation>(delegate (HttpClient client)
            {
                client.BaseAddress = new Uri(baseUrl);
            });
            return services;
        }

        public IServiceCollection Configure<Tinterface, Timplementation>(IServiceCollection services, string baseUrl, bool isDefaultCredentials) where Tinterface : class where Timplementation : class, Tinterface
        {
            IHttpClientBuilder httpClientBuilder = services.AddHttpClient<Tinterface, Timplementation>(delegate (HttpClient client)
            {
                client.BaseAddress = new Uri(baseUrl);
            });
            if (isDefaultCredentials)
            {
            //    httpClientBuilder.AddUseDefaultCredentials();
            }

            return services;
        }


        protected async Task<TReturn> GetAsync<TReturn>(string relativeUri)
        {
            //string url = Path.Combine(_httpClient.BaseAddress.AbsoluteUri, relativeUri);
            string fetchUri = new Uri(new Uri(_httpClient.BaseAddress.AbsoluteUri), relativeUri).ToString();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MyAppName/1.0");
            HttpResponseMessage res = await _httpClient.GetAsync(fetchUri);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>();
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        protected async Task<TReturn> PostAsync<TReturn, TRequest>(string relativeUri, TRequest request)
        {
            HttpResponseMessage res = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{relativeUri}", request);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>();
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        public string GetRequestUri(string endpoint)
        {
            //https://api.github.com/search/repositories?q=YOUR_SEARCH_KEYWORD
            return new Uri(new Uri(_baseUrl), endpoint).ToString();
        }
    }


}
