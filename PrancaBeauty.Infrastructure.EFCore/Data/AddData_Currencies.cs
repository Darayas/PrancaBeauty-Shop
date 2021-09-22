using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Currencies
    {
        BaseRepository<tblCurrencies> _Currencies;
        BaseRepository<tblCountries> _Countries;
        public AddData_Currencies()
        {
            _Currencies = new BaseRepository<tblCurrencies>(new MainContext());
            _Countries = new BaseRepository<tblCountries>(new MainContext());
        }

        public void Run()
        {
            if (!_Currencies.Get.Where(a => a.tblCountry.Name == "Iran").Where(a => a.Name == "").Any())
            {

            }
        }
    }
}
