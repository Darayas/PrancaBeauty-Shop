using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Contracts;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Keywords
{
    public class KeywordApplication : IKeywordApplication
    {
        private readonly ILogger _Logger;
        private readonly IKeywordRepository _KeywordRepository;
        public KeywordApplication(IKeywordRepository keywordRepository, ILogger logger)
        {
            _KeywordRepository = keywordRepository;
            _Logger = logger;
        }

        public async Task<bool> CheckExistByTitleAsync(string Title)
        {
            return await _KeywordRepository.Get.AnyAsync(a => a.Title == Title);
        }

        public async Task<string> GetIdByTitleAsync(string Title)
        {
            return await _KeywordRepository.Get.Where(a => a.Title == Title).Select(a => a.Id.ToString()).SingleAsync();
        }

        public async Task<OperationResult> AddKeywordAsync(string Title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Title))
                    throw new ArgumentInvalidException($"{nameof(Title)} cant be null or whitespace.");

                var tKeyword = new tblKeywords()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = Title.ToNormalizedUrl(),
                    Title = Title,
                    Description = ""
                };
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
