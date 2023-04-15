using DadJokesAPI.Models;

namespace DadJokesAPI.Service
{
    public interface IDadJokesApiService
    {
        public Task<JokeDTO> GetRandomJoke();
        public Task<JokesSearchResultsDTO> SearchJokes(string query, int limit);
    }
}
