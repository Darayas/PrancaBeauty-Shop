namespace PrancaBeauty.Application.Contracts.Address
{
    public class OutGetAddressDetails
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string CountryId { get; set; }

        public string ProvinceId { get; set; }

        public string CityId { get; set; }

        public string District { get; set; } // محله

        public string Address { get; set; }

        public string Plaque { get; set; } // پلاک

        public string Unit { get; set; }

        public string PostalCode { get; set; }

        public string NationalCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
