using AutoMapper;
using PrancaBeauty.Application.Contracts.Address;
using PrancaBeauty.Application.Contracts.Categories;
using PrancaBeauty.Application.Contracts.Currency;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.ProductProperties;
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
            CreateMap<viAddProduct, InpAddProdcut>();
            CreateMap<viAddCategory_Translate, InpAddCategory_Translate>();
            CreateMap<viEditCategory, InpSaveEdit>();
            CreateMap<viEditCategory_Translate, InpSaveEdit_Translate>();
            CreateMap<viCompo_ModalFileList, InpGetFileListForFileManager>();

            CreateMap<PrancaBeauty.Application.Contracts.Categories.OutGetListForAdminPage, vmCategoriesList>();
            CreateMap<OutGetForEdit, viEditCategory>();
            CreateMap<OutGetForEdit_Translate, viEditCategory_Translate>();

            CreateMap<PrancaBeauty.Application.Contracts.Countries.OutGetListForCombo, vmCompo_Combo_Countries>();
            CreateMap<PrancaBeauty.Application.Contracts.Province.OutGetListForCombo, vmCompo_Combo_Province>();
            CreateMap<PrancaBeauty.Application.Contracts.City.OutGetListForCombo, vmCompo_Combo_Cities>();
            CreateMap<PrancaBeauty.Application.Contracts.Categories.OutGetListForCombo, vmCompo_Combo_Categories>();
            CreateMap<PrancaBeauty.Application.Contracts.Users.OutGetListForCombo, vmCompo_Combo_Users>();
            CreateMap<PrancaBeauty.Application.Contracts.FileTypes.outGetListForCombo, vmCompo_ComboFileTypes>();
            CreateMap<PrancaBeauty.Application.Contracts.ProductTopics.OutGetListForCombo, vmCompo_Combo_Topics>();
            CreateMap<PrancaBeauty.Application.Contracts.ProductVariants.outGetLstForCombo, vmCompo_Combo_ProductVariants>();
            CreateMap<PrancaBeauty.Application.Contracts.Guarantee.OutGetListForCombo, vmCompo_Combo_Guarantee>();
            CreateMap<OutGetMainByCountryId, vmCompo_Input_Price>();
            CreateMap<outGetFileDetailsForFileSelector, vmCompo_FileSelector>();
            CreateMap<OutGetFileListForFileManager, vmCompo_ModalFileList>();
            CreateMap<OutGetForManageProduct, vmCompo_Properties>();
        }
    }
}
