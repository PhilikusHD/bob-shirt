using System;

namespace Bob.Core.DTO
{
    public record OrderDto(
        uint OrderId,
        uint CustomerId,
        uint CartId,
        DateTime OrderDate,
        decimal TotalAmount
    );
}
