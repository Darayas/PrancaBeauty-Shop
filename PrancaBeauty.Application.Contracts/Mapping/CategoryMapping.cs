using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<viAddCategory, InpAddCategory>();
            CreateMap<viAddCategory_Translate, InpAddCategory_Translate>();

            CreateMap<viEditCategory, InpSaveEdit>();
            CreateMap<viEditCategory_Translate, InpSaveEdit_Translate>();
            CreateMap<OutGetCategoryForEdit, viEditCategory>();
            CreateMap<OutGetForEdit_Translate, viEditCategory_Translate>();

            CreateMap<OutGetCategoryListForAdminPage, vmCategoriesList>();

            CreateMap<OutGetCategoryListForCombo, vmCompo_Combo_Categories>();

            CreateMap<viSearch, viCompoSearch_ProductList>();

        }
    }
}
