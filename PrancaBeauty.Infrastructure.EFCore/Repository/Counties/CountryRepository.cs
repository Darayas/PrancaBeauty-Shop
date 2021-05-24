using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CountryAgg.Contracts;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Counties
{
    public class CountryRepository : BaseRepository<tblCountries>, ICountryRepository
    {
        public CountryRepository(MainContext context) : base(context)
        {

        }
    }
}
