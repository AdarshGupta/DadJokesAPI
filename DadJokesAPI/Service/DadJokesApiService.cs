using DadJokesAPI.Controllers;
using DadJokesAPI.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DadJokesAPI.Service
{
    public class DadJokesApiService : IDadJokesApiService
    {
        private static readonly HttpClient httpClient;
        private readonly ILogger<DadJokesApiService> _logger;
        private int MAX_JOKES_PER_PAGE = 30; // Limit set by the "icanhazdadjoke" API service
        private string DAD_JOKE_API_FETCH_ENDPOINT = "/";
        private string DAD_JOKE_API_SEARCH_ENDPOINT = "/search";

        static DadJokesApiService()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/")
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Dad Jokes API (https://adarshgupta.github.io/)");
        }

        public DadJokesApiService(ILogger<DadJokesApiService> logger)
        {
            _logger = logger;
        }

        async Task<JokeDTO> IDadJokesApiService.GetRandomJoke()
        {
            var response = await httpClient.GetAsync(DAD_JOKE_API_FETCH_ENDPOINT);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<JokeDTO>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                return result;
            }
            else
            {
                _logger.LogInformation("Request to \"icanhazdadjoke\" Fetch a random joke API failed!");
                throw new HttpRequestException(response.ReasonPhrase);
            }
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
            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString(DAD_JOKE_API_SEARCH_ENDPOINT, requestQueryParams));

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<JokesSearchResultsDTO>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                return result;
            }
            else
            {
                _logger.LogInformation("Request to \"icanhazdadjoke\" search API failed!");
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
