﻿using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ShowcaseMapping : Profile
    {
        public ShowcaseMapping()
        {
            // Add
            CreateMap<viAddShowcase, InpAddShowcase>();
            CreateMap<viAddShowcase_Translate, InpAddShowcase_Translate>();

            // List
            CreateMap<OutGetListShowcaseForAdminPage, vmListShowcases>()
                                        .ForMember(a => a.StartDate, a => a.MapFrom(b =>  b.StartDate.ToString("yyyy/MM/dd HH:mm:ss")))
                                        .ForMember(a => a.EndDate, a => a.MapFrom(b => b.EndDate.HasValue ? b.EndDate.Value.ToString("yyyy/MM/dd HH:mm:ss") : ""));
        }
    }
}
