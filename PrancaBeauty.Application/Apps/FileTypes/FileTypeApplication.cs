using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.FileTypes;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileTypes
{
    public class FileTypeApplication : IFileTypeApplication
    {
        private readonly ILogger _Logger;
        private readonly IFileTypeRepository _FileTypeRepository;

        public FileTypeApplication(IFileTypeRepository fileTypeRepository, ILogger logger)
        {
            _FileTypeRepository = fileTypeRepository;
            _Logger = logger;
        }

        public async Task<string> GetIdByMimeTypeAsync(InpGetIdByMimeType Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();
                #endregion

                var qData = await _FileTypeRepository.Get.Where(a => a.MimeType == Input.MimeType).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
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

        public async Task<List<outGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();
                #endregion

                var qData = await _FileTypeRepository.Get
                                                     .Where(a => Input.Title != null ? a.MimeType.Contains(Input.Title) : true)
                                                     .Select(a => new outGetListForCombo
                                                     {
                                                         Id = a.Id.ToString(),
                                                         MimeType = a.MimeType,
                                                         FileEx = a.Extentions,
                                                         ImgUrl = a.IconUrl
                                                     })
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

        public async Task<string[]> GetAllFileExtentionAsync()
        {
            try
            {
                var qData = await _FileTypeRepository.Get
                                                     .Select(a => a.Extentions)
                                                     .ToArrayAsync();

                return qData;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<string[]> GetAllFileMimeTypeAsync()
        {
            try
            {
                var qData = await _FileTypeRepository.Get
                                                     .Select(a => a.MimeType)
                                                     .ToArrayAsync();

                return qData;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
