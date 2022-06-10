using Framework.Infrastructure;
using PrancaBeauty.Domin.Bills.TaxAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System.Linq;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_TaxGroups
    {
        BaseRepository<tblTaxGroups> _RepTaxGroup;
        BaseRepository<tblCountries> _RepCountry;
        public AddData_TaxGroups()
        {
            _RepTaxGroup= new BaseRepository<tblTaxGroups>(new MainContext());
            _RepCountry= new BaseRepository<tblCountries>(new MainContext());
        }

        public void Run()
        {
            var _IranCountryId = _RepCountry.Get.Where(b => b.Name=="Iran").Select(b => b.Id).Single();
            #region Iran - کالاهای لوکس
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_IranCountryId).Where(a => a.Title=="کالاهای لوکس").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {

                    }, default, true).Wait();
                }
            }
            #endregion
        }
    }
}
