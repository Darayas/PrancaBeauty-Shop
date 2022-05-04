using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.FilePath;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FilePath
{
    public class FilePathApplication : IFilePathApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IFilePathRepository _FilePathRepository;

        public FilePathApplication(IFilePathRepository filePathRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _FilePathRepository = filePathRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<string> GetIdByPathAsync(InpGetIdByPath Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _FilePathRepository.Get.Where(a => a.FileServerId == Guid.Parse(Input.FileServerId))
                                                         .Where(a => a.Path == Input.Path)
                                                         .Select(a => a.Id.ToString())
                                                         .SingleOrDefaultAsync();

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

        public async Task<bool> CheckDirectoryExistAsync(InpCheckDirectoryExist Input)
        {
            #region Vlidation
            Input.CheckModelState(_ServiceProvider);
            #endregion

           

            return await _FilePathRepository.Get.Where(a => a.FileServerId == Guid.Parse(Input.FileServerId))
                                                .Where(a => a.Path == Input.Path)
                                                .AnyAsync();
        }

        public async Task<OperationResult> MakePathAsync(InpMakePath Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var tFilePath = new tblFilePaths()
                {
                    Id = new Guid().SequentialGuid(),
                    FileServerId = Guid.Parse(Input.FileServerId),
                    Path = Input.Path
                };

                await _FilePathRepository.AddAsync(tFilePath, default, true);

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
