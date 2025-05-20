namespace KaffeeZeit.Server.Controllers.Dtos
{
    public record PaymentRequest
    {
        public required Guid CoworkerId { get; init; }
    }
}