using DadJokesAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DadJokesAPI.Service
{
    public class HTTPRequestService : IHTTPRequestService
    {
        private static readonly HttpClient httpClient;
        private readonly ILogger<HTTPRequestService> _logger;

        static HTTPRequestService()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/")
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Dad Jokes API (https://adarshgupta.github.io/)");
        }

        public HTTPRequestService(ILogger<HTTPRequestService> logger)
        {
            _logger = logger;
        }
        async Task<string> IHTTPRequestService.makeGETRequest(string relativeUri)
        {
            _logger.LogInformation("Relative Uri: " + relativeUri);
            var response = await httpClient.GetAsync(relativeUri);            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                _logger.LogError($"GET Request to {relativeUri} failed!");
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
