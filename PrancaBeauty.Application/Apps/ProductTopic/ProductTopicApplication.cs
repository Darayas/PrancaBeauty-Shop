using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductTopics;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductTopic
{
    public class ProductTopicApplication : IProductTopicApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductTopicRepository _ProductTopicRepository;

        public ProductTopicApplication(IProductTopicRepository productTopicRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _ProductTopicRepository = productTopicRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductTopicRepository.Get
                                                         .Select(a => new OutGetListForCombo()
                                                         {
                                                             Id = a.Id.ToString(),
                                                             Name = a.Name,
                                                             Title = a.tblProductTopic_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                             ImgUrl = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                     + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                     + a.tblFiles.tblFilePaths.Path
                                                                     + a.tblFiles.FileName
                                                         })
                                                         .Where(a => Input.Text != null ? a.Title.Contains(Input.Text) : true)
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
