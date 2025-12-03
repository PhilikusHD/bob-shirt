using LinqToDB.Mapping;

namespace Bob.Core.Domain
{


    [Table("ADDRESS")]
    public sealed class Address
    {
        public Address() { }

        [PrimaryKey]
        [Column("ADDRESSID")]
        public int Id { get; set; }

        [Column("STREET")]
        public string Street { get; set; }

        [Column("HOUSENUMBER")]
        public string HouseNumber { get; set; }

        [Column("POSTALCODE")]
        public int PostalCode { get; set; } // matches SQL INTEGER

        [Column("CITY")]
        public string City { get; set; }

        public Address(int id, string street, string houseNumber, int postalCode, string city)
        {
            Id = id;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
        }
    }

}
