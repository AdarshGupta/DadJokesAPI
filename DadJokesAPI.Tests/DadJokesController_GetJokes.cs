using DadJokesAPI.Controllers;
using DadJokesAPI.Models;
using DadJokesAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJokesAPI.Tests
{    
    public class DadJokesController_GetJokes
    {
        private DadJokesController _controller;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DadJokesController>>();
            var _serviceMock = new Mock<IDadJokesApiService>();
            var mockJoke = new JokeDTO() { Id = "1", Joke = "What a joke!" };            
            _serviceMock.Setup(x => x.GetRandomJoke()).ReturnsAsync(mockJoke);

            _controller = new DadJokesController(_serviceMock.Object, loggerMock.Object);
        }

        [Test]
        public async Task DadJokesController_GetJokes_Matches()
        {
            // Arrange
            
            List<JokeDTO> expectedJoke = new List<JokeDTO>(){
                new JokeDTO()
                {
                    Id = "1",
                    Joke = "What a joke!"
                }
            };

            // Act
            List<JokeDTO> randomJoke = await _controller.GetDadJokes(null);

            // Assert
            Assert.That(randomJoke, Is.EqualTo(expectedJoke));
        }

        [Test]
        public async Task DadJokesController_GetJokes_DoesNotMatch()
        {
            // Arrange

            List<JokeDTO> expectedJoke = new List<JokeDTO>(){
                new JokeDTO()
                {
                    Id = "123",
                    Joke = "This test fails!"
                }
            };

            // Act
            List<JokeDTO> randomJoke = await _controller.GetDadJokes(null);

            // Assert
            Assert.That(randomJoke, Is.Not.EqualTo(expectedJoke));
        }
    }
}
