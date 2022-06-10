using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_ProductGroups
    {
        BaseRepository<tblProductGroups> _ProductGroups;
        BaseRepository<tblProductTopic> _RepProductTopic;
        BaseRepository<tblLanguages> _RepLanguage;
        public AddData_ProductGroups()
        {
            _ProductGroups= new BaseRepository<tblProductGroups>(new MainContext());
            _RepProductTopic= new BaseRepository<tblProductTopic>(new MainContext());
            _RepLanguage= new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            var _PersianLangId = _RepLanguage.Get.Where(a => a.Code=="fa-IR").Select(a => a.Id).Single();
            var _EnLangId = _RepLanguage.Get.Where(a => a.Code=="en-US").Select(a => a.Id).Single();

            var _SmartPhoneId = _RepProductTopic.Get.Where(a => a.Name=="SmartPhone").Select(a => a.Id).Single();
            var _LaptopId = _RepProductTopic.Get.Where(a => a.Name=="Laptop").Select(a => a.Id).Single();

            if (!_ProductGroups.Get.Where(a => a.TopicId==_SmartPhoneId).Where(a => a.Name=="SmartPhone").Any())
            {
                _ProductGroups.AddAsync(new tblProductGroups()
                {
                    Id= new Guid().SequentialGuid(),
                    TopicId=_SmartPhoneId,
                    Name="SmartPhone",
                    tblProductGroupTranslate= new List<tblProductGroupTranslate>()
                {
                    new tblProductGroupTranslate
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=_PersianLangId,
                        Title="گوشی موبایل هوشمند"
                    },
                    new tblProductGroupTranslate
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=_EnLangId,
                        Title="Smart Phone"
                    }
                }
                }, default, true).Wait();
            }

            if (!_ProductGroups.Get.Where(a => a.TopicId==_SmartPhoneId).Where(a => a.Name=="IPhone13").Any())
            {
                _ProductGroups.AddAsync(new tblProductGroups()
                {
                    Id= new Guid().SequentialGuid(),
                    TopicId=_SmartPhoneId,
                    Name="IPhone13",
                    tblProductGroupTranslate= new List<tblProductGroupTranslate>()
                {
                    new tblProductGroupTranslate
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=_PersianLangId,
                        Title="آیفون 13"
                    },
                    new tblProductGroupTranslate
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=_EnLangId,
                        Title="IPhone13"
                    }
                }
                }, default, true).Wait();
            }

            if (!_ProductGroups.Get.Where(a => a.TopicId==_LaptopId).Where(a => a.Name=="Loptop").Any())
            {
                _ProductGroups.AddAsync(new tblProductGroups()
                {
                    Id= new Guid().SequentialGuid(),
                    TopicId=_LaptopId,
                    Name="Loptop",
                    tblProductGroupTranslate= new List<tblProductGroupTranslate>()
                {
                    new tblProductGroupTranslate
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=_PersianLangId,
                        Title="لپتاپ"
                    },
                    new tblProductGroupTranslate
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=_EnLangId,
                        Title="Loptop"
                    }
                }
                }, default, true).Wait();
            }
        }
    }
}
