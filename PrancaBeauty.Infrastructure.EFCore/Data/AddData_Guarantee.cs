using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.GuaranteeAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Guarantee
    {
        BaseRepository<tblGuarantee> repGuarantee = null;
        BaseRepository<tblLanguages> repLang = null;
        public AddData_Guarantee()
        {
            repGuarantee = new BaseRepository<tblGuarantee>(new MainContext());
            repLang = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            if (!repGuarantee.Get.Any(a => a.Name == "BrancabeautyGuarantee6Month"))
            {
                repGuarantee.AddAsync(new tblGuarantee()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "BrancabeautyGuarantee6Month",
                    IsEnable = true,
                    tblGuarantee_Translates = new List<tblGuarantee_Translates>()
                    {
                        new tblGuarantee_Translates()
                        {
                            Id= new Guid().SequentialGuid(),
                            LangId=repLang.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="گارانتی شش ماهه پرنسابیوتی"
                        },
                        new tblGuarantee_Translates()
                        {
                            Id= new Guid().SequentialGuid(),
                            LangId=repLang.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Pranca Beauty six-month Guarantee"
                        }
                    }
                }, default, false).Wait();

                if (!repGuarantee.Get.Any(a => a.Name == "2CellGuarantee6Month"))
                {
                    repGuarantee.AddAsync(new tblGuarantee()
                    {
                        Id = new Guid().SequentialGuid(),
                        Name = "BrancabeautyGuarantee6Month",
                        IsEnable = true,
                        tblGuarantee_Translates = new List<tblGuarantee_Translates>()
                        {
                            new tblGuarantee_Translates()
                            {
                                Id= new Guid().SequentialGuid(),
                                LangId=repLang.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                                Title="گارانتی دو سل"
                            },
                            new tblGuarantee_Translates()
                            {
                                Id= new Guid().SequentialGuid(),
                                LangId=repLang.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                                Title="Two Cell Guarantee"
                            }
                        }
                    }, default, false).Wait();
                }

                repGuarantee.SaveChangeAsync().Wait();
            }
        }
    }
}
