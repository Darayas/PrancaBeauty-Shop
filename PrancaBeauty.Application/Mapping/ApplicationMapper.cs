using AutoMapper;
using PrancaBeauty.Application.Contracts.KeywordProducts;
using PrancaBeauty.Application.Contracts.Keywords;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
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
