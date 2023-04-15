using DadJokesAPI.Models;
using DadJokesAPI.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace DadJokesAPI.Tests
{
    public class DadJokesApiService_FetchesAJoke_Deserialization
    {
        private IDadJokesApiService _service;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var httpRequestServiceMock = new Mock<IHTTPRequestService>();
            var loggerMock = new Mock<ILogger<DadJokesApiService>>();
            httpRequestServiceMock.Setup(x => x.makeGETRequest("/")).ReturnsAsync("{\"id\":\"123\",\"joke\":\"What kind of dog lives in a particle accelerator? A Fermilabrador Retriever.\",\"status\":200}");
            _service = new DadJokesApiService(loggerMock.Object, httpRequestServiceMock.Object);
        }

        [Test]
        public async Task DadJokesApiService_Deserialization_Passes()
        {
            // Arrange
            JokeDTO expectedJokeDTO = new JokeDTO()
            {
                Id = "123",
                Joke = "What kind of dog lives in a particle accelerator? A Fermilabrador Retriever."
            };

            // Act
            JokeDTO randomJoke = await _service.GetRandomJoke();

            // Assert
            Assert.That(randomJoke, Is.EqualTo(expectedJokeDTO));
        }

        [Test]
        public async Task DadJokesApiService_Deserialization_Fails()
        {
            // Arrange
            JokeDTO expectedJokeDTO = new JokeDTO()
            {
                Id = "123",
                Joke = "This test passes no mater the response."
            };

            // Act
            JokeDTO randomJoke = await _service.GetRandomJoke();

            // Assert
            Assert.That(randomJoke, Is.Not.EqualTo(expectedJokeDTO));
        }
    }
}