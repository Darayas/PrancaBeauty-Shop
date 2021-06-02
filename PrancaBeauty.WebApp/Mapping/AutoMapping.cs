using AutoMapper;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Models.ViewInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<viCompo_AccountSettings, InpSaveAccountSettingUserDetails>();
        }
    }
}
