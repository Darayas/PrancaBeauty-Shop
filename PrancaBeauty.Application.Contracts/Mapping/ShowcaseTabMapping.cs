﻿using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Globalization;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ShowcaseTabMapping : Profile
    {
        public ShowcaseTabMapping()
        {
            // Add
            CreateMap<viAddShowcaseTab, InpAddShowcaseTab>();
            CreateMap<viAddShowcaseTab_Translate, InpAddShowcaseTab_Translate>();

            // Edit
            CreateMap<OutGetShowcaseTabForEdit, viEditShowcaseTab>()
                                     .ForMember(a => a.StartDate, a => a.MapFrom(b => b.StartDate.ToString("yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US"))))
                                     .ForMember(a => a.EndDate, a => a.MapFrom(b => b.EndDate.HasValue ? b.EndDate.Value.ToString("yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US")) : "")); ;
            CreateMap<OutGetShowcaseTabForEdit_Translate, viEditShowcaseTab_Translate>();
            CreateMap<viEditShowcaseTab, InpSaveEditShowcaseTab>();
            CreateMap<viEditShowcaseTab_Translate, InpSaveEditShowcaseTab_Translate>();

            // List
            CreateMap<OutGetListShowcaseTabForAdminPage, vmListShowcaseTabs>()
                                     .ForMember(a => a.StartDate, a => a.MapFrom(b => b.StartDate.ToString("yyyy/MM/dd HH:mm:ss")))
                                     .ForMember(a => a.EndDate, a => a.MapFrom(b => b.EndDate.HasValue ? b.EndDate.Value.ToString("yyyy/MM/dd HH:mm:ss") : ""));
        }
    }
}