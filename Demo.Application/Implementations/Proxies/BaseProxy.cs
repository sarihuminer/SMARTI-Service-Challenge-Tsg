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
        protected BaseProxy(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public BaseProxy()
        {
        }

        public BaseProxy(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
           // _httpClient.AddTraceIdentifier(httpContextAccessor);
        }

        public BaseProxy(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, bool isJwtAuthentication)
            : this(httpClient, httpContextAccessor)
        {
            //if (isJwtAuthentication)
            //{
            //    _httpClient.AddJwtAuthorizationHeader(_httpContextAccessor);
            //}
        }

        public IServiceCollection Configure<Tinterface, Timplementation>(IServiceCollection services, string baseUri) where Tinterface : class where Timplementation : class, Tinterface
        {
            services.AddHttpClient<Tinterface, Timplementation>(delegate (HttpClient client)
            {
                client.BaseAddress = new Uri(baseUri);
            });
            return services;
        }

        public IServiceCollection Configure<Tinterface, Timplementation>(IServiceCollection services, string baseUri, bool isDefaultCredentials) where Tinterface : class where Timplementation : class, Tinterface
        {
            IHttpClientBuilder httpClientBuilder = services.AddHttpClient<Tinterface, Timplementation>(delegate (HttpClient client)
            {
                client.BaseAddress = new Uri(baseUri);
            });
            if (isDefaultCredentials)
            {
            //    httpClientBuilder.AddUseDefaultCredentials();
            }

            return services;
        }


        protected async Task<TReturn> GetAsync<TReturn>(string relativeUri)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{_baseUrl}/{relativeUri}");
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
