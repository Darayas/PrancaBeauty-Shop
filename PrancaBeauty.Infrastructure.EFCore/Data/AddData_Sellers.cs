using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Sellers
    {
        BaseRepository<tblSellers> _SellerRepository;
        BaseRepository<tblUsers> _UserRepository;
        BaseRepository<tblLanguages> _LanguageRepository;
        BaseRepository<tblFilePaths> _FilePaths;
        BaseRepository<tblFileTypes> _FileTypes;

        MainContext _Context;
        public AddData_Sellers()
        {
            _Context = new MainContext();
            _SellerRepository = new BaseRepository<tblSellers>(_Context);
            _UserRepository = new BaseRepository<tblUsers>(_Context);
            _LanguageRepository = new BaseRepository<tblLanguages>(_Context);
            _FilePaths = new BaseRepository<tblFilePaths>(_Context);
            _FileTypes = new BaseRepository<tblFileTypes>(_Context);
        }

        public void Run()
        {
            if (!_SellerRepository.Get.Where(a => a.tblUsers.UserName == "reza9025@gmail.com").Where(a => a.Name == "Darayas").Any())
            {
                _SellerRepository.AddAsync(new tblSellers
                {
                    Id = new Guid().SequentialGuid(),
                    UserId = _UserRepository.Get.Where(a => a.UserName == "reza9025@gmail.com").Select(a => a.Id).Single(),
                    Name = "Darayas",
                    Date = DateTime.Now,
                    tblSeller_Translates = new List<tblSeller_Translates> {
                        new tblSeller_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _LanguageRepository.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="دارایاس",
                            tblFiles = new tblFiles()
                            {
                                Id = new Guid().SequentialGuid(),
                                FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2021/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                                Title = "DarayasSellerFaLogo",
                                Date = DateTime.Now,
                                FileName = "DarayasSellerFaLogo.png",
                                FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                                SizeOnDisk = 0
                            }
                        },
                        new tblSeller_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _LanguageRepository.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Darayas",
                            tblFiles = new tblFiles()
                            {
                                Id = new Guid().SequentialGuid(),
                                FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2021/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                                Title = "DarayasSellerEnLogo",
                                Date = DateTime.Now,
                                FileName = "DarayasSellerEnLogo.png",
                                FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                                SizeOnDisk = 0
                            }
                        }
                    }
                }, default, false).Wait();
            }

            _SellerRepository.SaveChangeAsync().Wait();
        }
    }
}
