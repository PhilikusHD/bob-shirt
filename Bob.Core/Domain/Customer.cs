using System;

namespace Bob.Core.Domain
{
    public readonly struct CustomerId
    {
        public uint Value { get; }
        public CustomerId(uint value) => Value = value;
        public override string ToString() => Value.ToString();
    }

    public sealed class Customer
    {
        public CustomerId Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public AddressId AddressId { get; }
        public string PhoneNumber { get; }
        public DateTime SignupDate { get; }

        public Customer(
            CustomerId id,
            string name,
            string surname,
            string email,
            AddressId addressId,
            string phoneNumber,
            DateTime signupDate)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            AddressId = addressId;
            PhoneNumber = phoneNumber;
            SignupDate = signupDate;
        }
    }
}
