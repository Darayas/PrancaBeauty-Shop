﻿using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.PostingRestrictions;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Contracts;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Entites;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PostingRestrictions
{
    public class PostingRestrictionsApplication : IPostingRestrictionsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IPostingRestrictionsRepository _PostingRestrictionsRepository;

        public PostingRestrictionsApplication(IPostingRestrictionsRepository postingRestrictionsRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _PostingRestrictionsRepository = postingRestrictionsRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddPostingRestrictionsToProductAsync(InpAddPostingRestrictionsToProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                foreach (var item in Input.PostingRestrictions)
                {
                    var tPostingRestrictions = new tblPostingRestrictions()
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductId = Guid.Parse(Input.ProductId),
                        CountryId = Guid.Parse(item.CountryId),
                        Posting = item.Posting
                    };

                    await _PostingRestrictionsRepository.AddAsync(tPostingRestrictions, default, false);
                }

                await _PostingRestrictionsRepository.SaveChangeAsync();

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
