using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
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
    public class AddData_ProductTopics
    {
        BaseRepository<tblProductTopic> _repProdcutTopic;
        BaseRepository<tblLanguages> _repLang;
        BaseRepository<tblFilePaths> _repFilePath;
        BaseRepository<tblFileTypes> _repFileType;
        public AddData_ProductTopics()
        {
            _repProdcutTopic = new BaseRepository<tblProductTopic>(new MainContext());
            _repLang = new BaseRepository<tblLanguages>(new MainContext());
            _repFilePath = new BaseRepository<tblFilePaths>(new MainContext());
            _repFileType = new BaseRepository<tblFileTypes>(new MainContext());
        }

        public void Run()
        {
            if (!_repProdcutTopic.Get.Any(a => a.Name == "SmartPhone"))
            {
                _repProdcutTopic.AddAsync(new tblProductTopic()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "SmartPhone",
                    tblFiles = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Title = "SmartPhone",
                        Date = DateTime.Now,
                        FileName = "SmartPhone.png",
                        IsPrivate = false,
                        SizeOnDisk = 0,
                        FileTypeId = _repFileType.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                        FilePathId = _repFilePath.Get.Where(a => a.Path == "/image/png/2022/1/1/").Select(a => a.Id).Single()
                    },
                    tblProductTopic_Translates = (new List<tblProductTopic_Translates>() {
                        new tblProductTopic_Translates(){
                             Id = new Guid().SequentialGuid(),
                             LangId=_repLang.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                             Title="Smart Phone"
                        },
                        new tblProductTopic_Translates(){
                             Id = new Guid().SequentialGuid(),
                             LangId=_repLang.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                             Title="گوشی موبایل هوشمند"
                        }
                    }).ToList()
                }, default, false).Wait();
            }

            if (!_repProdcutTopic.Get.Any(a => a.Name == "Laptop"))
            {
                _repProdcutTopic.AddAsync(new tblProductTopic()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "Laptop",
                    tblFiles = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Title = "Loptop",
                        Date = DateTime.Now,
                        FileName = "Laptop.png",
                        IsPrivate = false,
                        SizeOnDisk = 0,
                        FileTypeId = _repFileType.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                        FilePathId = _repFilePath.Get.Where(a => a.Path == "/image/png/2022/1/1/").Select(a => a.Id).Single()
                    },
                    tblProductTopic_Translates = (new List<tblProductTopic_Translates>() {
                        new tblProductTopic_Translates(){
                             Id = new Guid().SequentialGuid(),
                             LangId=_repLang.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                             Title="Laptop"
                        },
                        new tblProductTopic_Translates(){
                             Id = new Guid().SequentialGuid(),
                             LangId=_repLang.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                             Title="لپتاپ"
                        }
                    }).ToList()
                }, default, false).Wait();
            }

            _repProdcutTopic.SaveChangeAsync().Wait();
        }
    }
}
