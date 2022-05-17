using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_ProductVariants
    {
        BaseRepository<tblProductVariants> _repProductVariants;
        BaseRepository<tblLanguages> _repLanguages;

        public AddData_ProductVariants()
        {
            _repProductVariants = new BaseRepository<tblProductVariants>(new MainContext());
            _repLanguages = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            if (!_repProductVariants.Get.Any(a => a.Name == "Color"))
            {
                _repProductVariants.AddAsync(new tblProductVariants()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "Color",
                    VariantType = "RadioButton",
                    tblProductVariants_Translates = (new List<tblProductVariants_Translates> {
                        new tblProductVariants_Translates(){
                            Id= new Guid().SequentialGuid(),
                            LangId=_repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="رنگ"
                        },
                        new tblProductVariants_Translates(){
                            Id= new Guid().SequentialGuid(),
                            LangId=_repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Color"
                        }
                    }).ToList()
                }, default, false).Wait();
            }

            if (!_repProductVariants.Get.Any(a => a.Name == "Size"))
            {
                _repProductVariants.AddAsync(new tblProductVariants()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "Size",
                    VariantType = "ComboBox",
                    tblProductVariants_Translates = (new List<tblProductVariants_Translates> {
                        new tblProductVariants_Translates(){
                            Id= new Guid().SequentialGuid(),
                            LangId=_repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="اندازه"
                        },
                        new tblProductVariants_Translates(){
                            Id= new Guid().SequentialGuid(),
                            LangId=_repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Size"
                        }
                    }).ToList()
                }, default, false).Wait();
            }

            _repProductVariants.SaveChangeAsync().Wait();
        }
    }
}
