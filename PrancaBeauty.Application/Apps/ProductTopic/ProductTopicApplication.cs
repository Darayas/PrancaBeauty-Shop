using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProductTopics;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductTopic
{
    public class ProductTopicApplication : IProductTopicApplication
    {
        private readonly ILogger _Logger;
        private readonly IProductTopicRepository _ProductTopicRepository;

        public ProductTopicApplication(IProductTopicRepository productTopicRepository, ILogger logger)
        {
            _ProductTopicRepository = productTopicRepository;
            _Logger = logger;
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(string LangId, string Text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");
                
                var qData = await _ProductTopicRepository.Get
                                                         .Select(a => new OutGetListForCombo()
                                                         {
                                                             Id = a.Id.ToString(),
                                                             Name = a.Name,
                                                             Title = a.tblProductTopic_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single(),
                                                             ImgUrl = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                     + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                     + a.tblFiles.tblFilePaths.Path
                                                                     + a.tblFiles.FileName
                                                         })
                                                         .Where(a => Text != null ? a.Title.Contains(Text) : true)
                                                         .OrderBy(a => a.Title)
                                                         .Skip(0)
                                                         .Take(50)
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
    }
}
