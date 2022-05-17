using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Category
    {
        BaseRepository<tblCategoris> _RepCategory;
        BaseRepository<tblLanguages> _LanguageRepository;
        BaseRepository<tblFilePaths> _FilePaths;
        BaseRepository<tblFileTypes> _FileTypes;

        MainContext _Context;
        public AddData_Category()
        {
            _Context = new MainContext();
            _RepCategory = new BaseRepository<tblCategoris>(_Context);
            _LanguageRepository = new BaseRepository<tblLanguages>(_Context);
            _FilePaths = new BaseRepository<tblFilePaths>(_Context);
            _FileTypes = new BaseRepository<tblFileTypes>(_Context);
        }
        public void Run()
        {
            if (!_RepCategory.Get.Where(a => a.Name == "AllProducts").Any())
            {
                _RepCategory.AddAsync(new tblCategoris
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "AllProducts",
                    ParentId = default,
                    Sort = 0,
                    tblCategory_Translates = (new List<tblCategory_Translates>
                    {
                        new tblCategory_Translates{
                             Id = new Guid().SequentialGuid(),
                             LangId=_LanguageRepository.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                             Title="همه محصولات",
                             Description=""
                        },
                        new tblCategory_Translates{
                             Id = new Guid().SequentialGuid(),
                             LangId=_LanguageRepository.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                             Title="All Products",
                             Description=""
                        }
                    }).ToList(),
                    tblFiles = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2022/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                        Title = "AllProducts",
                        Date = DateTime.Now,
                        FileName = "AllProducts.png",
                        FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                        SizeOnDisk = 0
                    }
                }, default, false).Wait();
            }

            _RepCategory.SaveChangeAsync().Wait();
        }
    }
}
