using PrancaBeauty.Domin.Region.CountryAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Countries
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ICountryRepository _CountryRepository;

        public CountryApplication(ICountryRepository countryRepository)
        {
            _CountryRepository = countryRepository;
        }
    }
}
