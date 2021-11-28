using AngleSharp.Css;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.PostingRestrictions;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Contracts;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PostingRestrictions
{
    public class PostingRestrictionsApplication : IPostingRestrictionsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IPostingRestrictionsRepository _PostingRestrictionsRepository;

        public PostingRestrictionsApplication(IPostingRestrictionsRepository postingRestrictionsRepository, ILogger logger, ILocalizer localizer)
        {
            _PostingRestrictionsRepository = postingRestrictionsRepository;
            _Logger = logger;
            _Localizer = localizer;
        }

        public async Task<OperationResult> AddPostingRestrictionsToProductAsync(InpAddPostingRestrictionsToProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_Localizer);
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
