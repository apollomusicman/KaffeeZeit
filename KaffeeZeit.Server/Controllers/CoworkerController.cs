using System.Net;
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
        private readonly CoworkerService _coworkerService = CoworkerService.Instance;

        public CoworkerController()
        {
        }

        [HttpGet]
        public IEnumerable<Coworker> Get()
        {
            return _coworkerService.Coworkers;
        }

        [HttpPost]

        public HttpResponseMessage Post(CreateCoworkerRequest request)
        {
            try
            {
                _coworkerService.AddCoworker(request);
            }
            catch (InvalidOperationException)
            {
                //TODO add error code or message.
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(Guid id)
        {
            _coworkerService.RemoveCoworker(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
