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

            CreateMap<OutGetListShowcaseTabSectionItemForAdminPage, vmListShowcaseTabSectionItem>();

            CreateMap<viAddSectionProductItem, InpAddTabSectionProductItem>();

        }
    }
}
