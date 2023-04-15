using DadJokesAPI.Models;
using DadJokesAPI.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace DadJokesAPI.Tests
{
    public class DadJokesApiService_SearchJokes_Deserialization
    {
        private IDadJokesApiService _service;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var httpRequestServiceMock = new Mock<IHTTPRequestService>();
            var loggerMock = new Mock<ILogger<DadJokesApiService>>();
            httpRequestServiceMock.Setup(x => x.makeGETRequest("/search?page=1&limit=30&term=bun")).ReturnsAsync("{\"current_page\":1,\"limit\":30,\"next_page\":1,\"previous_page\":1,\"results\":[{\"id\":\"a29pbp4haFd\",\"joke\":\"Where do rabbits go after they get married? On a bunny-moon.\"},{\"id\":\"PfaMusPucFd\",\"joke\":\"What do you do when your bunny gets wet? You get your hare dryer.\"},{\"id\":\"JBs4T79Edpb\",\"joke\":\"Breaking news! Energizer Bunny arrested \\u2013 charged with battery.\"},{\"id\":\"ZvPfFQukVvc\",\"joke\":\"The word queue is ironic. It's just q with a bunch of silent letters waiting in line.\"}],\"search_term\":\"bun\",\"status\":200,\"total_jokes\":4,\"total_pages\":1}");
            _service = new DadJokesApiService(loggerMock.Object, httpRequestServiceMock.Object);
        }

        [Test]
        public async Task DadJokesApiService_Deserialization_Passes()
        {
            // Arrange
            int JOKES_LIMIT = 30;
            List<JokeDTO> expectedJokes = new List<JokeDTO>() {
                new JokeDTO() { Id = "a29pbp4haFd", Joke = "Where do rabbits go after they get married? On a bunny-moon."},
                new JokeDTO() { Id = "PfaMusPucFd", Joke = "What do you do when your bunny gets wet? You get your hare dryer."},
                new JokeDTO() { Id = "JBs4T79Edpb", Joke = "Breaking news! Energizer Bunny arrested – charged with battery."},
                new JokeDTO() { Id = "ZvPfFQukVvc", Joke = "The word queue is ironic. It's just q with a bunch of silent letters waiting in line."},
            };

            JokesSearchResultsDTO expectedJokeSearchResultsDTO = new JokesSearchResultsDTO()
            {
                currentPage = 1,
                limit = JOKES_LIMIT,
                results = expectedJokes,
                searchTerm = "bun",
                totalJokes = expectedJokes.Count(),
                totalPages = 1
            };

            // Act
            JokesSearchResultsDTO actualJokeResults = await _service.SearchJokes("bun", JOKES_LIMIT);

            // Assert
            Assert.That(actualJokeResults, Is.EqualTo(expectedJokeSearchResultsDTO));
        }

        [Test]
        public async Task DadJokesApiService_Deserialization_Fails()
        {
            // Arrange
            int JOKES_LIMIT = 30;
            List<JokeDTO> expectedJokes = new List<JokeDTO>() {
                new JokeDTO() { Id = "a29pbp4haFd", Joke = "Where do rabbits go after they get married? On a bunny-moon."},
                new JokeDTO() { Id = "PfaMusPucFd", Joke = "What do you do when your bunny gets wet? You get your hare dryer."}
            };

            JokesSearchResultsDTO expectedJokeSearchResultsDTO = new JokesSearchResultsDTO()
            {
                currentPage = 1,
                limit = JOKES_LIMIT,
                results = expectedJokes,
                searchTerm = "bun",
                totalJokes = expectedJokes.Count(),
                totalPages = 1
            };

            // Act
            JokesSearchResultsDTO actualJokeResults = await _service.SearchJokes("bun", JOKES_LIMIT);

            // Assert
            Assert.That(actualJokeResults, Is.Not.EqualTo(expectedJokeSearchResultsDTO));
        }
    }
}
