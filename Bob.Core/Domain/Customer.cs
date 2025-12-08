using System;
using LinqToDB.Mapping;

namespace Bob.Core.Domain
{

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

        [Column("PASSWORDHASH")]
        public string PasswordHash { get; set; }

        [Column("ISADMIN")]
        public bool IsAdmin { get; set; }

        public Customer(
            int id,
            string name,
            string surname,
            string email,
            int addressId,
            string phoneNumber, DateTime signupDate, string password, bool isAdmin)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            AddressId = addressId;
            PhoneNumber = phoneNumber;
            SignupDate = signupDate;
            PasswordHash = password;
            IsAdmin = isAdmin;
        }
    }
}
