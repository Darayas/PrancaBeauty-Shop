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

        public async Task<string> GetIdByMimeTypeAsync(string MimeType)
        {
            try
            {
                var qData = await _FileTypeRepository.Get.Where(a => a.MimeType == MimeType).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
                if (qData == null)
                    return null;

                return qData;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<List<outGetListForCombo>> GetListForComboAsync(string Title)
        {
            try
            {
                var qData = await _FileTypeRepository.Get
                                                     .Where(a => Title != null ? a.MimeType.Contains(Title) : true)
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
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
