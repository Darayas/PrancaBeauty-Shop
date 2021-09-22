using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<string> GetIdByPathAsync(string FileServerId, string Path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FileServerId))
                    throw new ArgumentInvalidException($"'{nameof(FileServer)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(Path))
                    throw new ArgumentInvalidException($"'{nameof(Path)}' cannot be null or whitespace.");

                var qData = await _FilePathRepository.Get.Where(a => a.FileServerId == Guid.Parse(FileServerId))
                                                         .Where(a => a.Path == Path)
                                                         .Select(a => a.Id.ToString())
                                                         .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<bool> CheckDirectoryExistAsync(string FileServerId, string Path)
        {
            return await _FilePathRepository.Get.Where(a => a.FileServerId == Guid.Parse(FileServerId))
                                                .Where(a => a.Path == Path)
                                                .AnyAsync();
        }

        public async Task<OperationResult> MakePathAsync(string FileServerId, string Path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FileServerId))
                    throw new ArgumentInvalidException($"'{nameof(FileServer)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(Path))
                    throw new ArgumentInvalidException($"'{nameof(Path)}' cannot be null or whitespace.");

                var tFilePath = new tblFilePaths()
                {
                    Id = new Guid().SequentialGuid(),
                    FileServerId = Guid.Parse(FileServerId),
                    Path = Path
                };

                await _FilePathRepository.AddAsync(tFilePath, default, true);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
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
