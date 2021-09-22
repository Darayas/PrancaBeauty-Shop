using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.FileTypes;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.Results;
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
        private readonly IFileTypeApplication _FileTypeApplication;
        public FileApplication(IFileRepository fileRepository, ILogger logger, IFileTypeApplication fileTypeApplication)
        {
            _FileRepository = fileRepository;
            _Logger = logger;
            _FileTypeApplication = fileTypeApplication;
        }

        public async Task<OperationResult> AddFileAsync(InpAddFile Input)
        {
            try
            {
                if (Input is null)
                    throw new ArgumentInvalidException("Input cant be null");

                if (await CheckExsitAsync(Input.FileServerId, Input.Path, Input.FileName))
                    return new OperationResult().Failed("FileInfoIsDuplicated");

                // اعتبار سنجی نوع فایل
                string FileTypeId = await _FileTypeApplication.GetIdByMimeTypeAsync(Input.MimeType.ToLower());
                if (FileTypeId == null)
                    return new OperationResult().Failed("MimemyTypeIsInvalid");

                tblFiles tFile = new tblFiles()
                {
                    Id = Guid.Parse(Input.Id),
                    Date = DateTime.Now,
                    FileServerId = Guid.Parse(Input.FileServerId),
                    FileTypeId = Guid.Parse(FileTypeId),
                    UserId = Input.UserId != null ? Guid.Parse(Input.UserId) : null,
                    FileName = Input.FileName,
                    IsPrivate = Input.IsPrivate,
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

        public async Task<OutGetFileInfo> GetFileInfoAsync(string FileId, string UserId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FileId))
                    throw new ArgumentInvalidException($"'{nameof(FileId)}' cannot be null or whitespace.");

                var qData = await _FileRepository.Get
                                                .Where(a => a.Id == Guid.Parse(FileId))
                                                .Where(a => UserId != null ? a.UserId == Guid.Parse(UserId) : true)
                                                .Select(a => new OutGetFileInfo
                                                {
                                                    Title = a.Title,
                                                    UserId = a.UserId.ToString(),
                                                    FileName = a.FileName,
                                                    FileServerId = a.FileServerId.ToString(),
                                                    FileServerName = a.tblFileServer.Name,
                                                    Date = a.Date,
                                                    IsPrivate = a.IsPrivate,
                                                    MimeType = a.tblFileTypes.MimeType,
                                                    Path = a.Path,
                                                    SizeOnDisk = a.SizeOnDisk
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

        public async Task<OperationResult> RemoveFileAsync(string FileId, string UserId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FileId))
                    throw new ArgumentInvalidException($"'{nameof(FileId)}' cannot be null or whitespace.");

                var qData = await _FileRepository.Get
                                                 .Where(a => a.Id == Guid.Parse(FileId))
                                                 .Where(a => UserId != null ? a.UserId == Guid.Parse(UserId) : true)
                                                 .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdNotFound");

                await _FileRepository.DeleteAsync(qData, default, true);

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
