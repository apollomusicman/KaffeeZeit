using KaffeeZeit.Server.Controllers.Dtos;
using KaffeeZeit.Server.Models;

namespace KaffeeZeit.Server.Service
{
    //TODO this might be redundant with TabService.
    public class CoworkerService
    {
        // TODO Ideally use dependency injection to manage lifecycle, come back to that if there is time.
        private static readonly Lazy<CoworkerService> _instance = new(() => new CoworkerService()); 
        private readonly TabService _tabService = TabService.Instance;

        public static CoworkerService Instance { get { return _instance.Value; } }
        public List<Coworker> Coworkers { get { return [.. _tabService.Tabs.Select(t => t.Coworker)]; } }

        private CoworkerService() { }

        public void AddCoworker(CreateCoworkerRequest request)
        {
            if (_tabService.Tabs.Any(t => string.Equals(t.Coworker.Name, request.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Cannot add a coworker with duplicate name.");
            }
            var coworker = new Coworker { Name = request.Name, FavoriteDrinkCost = request.FavoriteDrinkCost };
            _tabService.AddTab(coworker);
        }

        public void RemoveCoworker(Guid id)
        {
            _tabService.RemoveTabByCoworkerId(id);
        }
    }
}