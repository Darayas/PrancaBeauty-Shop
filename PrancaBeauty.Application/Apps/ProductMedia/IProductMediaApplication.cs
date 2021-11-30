﻿using PrancaBeauty.Application.Contracts.ProductMedia;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductMedia
{
    public interface IProductMediaApplication
    {
        Task<OperationResult> AddMediasToProductAsync(InpAddMediasToProduct Input);
    }
}