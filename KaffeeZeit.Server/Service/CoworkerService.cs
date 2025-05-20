using KaffeeZeit.Server.Controllers.Dtos;
using KaffeeZeit.Server.Models;

namespace KaffeeZeit.Server.Service
{
    public class CoworkerService
    {
        // TODO time permitting put this in some lightweight db i.e. reddis, or mongodb. 
        private readonly Dictionary<Guid, Coworker> _coworkers = [];

        public List<Coworker> Coworkers { get { return [.. _coworkers.Values]; } }

        public CoworkerService() { }

        public void AddCoworker(CreateCoworkerRequest request)
        {
            if (_coworkers.Any(coworker => coworker.Value.Name == request.Name))
            {
                //TODO add error handling. Names probably should be unique to avoid confusion of who owes what.
                return;
            }
            var coworker = new Coworker { Name = request.Name };

            _coworkers.Add(coworker.Id, coworker);
        }

        public void RemoveCoworker(Guid id)
        {
            _coworkers.Remove(id);
        }
    }
}