namespace KaffeeZeit.Server.Controllers.Dtos
{
    public record RunningTabs
    {
        public required List<Tab> CoworkerTabs { get; set; }
        public required int Revision { get; set; }
    }

    public record Tab
    {
        public required string CoworkerName { get; set; }
        public required Guid CoworkerId { get; set; }
        public required decimal RunningTab { get; set; }
        public required bool IsNextToPay { get; set; }
    }
}