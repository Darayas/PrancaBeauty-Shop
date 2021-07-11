using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Exceptions;
using PrancaBeauty.Domin.FileServer.FileAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Files
{
    public class FileApplication : IFileApplication
    {
        private readonly ILogger _Logger;
        private readonly IFileRepository _FileRepository;
        public FileApplication(IFileRepository fileRepository, ILogger logger)
        {
            _FileRepository = fileRepository;
            _Logger = logger;
        }

        public async Task<OperationResult> AddFileAsync(InpAddFile Input)
        {
            try
            {
                if (Input is null)
                    throw new ArgumentInvalidException("Input cant be null");

                if (await CheckExsitAsync(Input.FileServerId, Input.Path, Input.FileName))
                    return new OperationResult().Failed("FileInfoIsDuplicated");

                tblFiles tFile = new tblFiles()
                {
                    Id = Guid.Parse(Input.Id),
                    Date = DateTime.Now,
                    FileServerId = Guid.Parse(Input.FileServerId),
                    UserId = Input.UserId != null ? Guid.Parse(Input.UserId) : null,
                    FileName = Input.FileName,
                    IsPrivate = Input.IsPrivate,
                    MimeType = Input.MimeType,
                    Path = Input.Path,
                    SizeOnDisk = Input.SizeOnDisk,
                    Title = Input.Title
                };

                await _FileRepository.AddAsync(tFile, default, true);

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

        private async Task<bool> CheckExsitAsync(string FileServerId = null, string Path = null, string FileName = null)
        {
            if (FileServerId == null && Path == null && FileName == null)
                throw new ArgumentInvalidException();

            return await _FileRepository.Get
                                        .Where(a => FileServerId != null ? a.FileServerId == Guid.Parse(FileServerId) : true)
                                        .Where(a => Path != null ? a.Path == Path : true)
                                        .Where(a => FileName != null ? a.FileName == FileName : true)
                                        .AnyAsync();
        }

        public async Task<OutGetFileInfo> GetFileInfoAsync(string FileId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FileId))
                    throw new ArgumentInvalidException($"'{nameof(FileId)}' cannot be null or whitespace.");

                var qData = await _FileRepository.Get
                                                .Where(a => a.Id == Guid.Parse(FileId))
                                                .Select(a => new OutGetFileInfo
                                                {

                                                })
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
    }
}
