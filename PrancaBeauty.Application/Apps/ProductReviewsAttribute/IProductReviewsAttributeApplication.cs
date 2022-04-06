﻿using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewsAttributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsAttribute
{
    public interface IProductReviewsAttributeApplication
    {
        Task<List<OutGetAttributesByTopicId>> GetAttributesByTopicIdAsync(InpGetAttributesByTopicId Input);
    }
}