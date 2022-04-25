using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Contracts;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Keywords
{
    public class KeywordApplication : IKeywordApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IKeywordRepository _KeywordRepository;
        public KeywordApplication(IKeywordRepository keywordRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _KeywordRepository = keywordRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<bool> CheckExistByTitleAsync(InpCheckExistByTitle Input)
        {
            return await _KeywordRepository.Get.AnyAsync(a => a.Title == Input.Title);
        }

        public async Task<string> GetIdByTitleAsync(InpGetIdByTitle Input)
        {
            return await _KeywordRepository.Get.Where(a => a.Title == Input.Title).Select(a => a.Id.ToString()).SingleAsync();
        }

        public async Task<OperationResult> AddKeywordAsync(InpAddKeyword Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var tKeyword = new tblKeywords()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = Input.Title.ToNormalizedUrl(),
                    Title = Input.Title,
                    Description = Input.Description
                };

                await _KeywordRepository.AddAsync(tKeyword, default, true);

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

        public async Task<List<OutGetKeywordListForCombo>> GetKeywordListForComboAsync(InpGetKeywordListForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _KeywordRepository.Get
                                                .Where(a => Input.Title!=null ? a.Title.Contains(Input.Title) : true)
                                                .Select(a => new OutGetKeywordListForCombo
                                                {
                                                    Id=a.Id.ToString(),
                                                    Title=a.Title.ToString()
                                                })
                                                .OrderBy(a=>a.Title)
                                                .Take(20)
                                                .ToListAsync();

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }
    }
}
