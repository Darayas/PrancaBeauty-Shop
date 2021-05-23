using Framework.Domain;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.AddressAgg.Entities
{
    public class tblAddress : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CountryId { get; set; }
        public Guid ProviceId { get; set; }
        public Guid CityId { get; set; }
        public string District { get; set; } // محله
        public string Address { get; set; }
        public string Plaque { get; set; } // پلاک
        public string Unit { get; set; }
        public string PostalCode { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual tblCountries tblCountries { get; set; }
    }
}
