using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
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
            CreateMap<OutGetListShowcaseTabSectionForAdminPage, OutGetListShowcaseTabSectionForAdminPage>();
        }
    }
}
