using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Address
{
    public class OutGetAddressById
    {
        public string CountryId { get; set; }
        public string ProviceId { get; set; }
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
