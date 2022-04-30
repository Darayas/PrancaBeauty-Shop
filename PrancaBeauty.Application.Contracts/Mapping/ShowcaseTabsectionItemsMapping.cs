using AutoMapper;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ShowcaseTabsectionItemsMapping : Profile
    {
        public ShowcaseTabsectionItemsMapping()
        {
            // Add free items
            CreateMap<viAddSectionFreeItem, InpAddTabSectionFreeItem>();
            CreateMap<viAddSectionFreeItemTranslate, InpAddTabSectionFreeItemTranslate>();

            CreateMap<viAddSectionProductItem, InpAddTabSectionProductItem>();
            CreateMap<viAddSectionCategoryItem, InpAddTabSectionCategoryItem>();
            CreateMap<viAddSectionKeywordItem, InpAddTabSectionKeywordItem>();

            CreateMap<OutGetListShowcaseTabSectionItemForAdminPage, vmListShowcaseTabSectionItem>();

            // Edit FreeItem
            CreateMap<OutGetFreeItemForEdit, viEditSectionFreeItem>();
            CreateMap<OutGetFreeItemForEditTranslate, viEditSectionFreeItemTranslate>();
            CreateMap<viEditSectionFreeItem, InpSaveEditFreeItem>();
            CreateMap<viEditSectionFreeItemTranslate, InpSaveEditFreeItemTranslate>();

            CreateMap<OutGetProductItemForEdit, viEditSectionProductItem>();
            CreateMap<viEditSectionProductItem, InpSaveEditProductItem>();

            CreateMap<OutGetCategoryItemForEdit, viEditSectionCategoryItem>();
            CreateMap<viEditSectionCategoryItem, InpSaveEditCategoryItem>();
            
            CreateMap<OutGetKeywordItemForEdit, viEditSectionKeywordItem>();
            CreateMap<viEditSectionKeywordItem, InpSaveEditKeywordItem>();
        }
    }
}
