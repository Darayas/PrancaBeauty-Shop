using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Address;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Users;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<viCompo_AccountSettings, InpSaveAccountSettingUserDetails>();
            CreateMap<viCompo_AddAddress, InpAddAddress>();
            CreateMap<OutGetAddressByUserIdForManage, vmCompo_ListAddress>();
            CreateMap<OutGetAddressDetails, viCompo_EditAddress>();
            CreateMap<viCompo_EditAddress, InpEditAddress>();
            CreateMap<ApplicationDTO.Users.OutGetListForCombo, vmCompo_Combo_Users>();
        }
    }
}
