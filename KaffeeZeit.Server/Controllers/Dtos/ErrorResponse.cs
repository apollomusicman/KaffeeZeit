namespace KaffeeZeit.Server.Controllers.Dtos
{
    public record ErrorResponse
    {
        public required string ErrorCode { get; init; }
        public required string ErrorMessage { get; init; }
    }
}
