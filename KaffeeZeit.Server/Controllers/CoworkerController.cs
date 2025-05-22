using System.Net;
using KaffeeZeit.Server.Controllers.Dtos;
using KaffeeZeit.Server.Models;
using KaffeeZeit.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace KaffeeZeit.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CoworkerController : ControllerBase
    {
        private readonly CoworkerService _coworkerService = CoworkerService.Instance;

        public CoworkerController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_coworkerService.Coworkers);
        }

        [HttpPost]
        public IActionResult Post(CreateCoworkerRequest request)
        {
            try
            {
                _coworkerService.AddCoworker(request);
            }
            catch (InvalidOperationException)
            {
                var error = new ErrorResponse
                {
                    ErrorCode = "NameConflictError",
                    ErrorMessage = "The given name is already taken, please choose a diferent  name."
                };
                return Conflict(error);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _coworkerService.RemoveCoworker(id);
            return Ok();
        }
    }
}
