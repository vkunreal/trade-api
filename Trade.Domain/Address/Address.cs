namespace Trade.Domain
{
    public class Address
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string? FlatNumber { get; set; }

        public string AddressDescription { get; set; }

        public Address(Guid userId, string country, string region, string city, string postalCode, string street, string houseNumber, string flatNumber, string addressDescription)
        {
            Id = new();
            UserId = userId;

            Country = country;
            Region = region;
            City = city;
            PostalCode = postalCode;
            Street = street;
            HouseNumber = houseNumber;
            FlatNumber = flatNumber;
            AddressDescription = addressDescription;
        }

        public virtual User User { get; set; }
    }
}
