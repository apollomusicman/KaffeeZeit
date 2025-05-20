namespace KaffeeZeit.Server.Controllers.Dtos
{
    public record RunningTabs
    {
        public required List<Tab> CoworkerTabs { get; init; }
        public required int Revision { get; init; }
    }

    public record Tab
    {
        public required string CoworkerName { get; init; }
        public required Guid CoworkerId { get; init; }
        public required decimal RunningTab { get; init; }
    }
}