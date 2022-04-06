using AutoMapper;
using Framework.Common.ExMethods;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Address;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Currency;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Files;
using PrancaBeauty.Application.Contracts.ApplicationDTO.KeywordProducts;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PostingRestrictions;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProdcutReviews;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAsks;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductProperties;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewsAttributes;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductSellers;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sellers;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Users;
using PrancaBeauty.Application.Contracts.ProductAsks;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Globalization;

namespace PrancaBeauty.Application.Contracts.Mapping
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
                                                                                          opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")));
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

            CreateMap<ApplicationDTO.Categories.OutGetListForAdminPage, vmCategoriesList>();
            CreateMap<ApplicationDTO.Categories.OutGetForEdit, viEditCategory>();
            CreateMap<OutGetForEdit_Translate, viEditCategory_Translate>();
            CreateMap<ApplicationDTO.Products.OutGetForEdit, viEditProduct>().ForMember(x => x.Date,
                                                                                                    opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"))));

            CreateMap<ApplicationDTO.Countries.OutGetListForCombo, vmCompo_Combo_Countries>();
            CreateMap<ApplicationDTO.Province.OutGetListForCombo, vmCompo_Combo_Province>();
            CreateMap<ApplicationDTO.City.OutGetListForCombo, vmCompo_Combo_Cities>();
            CreateMap<ApplicationDTO.Categories.OutGetListForCombo, vmCompo_Combo_Categories>();
            CreateMap<ApplicationDTO.Users.OutGetListForCombo, vmCompo_Combo_Users>();
            CreateMap<ApplicationDTO.FileTypes.outGetListForCombo, vmCompo_ComboFileTypes>();
            CreateMap<ApplicationDTO.ProductTopics.OutGetListForCombo, vmCompo_Combo_Topics>();
            CreateMap<ApplicationDTO.ProductVariants.outGetLstForCombo, vmCompo_Combo_ProductVariants>();
            CreateMap<ApplicationDTO.Guarantee.OutGetListForCombo, vmCompo_Combo_Guarantee>();
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
            CreateMap<OutGetProductForDetails, vmProductDetails>();
            CreateMap<OutGetProductForDetails_Media, vmProductDetails_Media>();
            CreateMap<OutGetProductForDetails_Properties, vmProductDetails_Properties>();

            CreateMap<viEditProductSeller, InpEditProductVariants>();
            CreateMap<viEditProductSeller_Items, InpEditProductVariants_Variants>();
            CreateMap<OutGetListSellerForCombo, vmCompo_Combo_Sellers>();
            CreateMap<OutGetAllVariantsByProductId, vmGetProductSellerVariants>();
            CreateMap<OutGetSummaryBySellerId, vmProductSellerDetails>().ForMember(x => x.DateTime, opt => opt.MapFrom(src => src.DateTime.ToString("yyyy-MM-dd")));

            CreateMap<OutGetReviewsForProductDetails, vmCompo_ListProductReviews>();
            CreateMap<OutGetReviewsForProductDetailsItems, vmCompo_ListProductReviewsItems>()
                                                                                   .ForMember(a => a.Date, a => a.MapFrom(b => b.Date.ToString("dd MMMM yyyy")));
            CreateMap<OutGetReviewsForProductDetailsMedia, vmCompo_ListProductReviewsMedia>();
            CreateMap<OutGetReviewsForProductDetailsAttributes, vmCompo_ListProductReviewsAttributes>()
                                                                                    .ForMember(a => a.Avg, a => a.MapFrom(b => Math.Round(b.Avg)));
            CreateMap<OutGetReviewsForProductDetailsSellers, vmGetReviewsForProductDetailsSellers>();

            CreateMap<OutGetAttributesByTopicId, viCompo_ProductReviewAttributes>();


            CreateMap<viCompo_AddProductReviews, InpAddReviewFromUser>();
            CreateMap<viCompo_AddProductReviewsAttributes, InpAddReviewFromUserAttributes>();

            CreateMap<viCompo_AddProductAsk, InpAddNewAsk>();
            CreateMap<OutGetListAsks, vmCompo_ListProductAsks>();
            CreateMap<OutGetListAsks_Answer, vmGetListAsks_Answer>();

            CreateMap<OutGetAllProductVariantsForProductDetails, vmCompo_ProductVariantItem>();
            CreateMap<OutGetAllProductVariantsForProductDetailsItem, vmLstCompo_ProductVariantItem>();

            CreateMap<OutGetListSellerByVariantValue, vmCompo_ProductSellers>()
                                                                 .ForMember(a => a.MainPrice, a => a.MapFrom(b => b.MainPrice.ToN3()))
                                                                 .ForMember(a => a.PercentSavePrice, a => a.MapFrom(b => b.PercentSavePrice.ToString(new CultureInfo("en-US"))))
                                                                 .ForMember(a => a.OldPrice, a => a.MapFrom(b => b.OldPrice.ToN3()));

            CreateMap<OutGetProductPriceByVariantId, vmCompo_ProductSelectedPrice>()
                                                                 .ForMember(a => a.ProductOldPrice, a => a.MapFrom(b => b.ProductOldPrice.ToN3()))
                                                                 .ForMember(a => a.ProductPrice, a => a.MapFrom(b => b.ProductPrice.ToN3()));
        }
    }
}
