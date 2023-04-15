using DadJokesAPI.Controllers;
using DadJokesAPI.Models;
using DadJokesAPI.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace DadJokesAPI.Tests
{
    public class DadJokesController_SearchJokes
    {
        private DadJokesController _controller;

        [SetUp]
        public void Setup()
        {
            // Arrange
            int JOKES_LIMIT = 30;
            var loggerMock = new Mock<ILogger<DadJokesController>>();
            var _serviceMock = new Mock<IDadJokesApiService>();
            List<JokeDTO> mockJokes = new List<JokeDTO>() {
                new JokeDTO() { Id = "a29pbp4haFd", Joke = "Where do rabbits go after they get married? On a bunny-moon."},
                new JokeDTO() { Id = "PfaMusPucFd", Joke = "What do you do when your bunny gets wet? You get your hare dryer."},
                new JokeDTO() { Id = "JBs4T79Edpb", Joke = "Breaking news! Energizer Bunny arrested – charged with battery."},
                new JokeDTO() { Id = "ZvPfFQukVvc", Joke = "The word queue is ironic. It's just q with a bunch of silent letters waiting in line."},
            };
            JokesSearchResultsDTO mockJokeResults = new JokesSearchResultsDTO()
            {
                currentPage = 1,
                limit = JOKES_LIMIT,
                results = mockJokes,
                searchTerm = "bun",
                totalJokes = mockJokes.Count(),
                totalPages = 1
            };
            _serviceMock.Setup(x => x.SearchJokes("bun", JOKES_LIMIT)).ReturnsAsync(mockJokeResults);
            _controller = new DadJokesController(_serviceMock.Object, loggerMock.Object);
        }

        [Test]
        public async Task DadJokesController_SearchJokes_Matches()
        {
            // Arrange
            List<JokeDTO> expectedJokes = new List<JokeDTO>() {
                new JokeDTO() { Id = "a29pbp4haFd", Joke = "Where do rabbits go after they get married? On a bunny-moon."},
                new JokeDTO() { Id = "PfaMusPucFd", Joke = "What do you do when your bunny gets wet? You get your hare dryer."},
                new JokeDTO() { Id = "JBs4T79Edpb", Joke = "Breaking news! Energizer Bunny arrested – charged with battery."},
                new JokeDTO() { Id = "ZvPfFQukVvc", Joke = "The word queue is ironic. It's just q with a bunch of silent letters waiting in line."},
            };

            // Act
            List<JokeDTO> searchedJokes = await _controller.GetDadJokes("bun");

            // Assert
            Assert.That(searchedJokes, Is.EqualTo(expectedJokes));
        }

        [Test]
        public async Task DadJokesController_SearchJokes_DoesNotMatch()
        {
            // Arrange
            List<JokeDTO> expectedJokes = new List<JokeDTO>() {
                new JokeDTO() { Id = "a29pbp4haFd", Joke = "Where do rabbits go after they get married? On a bunny-moon."},
                new JokeDTO() { Id = "ZvPfFQukVvc", Joke = "The word queue is ironic. It's just q with a bunch of silent letters waiting in line."},
            };

            // Act
            List<JokeDTO> searchedJokes = await _controller.GetDadJokes("bun");

            // Assert
            Assert.That(searchedJokes, Is.Not.EqualTo(expectedJokes));
        }
    }
}
