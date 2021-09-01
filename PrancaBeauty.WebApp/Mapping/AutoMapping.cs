using AutoMapper;
using PrancaBeauty.Application.Contracts.Address;
using PrancaBeauty.Application.Contracts.Categories;
using PrancaBeauty.Application.Contracts.Products;
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
            CreateMap<OutGetProductsForManage, vmProductList>();
            CreateMap<OutGetAddressDetails, viCompo_EditAddress>();
            CreateMap<viCompo_EditAddress, InpEditAddress>();
            CreateMap<viAddCategory, InpAddCategory>();
            CreateMap<viAddCategory_Translate, InpAddCategory_Translate>();
            CreateMap<viEditCategory, InpSaveEdit>();
            CreateMap<viEditCategory_Translate, InpSaveEdit_Translate>();

            CreateMap<PrancaBeauty.Application.Contracts.Categories.OutGetListForAdminPage, vmCategoriesList>();
            CreateMap<OutGetForEdit, viEditCategory>();
            CreateMap<OutGetForEdit_Translate, viEditCategory_Translate>();

            CreateMap<PrancaBeauty.Application.Contracts.Countries.OutGetListForCombo, vmCompo_Combo_Countries>();
            CreateMap<PrancaBeauty.Application.Contracts.Province.OutGetListForCombo, vmCompo_Combo_Province>();
            CreateMap<PrancaBeauty.Application.Contracts.City.OutGetListForCombo, vmCompo_Combo_Cities>();
            CreateMap<PrancaBeauty.Application.Contracts.Categories.OutGetListForCombo, vmCompo_Combo_Categories>();
            CreateMap<PrancaBeauty.Application.Contracts.Users.OutGetListForCombo, vmCompo_Combo_Users>();
        }
    }
}
