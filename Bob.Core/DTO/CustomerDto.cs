using System;

namespace Bob.Core.DTO
{
    public record CustomerDto(
        uint CustomerId,
        string Name,
        string Surname,
        string Email,
        uint AddressId,
        string PhoneNumber,
        DateTime SignupDate
    );
}
