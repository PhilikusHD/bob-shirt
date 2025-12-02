namespace Bob.Core.DTO
{
    public record PaymentProcessorDto(
        uint ProcessorId,
        string Method
    );
}
