namespace KaffeeZeit.Server.Models
{

    public record Coworker
    {
        private Guid _id = Guid.NewGuid();
        

        public required string Name { get; init; }

        public Guid Id { get { return _id; } }

        public required decimal FavoriteDrinkCost { get; init; }
    }
}