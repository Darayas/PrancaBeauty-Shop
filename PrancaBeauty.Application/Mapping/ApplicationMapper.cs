using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.KeywordProducts;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Mapping
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<InpAddKeywordsToProduct_LstKeywords, InpAddKeyword>();

            CreateMap<InpAddProdcut, InpAddVariantsToProduct>();
            //CreateMap<viAddProduct_Variants, InpAddVariantsToProduct_Variants>();

        }
    }
}
