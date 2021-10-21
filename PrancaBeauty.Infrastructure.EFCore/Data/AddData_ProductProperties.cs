using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Entities;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_ProductProperties
    {
        BaseRepository<tblProductTopic> _repProductTopics;
        BaseRepository<tblLanguages> _repLanguage;
        BaseRepository<tblProductPropertis> _repProductPropertis;

        public AddData_ProductProperties()
        {
            _repProductPropertis = new BaseRepository<tblProductPropertis>(new MainContext());
            _repProductTopics = new BaseRepository<tblProductTopic>(new MainContext());
            _repLanguage = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            #region SmartPhone
            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "SmartPhone" & a.Name == "CPU"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "CPU",
                    Sort = 0,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="CPU"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="پردازنده"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "SmartPhone" & a.Name == "RAM"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "RAM",
                    Sort = 1,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="RAM"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="حافظه اجرایی"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "SmartPhone" & a.Name == "OS"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "OS",
                    Sort = 2,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="OS"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="سیستم عامل"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "SmartPhone" & a.Name == "Camera"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "Camera",
                    Sort = 3,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Camera"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="دوربین"
                        }
                    }
                }, default, false).Wait();
            }
            #endregion

            #region Laptop
            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "Laptop" & a.Name == "CPU"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "CPU",
                    Sort = 0,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="CPU"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="پردازنده"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "Laptop" & a.Name == "RAM"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "RAM",
                    Sort = 1,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="RAM"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="حافظه اجرایی"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "Laptop" & a.Name == "OS"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "OS",
                    Sort = 2,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="OS"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="سیستم عامل"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "Laptop" & a.Name == "Camera"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "Camera",
                    Sort = 3,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Camera"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="دوربین"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "Laptop" & a.Name == "Diplay"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "Diplay",
                    Sort = 4,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Diplay"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="صفحه نمایش"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "Laptop" & a.Name == "HDD"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "HDD",
                    Sort = 5,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="HDD"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="هارد دیسک"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_repProductPropertis.Get.Any(a => a.tblProductTopic.Name == "Laptop" & a.Name == "SSD"))
            {
                _repProductPropertis.AddAsync(new tblProductPropertis()
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopics.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "SSD",
                    Sort = 6,
                    tblProductPropertis_Translates = new List<tblProductPropertis_Translates>() {
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="SSD"
                        },
                        new tblProductPropertis_Translates(){
                            Id = new Guid().SequentialGuid(),
                            LangId=_repLanguage.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="حافظه ی SSD"
                        }
                    }
                }, default, false).Wait();
            }
            #endregion

            _repProductTopics.SaveChangeAsync().Wait();
        }
    }
}
