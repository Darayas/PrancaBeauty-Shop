﻿using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.FilePath;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FilePath
{
    public class FilePathApplication : IFilePathApplication
    {
        private readonly ILogger _Logger;
        private readonly IFilePathRepository _FilePathRepository;

        public FilePathApplication(IFilePathRepository filePathRepository, ILogger logger)
        {
            _FilePathRepository = filePathRepository;
            _Logger = logger;
        }

        public async Task<string> GetIdByPathAsync(InpGetIdByPath Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState();
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
            Input.CheckModelState();
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
                Input.CheckModelState();
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
