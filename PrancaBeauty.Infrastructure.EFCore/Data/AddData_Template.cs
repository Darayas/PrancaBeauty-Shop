using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.TemplatesAgg.Entitis;
using PrancaBeauty.Infrastructure.EFCore.Common.ExMethods;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Template
    {
        BaseRepository<tblTamplates> _repTemplate;
        BaseRepository<tblLanguages> _repLang;
        public AddData_Template()
        {
            _repTemplate = new BaseRepository<tblTamplates>(new MainContext());
            _repLang = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {

            // ConfirmationEmail  fa-IR
            if (!_repTemplate.GetNoTraking.Any(a => a.tblLanguages.Code == "fa-IR" && a.Name == "ConfirmationEmail"))
            {
                _repTemplate.AddAsync(new tblTamplates()
                {
                    Id = new Guid().SequentialGuid(),
                    LangId = _repLang.GetNoTraking.Where(a => a.Code == "fa-IR").Select(a => a.Id).Single(),
                    Name = "ConfirmationEmail",
                    Template = "<a href='[Url]'>کلیک</a>"
                }, default, true).Wait();
            }

            // ConfirmationEmail  en-US
            if (!_repTemplate.GetNoTraking.Any(a => a.tblLanguages.Code == "en-US" && a.Name == "ConfirmationEmail"))
            {
                _repTemplate.AddAsync(new tblTamplates()
                {
                    Id = new Guid().SequentialGuid(),
                    LangId = _repLang.GetNoTraking.Where(a => a.Code == "en-US").Select(a => a.Id).Single(),
                    Name = "ConfirmationEmail",
                    Template = "<a href='[Url]'>Click</a>"
                }, default, true).Wait();
            }

            // EmailLogin  fa-IR
            if (!_repTemplate.GetNoTraking.Any(a => a.tblLanguages.Code == "fa-IR" && a.Name == "EmailLogin"))
            {
                _repTemplate.AddAsync(new tblTamplates()
                {
                    Id = new Guid().SequentialGuid(),
                    LangId = _repLang.GetNoTraking.Where(a => a.Code == "fa-IR").Select(a => a.Id).Single(),
                    Name = "EmailLogin",
                    Template = "<a href='[Url]'>ورود به سایت</a>"
                }, default, true).Wait();
            }

            // EmailLogin  en-US
            if (!_repTemplate.GetNoTraking.Any(a => a.tblLanguages.Code == "en-US" && a.Name == "EmailLogin"))
            {
                _repTemplate.AddAsync(new tblTamplates()
                {
                    Id = new Guid().SequentialGuid(),
                    LangId = _repLang.GetNoTraking.Where(a => a.Code == "en-US").Select(a => a.Id).Single(),
                    Name = "EmailLogin",
                    Template = "<a href='[Url]'>Click To Login</a>"
                }, default, true).Wait();
            }
        }

    }
}
