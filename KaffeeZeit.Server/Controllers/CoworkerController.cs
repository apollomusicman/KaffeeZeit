using KaffeeZeit.Server.Controllers.Dtos;
using KaffeeZeit.Server.Models;
using KaffeeZeit.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace KaffeeZeit.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoworkerController : ControllerBase
    {

        private readonly ILogger<CoworkerController> _logger;
        private CoworkerService _coworkerService = new CoworkerService();

        public CoworkerController(ILogger<CoworkerController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Coworker> Get()
        {
            return _coworkerService.Coworkers;
        }

        [HttpPost]

        public void Post(CreateCoworkerRequest request)
        {
            _coworkerService.AddCoworker(request);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _coworkerService.RemoveCoworker(id);
        }
    }
}
