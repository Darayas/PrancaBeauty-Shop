using AutoMapper;
using PrancaBeauty.Application.Contracts.Address;
using PrancaBeauty.Application.Contracts.Categories;
using PrancaBeauty.Application.Contracts.Currency;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.KeywordProducts;
using PrancaBeauty.Application.Contracts.PostingRestrictions;
using PrancaBeauty.Application.Contracts.ProductProperties;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<DateTime, string>().ConvertUsing(a => a.ToString("yyyy-MM-dd HH:mm"));
            CreateMap<double, string>().ConvertUsing(a => a.ToString(new CultureInfo("en-US")));
            CreateMap<viCompo_AccountSettings, InpSaveAccountSettingUserDetails>();
            CreateMap<viCompo_AddAddress, InpAddAddress>();
            CreateMap<OutGetAddressByUserIdForManage, vmCompo_ListAddress>();
            CreateMap<OutGetProductsForManage, vmProductList>();
            CreateMap<vmGetAllSellerForManageByProductId, vmListSellers>().ForMember(x => x.Date,
                                                                                          opt => opt.MapFrom(src => ((DateTime)src.Date).ToString("yyyy-MM-dd")));
            CreateMap<OutGetAddressDetails, viCompo_EditAddress>();
            CreateMap<viCompo_EditAddress, InpEditAddress>();
            CreateMap<viAddCategory, InpAddCategory>();
            CreateMap<viAddCategory_Translate, InpAddCategory_Translate>();
            CreateMap<viEditCategory, InpSaveEdit>();
            CreateMap<viEditCategory_Translate, InpSaveEdit_Translate>();
            CreateMap<viCompo_ModalFileList, InpGetFileListForFileManager>();

            CreateMap<viAddProduct, InpAddProdcut>();
            CreateMap<viAddProduct_Properties, InpAddProduct_Properties>();
            CreateMap<viAddProduct_Keywords, InpAddProduct_Keywords>();
            CreateMap<viAddProduct_Variants, InpAddProduct_Variants>();
            CreateMap<viAddProduct_PostingRestrictions, InpAddProduct_PostingRestrictions>();

            CreateMap<Application.Contracts.Categories.OutGetListForAdminPage, vmCategoriesList>();
            CreateMap<Application.Contracts.Categories.OutGetForEdit, viEditCategory>();
            CreateMap<OutGetForEdit_Translate, viEditCategory_Translate>();
            CreateMap<Application.Contracts.Products.OutGetForEdit, viEditProduct>().ForMember(x => x.Date,
                                                                                                    opt => opt.MapFrom(src => ((DateTime)src.Date).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"))));

            CreateMap<Application.Contracts.Countries.OutGetListForCombo, vmCompo_Combo_Countries>();
            CreateMap<Application.Contracts.Province.OutGetListForCombo, vmCompo_Combo_Province>();
            CreateMap<Application.Contracts.City.OutGetListForCombo, vmCompo_Combo_Cities>();
            CreateMap<Application.Contracts.Categories.OutGetListForCombo, vmCompo_Combo_Categories>();
            CreateMap<Application.Contracts.Users.OutGetListForCombo, vmCompo_Combo_Users>();
            CreateMap<Application.Contracts.FileTypes.outGetListForCombo, vmCompo_ComboFileTypes>();
            CreateMap<Application.Contracts.ProductTopics.OutGetListForCombo, vmCompo_Combo_Topics>();
            CreateMap<Application.Contracts.ProductVariants.outGetLstForCombo, vmCompo_Combo_ProductVariants>();
            CreateMap<Application.Contracts.Guarantee.OutGetListForCombo, vmCompo_Combo_Guarantee>();
            CreateMap<OutGetMainByCountryId, vmCompo_Input_Price>();
            CreateMap<outGetFileDetailsForFileSelector, vmCompo_FileSelector>();
            CreateMap<OutGetFileListForFileManager, vmCompo_ModalFileList>();
            CreateMap<OutGetForManageProduct, vmCompo_Properties>();
            CreateMap<OutGetKeywordByProductId, vmCompo_Input_Keywords>();
            CreateMap<OutGetAllVariantsByProductId, vmCompo_Variants>();
            CreateMap<OutGetAllPostingRestrictionsByProductId, vmCompo_PostingRestrictions>();


            CreateMap<viEditProduct, InpSaveEditProduct>();
            CreateMap<viEditProduct_Properties, InpSaveEditProduct_Properties>();
            CreateMap<viEditProduct_Keywords, InpSaveEditProduct_Keywords>();
            CreateMap<viEditProduct_Variants, InpSaveEditProduct_Variants>();
            CreateMap<viEditProduct_PostingRestrictions, InpSaveEditProduct_PostingRestrictions>();
            CreateMap<viAddProductSeller, InpAddSellerWithVariantsToProdcut>();
            CreateMap<viAddProductSeller_Items, InpAddSellerWithVariantsToProdcut_Variants>();

            CreateMap<viEditProductSeller, InpEditProductVariants>();
            CreateMap<viEditProductSeller_Items, InpEditProductVariants_Variants>();

        }
    }
}
