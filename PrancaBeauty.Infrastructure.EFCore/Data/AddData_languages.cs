using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Common.ExMethods;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_languages
    {
        BaseRepository<tblLanguages> _repLang;
        public AddData_languages()
        {
            _repLang = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            if (!_repLang.GetNoTraking.Any(a => a.Code == "fa-IR"))
            {
                _repLang.AddAsync(new tblLanguages()
                {
                    Id = new Guid().SequentialGuid(),
                    Code = "fa-IR",
                    IsActive = true,
                    IsRtl = true,
                    Name = "Persian_IR",
                    NativeName = "فارسی (ایران)",
                    Abbr="fa"
                }, default, true).Wait();
            }

            if (!_repLang.GetNoTraking.Any(a => a.Code == "en-US"))
            {
                _repLang.AddAsync(new tblLanguages()
                {
                    Id = new Guid().SequentialGuid(),
                    Code = "en-US",
                    IsActive = true,
                    IsRtl = false,
                    Name = "English_USA",
                    NativeName = "English (USA)",
                    Abbr="en"
                }, default, true).Wait();
            }
        }
    }
}
