using AutoMapper;
using PrancaBeauty.Application.Contracts.Address;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
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
            CreateMap<viCompo_AddAddress, InpAddAddress>();
            CreateMap<OutGetAddressByUserIdForManage, vmCompo_ListAddress>();
            CreateMap<PrancaBeauty.Application.Contracts.Countries.OutGetListForCombo, vmCompo_Combo_Countries>();
            CreateMap<PrancaBeauty.Application.Contracts.Province.OutGetListForCombo, vmCompo_Combo_Province>();
            CreateMap<PrancaBeauty.Application.Contracts.City.OutGetListForCombo, vmCompo_Combo_Cities>();
        }
    }
}
