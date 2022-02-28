using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Entities;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_ProductReviewAttributes
    {
        BaseRepository<tblProductTopic> _repProductTopic;
        BaseRepository<tblLanguages> _repLanguages;
        BaseRepository<tblProductReviewsAttribute> _repProductReviewsAttribute;

        public AddData_ProductReviewAttributes()
        {
            _repProductTopic = new BaseRepository<tblProductTopic>(new MainContext());
            _repLanguages = new BaseRepository<tblLanguages>(new MainContext());
            _repProductReviewsAttribute = new BaseRepository<tblProductReviewsAttribute>(new MainContext());
        }

        public async void Run()
        {
            #region Smart phone
            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "SmartPhone" && a.Name == "ConstructionQuality").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "ConstructionQuality",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="کیفیت ساخت"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Construction quality"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "SmartPhone" && a.Name == "PurchaseValueRelativeToPrice").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "PurchaseValueRelativeToPrice",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="ارزش خرید به نسبت قیمت"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Purchase value relative to price"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "SmartPhone" && a.Name == "Innovation").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "Innovation",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="نوآوری"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Innovation"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "SmartPhone" && a.Name == "FeaturesAndCapabilities").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "FeaturesAndCapabilities",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="امکانات و قابلیت ها"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Features and capabilities"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "SmartPhone" && a.Name == "EaseOfUse").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "EaseOfUse",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="سهولت استفاده"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="ease of use"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "SmartPhone" && a.Name == "DesignAndAppearance").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "SmartPhone").Select(a => a.Id).Single(),
                    Name = "DesignAndAppearance",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="طراحی و ظاهر"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Design and appearance"
                        }
                    }
                }, default, false);
            }
            #endregion

            #region Loptop
            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "Laptop" && a.Name == "WorthBuyingAgainstPrice").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "WorthBuyingAgainstPrice",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="ارزش خرید در برابر قیمت"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Worth buying against price"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "Laptop" && a.Name == "InternalHardwareAndBatteryPerformance").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "InternalHardwareAndBatteryPerformance",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="عملکرد سخت افزار داخلی و باتری"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Internal hardware and battery performance"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "Laptop" && a.Name == "KeyboardAndTouchpad").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "KeyboardAndTouchpad",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="کیبورد و تاچ پد"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Keyboard and touchpad"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "Laptop" && a.Name == "ScreenQualityAndSpeakers").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "ScreenQualityAndSpeakers",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="کیفیت صفحه نمایش و بلندگوها"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Screen quality and speakers"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "Laptop" && a.Name == "FeaturesAndCapabilities").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "FeaturesAndCapabilities",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="امکانات و قابلیت ها"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Features and capabilities"
                        }
                    }
                }, default, false);
            }

            if (!await _repProductReviewsAttribute.Get.Where(a => a.tblProductTopic.Name == "Laptop" && a.Name == "DesignAndBuildQuality").AnyAsync())
            {
                await _repProductReviewsAttribute.AddAsync(new tblProductReviewsAttribute
                {
                    Id = new Guid().SequentialGuid(),
                    TopicId = _repProductTopic.Get.Where(a => a.Name == "Laptop").Select(a => a.Id).Single(),
                    Name = "DesignAndBuildQuality",
                    tblProductReviewsAttribute_Translate = new List<tblProductReviewsAttribute_Translate>
                    {
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).SingleAsync(),
                            Title="طراحی و کیفیت ساخت"
                        },
                        new tblProductReviewsAttribute_Translate{
                            Id=new Guid().SequentialGuid(),
                            LangId=await _repLanguages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).SingleAsync(),
                            Title="Design and build quality"
                        }
                    }
                }, default, false);
            }

            #endregion

            await _repProductReviewsAttribute.SaveChangeAsync();
        }
    }
}
