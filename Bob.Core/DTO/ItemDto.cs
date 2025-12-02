namespace Bob.Core.DTO
{
    public record ItemDto(
        uint ItemId,
        string Name,
        string Size,
        string Color,
        decimal Price
    );
}
