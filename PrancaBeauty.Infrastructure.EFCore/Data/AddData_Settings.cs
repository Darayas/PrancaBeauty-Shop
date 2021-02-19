using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.StettingsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Common.ExMethods;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Settings
    {
        BaseRepository<tblSettings> _repSetting;
        BaseRepository<tblLanguages> _repLang;
        public AddData_Settings()
        {
            _repSetting = new BaseRepository<tblSettings>(new MainContext());
            _repLang = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            if (!_repSetting.Get.Any(a => a.IsEnable == true && a.tblLanguages.Code == "fa-IR"))
            {
                _repSetting.AddAsync(new tblSettings()
                {
                    Id = new Guid().SequentialGuid(),
                    Date = DateTime.Now,
                    IsEnable = true,
                    IsInManufacture = false,
                    LangId = _repLang.Get.Where(a => a.Code == "fa-IR").Select(a => a.Id).Single(),
                    SiteDescription = "سایت فروشگاهی پرنسابیوتی",
                    SiteEmail = "info@prancabeauty.com",
                    SitePhoneNumber = "09147922542",
                    SiteTitle = "PrancaBeauty",
                    SiteUrl = "https://localhost:44377"
                }, default, true).Wait();
            }
        }
    }
}
