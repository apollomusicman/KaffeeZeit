using KaffeeZeit.Server.Controllers.Dtos;
using KaffeeZeit.Server.Models;

namespace KaffeeZeit.Server.Service
{
    public class CoworkerService
    {
        // TODO Ideally use dependency injection to manage lifecycle, come back to that if there is time.
        private static readonly Lazy<CoworkerService> _instance = new(() => new CoworkerService()); 
        // TODO time permitting put this in some lightweight db i.e. reddis, or mongodb. 
        private readonly Dictionary<Guid, Coworker> _coworkers = [];

        public static CoworkerService Instance { get { return _instance.Value; } }
        public List<Coworker> Coworkers { get { return [.. _coworkers.Values]; } }

        private CoworkerService() { }

        public void AddCoworker(CreateCoworkerRequest request)
        {
            if (_coworkers.Any(coworker => coworker.Value.Name == request.Name))
            {
                throw new InvalidOperationException("Cannot add a coworker with duplicate name.");
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