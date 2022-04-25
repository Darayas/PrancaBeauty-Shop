using AutoMapper;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
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

        }
    }
}
