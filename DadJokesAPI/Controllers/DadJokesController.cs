using DadJokesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DadJokesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DadJokesController : ControllerBase
    {

        private readonly ILogger<DadJokesController> _logger;

        public DadJokesController(ILogger<DadJokesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDadJoke")]
        public JokeDTO Get()
        {
        }
    }
}