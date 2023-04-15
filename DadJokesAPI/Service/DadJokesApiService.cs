using DadJokesAPI.Controllers;
using DadJokesAPI.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DadJokesAPI.Service
{
    public class DadJokesApiService : IDadJokesApiService
    {
        private readonly IHTTPRequestService _httpRequestService;
        private readonly ILogger<DadJokesApiService> _logger;
        private int MAX_JOKES_PER_PAGE = 30; // Limit set by the "icanhazdadjoke" API service
        private string DAD_JOKE_API_FETCH_ENDPOINT = "/";
        private string DAD_JOKE_API_SEARCH_ENDPOINT = "/search";

        public DadJokesApiService(ILogger<DadJokesApiService> logger, IHTTPRequestService httpRequestService)
        {
            _logger = logger;
            _httpRequestService = httpRequestService;
        }

        async Task<JokeDTO> IDadJokesApiService.GetRandomJoke()
        {
            var response = await _httpRequestService.makeGETRequest(DAD_JOKE_API_FETCH_ENDPOINT);
            var result = JsonSerializer.Deserialize<JokeDTO>(response,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return result;
        }

        async Task<JokesSearchResultsDTO> IDadJokesApiService.SearchJokes(string query, int limit)
        {

            // Set query params required by the "icanhazdadjoke" API to provide joke search service
            var requestQueryParams = new Dictionary<string, string>
            {
                {"page", "1" },
                {"limit", Math.Min(limit, MAX_JOKES_PER_PAGE).ToString() },
                {"term", query }
            };
            string requestUri = QueryHelpers.AddQueryString(DAD_JOKE_API_SEARCH_ENDPOINT, requestQueryParams);
            var response = await _httpRequestService.makeGETRequest(requestUri);
            var result = JsonSerializer.Deserialize<JokesSearchResultsDTO>(response,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return result;
        }
    }
}
