using DadJokesAPI.Models;
using DadJokesAPI.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DadJokesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DadJokesController : ControllerBase
    {

        private readonly ILogger<DadJokesController> _logger;
        private readonly IDadJokesApiService _dadJokesApiService;

        public DadJokesController(IDadJokesApiService dadJokesApiService, ILogger<DadJokesController> logger)
        {
            _dadJokesApiService = dadJokesApiService;
            _logger = logger;
        }

        [EnableCors("FrontendAccessPolicy")]
        [HttpGet(Name = "GetDadJokes")]
        public async Task<List<JokeDTO>> GetDadJokes([FromQuery]string? queryWord)
        {

            if(queryWord == null)
            {
                JokeDTO randomJoke = await _dadJokesApiService.GetRandomJoke();
                List<JokeDTO> jokes = new List<JokeDTO>() { randomJoke };
                return jokes;
            }
            else
            {
                int maxJokesRequired = 30;
                JokesSearchResultsDTO searchedJokes = await _dadJokesApiService.SearchJokes(queryWord, maxJokesRequired);
                _logger.LogInformation("[Search] Joke results found: " + searchedJokes.results.Count().ToString());
                return searchedJokes.results;
            }
        }
    }
}