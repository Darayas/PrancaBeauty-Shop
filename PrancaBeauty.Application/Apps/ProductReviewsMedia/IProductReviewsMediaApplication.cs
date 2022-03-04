﻿using PrancaBeauty.Application.Contracts.ProdcutReviewsMedia;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsMedia
{
    public interface IProductReviewsMediaApplication
    {
        Task<OperationResult> AddMediaToReviewAsync(InpAddMediaToReview Input);
    }
}