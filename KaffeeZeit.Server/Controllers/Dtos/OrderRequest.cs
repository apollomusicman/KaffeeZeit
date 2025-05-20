namespace KaffeeZeit.Server.Controllers.Dtos
{
    public record OrderRequest
    {
        public required List<CoworkerOrder> Orders { get; init; }
        public required int Revision { get; init; }
    }

    public record CoworkerOrder
    {
        public required Guid CoworkerId { get; init; }
        public required decimal DrinkCost { get; init; }
    }
}