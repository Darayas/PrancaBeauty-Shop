using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_PaymentGates
    {
        BaseRepository<tblPaymentGates> _RepPaymentGate;
        BaseRepository<tblFiles> _RepFile;
        BaseRepository<tblLanguages> _RepLang;
        BaseRepository<tblFileTypes> _FileTypes;
        BaseRepository<tblFilePaths> _FilePaths;
        public AddData_PaymentGates()
        {
            _RepPaymentGate=new BaseRepository<tblPaymentGates>(new MainContext());
            _RepFile=new BaseRepository<tblFiles>(new MainContext());
            _RepLang=new BaseRepository<tblLanguages>(new MainContext());
            _FileTypes=new BaseRepository<tblFileTypes>(new MainContext());
            _FilePaths=new BaseRepository<tblFilePaths>(new MainContext());
        }

        public void Run()
        {
            var _PersianLangId = _RepLang.Get.Where(a => a.Code=="fa-IR").Select(a => a.Id).Single();
            var _EnLangId = _RepLang.Get.Where(a => a.Code=="en-US").Select(a => a.Id).Single();

            #region ZarinPal Gate
            {
                #region Encrypt Data
                string _JsonData = "";
                {
                    var JsonData = File.ReadAllText($@"{AssemblyDirectory}\Data\AddData_PaymentGatesZarinPal.json", Encoding.UTF8);
                    _JsonData = JsonData.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                if (!_RepPaymentGate.Get.Where(a => a.Name=="ZarinPal").Any())
                {
                    _RepPaymentGate.AddAsync(new tblPaymentGates
                    {
                        Id= new Guid().SequentialGuid(),
                        Name="ZarinPal",
                        Data=_JsonData,
                        IsEnable=true,
                        tblFiles= new tblFiles
                        {
                            Id = new Guid().SequentialGuid(),
                            FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2022/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                            Title = "ZarinPal",
                            Date = DateTime.Now,
                            FileName = "ZarinPal.png",
                            FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                            SizeOnDisk = 0
                        },
                        tblPaymentGateTranslate= new List<tblPaymentGateTranslate> {
                            new tblPaymentGateTranslate{
                                Id = new Guid().SequentialGuid(),
                                LangId=_PersianLangId,
                                Title="زرین پال"
                            },
                            new tblPaymentGateTranslate{
                                Id = new Guid().SequentialGuid(),
                                LangId=_EnLangId,
                                Title="ZarinPal"
                            }
                        }
                    }, default, true).Wait();
                }
            }
            #endregion

            #region Paypal Gate
            {
                #region Encrypt Data
                string _JsonData = "";
                {
                    var JsonData = File.ReadAllText($@"{AssemblyDirectory}\Data\AddData_PaymentGatesPaypal.json", Encoding.UTF8);
                    _JsonData = JsonData.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                if (!_RepPaymentGate.Get.Where(a => a.Name=="Paypal").Any())
                {
                    _RepPaymentGate.AddAsync(new tblPaymentGates
                    {
                        Id= new Guid().SequentialGuid(),
                        Name="Paypal",
                        Data=_JsonData,
                        IsEnable=true,
                        tblFiles= new tblFiles
                        {
                            Id = new Guid().SequentialGuid(),
                            FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2022/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                            Title = "Paypal",
                            Date = DateTime.Now,
                            FileName = "Paypal.png",
                            FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                            SizeOnDisk = 0
                        },
                        tblPaymentGateTranslate= new List<tblPaymentGateTranslate> {
                            new tblPaymentGateTranslate{
                                Id = new Guid().SequentialGuid(),
                                LangId=_PersianLangId,
                                Title="پی پال"
                            },
                            new tblPaymentGateTranslate{
                                Id = new Guid().SequentialGuid(),
                                LangId=_EnLangId,
                                Title="Paypal"
                            }
                        }
                    }, default, true).Wait();
                }
            }
            #endregion
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
