﻿using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariants;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariants
{
    public class ProductVariantsApplication : IProductVariantsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductVariantsRepository _ProductVariantsRepository;

        public ProductVariantsApplication(IProductVariantsRepository productVariantsRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _ProductVariantsRepository = productVariantsRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<List<outGetProductVariantsLstForCombo>> GetLstForComboAsync(InpGetLstForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantsRepository.Get
                                                           .Select(a => new outGetProductVariantsLstForCombo
                                                           {
                                                               Id = a.Id.ToString(),
                                                               Title = a.tblProductVariants_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single()
                                                           })
                                                           .Where(a => Input.Text != null ? a.Title.Contains(Input.Text) : true)
                                                           .ToListAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<List<OutGetVariantsForSearchByCateId>> GetVariantsForSearchByCateIdAsync(InpGetVariantsForSearchByCateId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = from a in _ProductVariantsRepository.Get
                            let Items = a.tblProductVariantItems.Where(b => b.tblProducts.CategoryId==Input.CategoryId.ToGuid())
                            where Items.Any()
                            select new OutGetVariantsForSearchByCateId
                            {
                                Id=a.Id.ToString(),
                                Title=a.tblProductVariants_Translates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single(),
                                ValueItems= Items
                                                .Select(b => new OutGetVariantsForSearchByCateIdItem
                                                {
                                                    Title=b.Title,
                                                    Value=b.Value
                                                })
                                                .Distinct()
                                                .ToList()
                            };

                return await qData.ToListAsync();

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
