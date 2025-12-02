using System;

namespace Bob.Core.DTO
{
    public record PaymentDto(
        uint PaymentId,
        uint OrderId,
        DateTime PaymentDate,
        uint ProcessorId
    );
}
