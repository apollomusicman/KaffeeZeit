namespace KaffeeZeit.Server.Controllers.Dtos
{
    public record PaymentRequest
    {
        public required Guid CoworkerId { get; init; }
        public required bool OverrideNext { get; init; }
    }
}