using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ShowCaseTabSectionMapping:Profile
    {
        public ShowCaseTabSectionMapping()
        {
            // List
            CreateMap<OutGetListShowcaseTabSectionForAdminPage, vmListShowcaseTabSection>();

            // Add
            CreateMap<viAddShowcaseTabSection, InpAddShowcaseTabSection>();

            // Edit
            CreateMap<OutGetTabSectionForEdit, viEditShowcaseTabSection>();
            CreateMap<viEditShowcaseTabSection, InpEditShowcaseTabSection>();
        }
    }
}
