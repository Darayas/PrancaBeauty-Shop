using Framework.Domain;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.CountryAgg.Contracts
{
    public interface ICountryRepository : IRepository<tblCountries>
    {
    }
}
