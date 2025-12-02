namespace Bob.Core.DTO
{
    public record CartDto(
        uint CartId,
        uint ItemId,
        uint OrderId
    );
}
