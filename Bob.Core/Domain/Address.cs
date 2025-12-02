namespace Bob.Core.Domain
{
    public readonly struct AddressId
    {
        public uint Value { get; }
        public AddressId(uint value) => Value = value;

        public override string ToString() => Value.ToString();
    }


    public sealed class Address
    {
        public AddressId Id { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string PostalCode { get; }
        public string City { get; }

        public Address(AddressId id, string street, string houseNumber, string postalCode, string city)
        {
            Id = id;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
        }
    }
}
