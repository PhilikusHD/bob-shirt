namespace Bob.Core.DTO
{
    public record AddressDto(
        uint AddressId,
        string Street,
        string HouseNumber,
        string PostalCode,
        string City
    );
}
