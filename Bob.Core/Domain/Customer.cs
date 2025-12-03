using System;
using LinqToDB.Mapping;

namespace Bob.Core.Domain
{
    public readonly struct CustomerId
    {
        public int Value { get; }
        public CustomerId(int value) => Value = value;
        public override string ToString() => Value.ToString();

        public static implicit operator CustomerId(int id) => new CustomerId(id);
        public static implicit operator int(CustomerId id) => id.Value;
    }

    [Table("CUSTOMER")]
    public sealed class Customer
    {
        [PrimaryKey]
        [Column("CUSTOMERID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("SURNAME")]
        public string Surname { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("ADDRESSID")]
        public int AddressId { get; set; }

        [Column("PHONENR")]
        public string PhoneNumber { get; set; }

        [Column("SIGNUPDATE")]
        public DateTime SignupDate { get; set; }

        public Customer(
            int id,
            string name,
            string surname,
            string email,
            int addressId,
            string phoneNumber, DateTime signupDate)
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
