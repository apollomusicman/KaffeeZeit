namespace KaffeeZeit.Server.Controllers.Dtos
{
    public record CreateCoworkerRequest
    {
        public required string Name { get; init; }
        public required decimal FavoriteDrinkCost { get; init; }
    }
}