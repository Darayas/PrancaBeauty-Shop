﻿using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.FilePath;
using PrancaBeauty.Application.Apps.FileTypes;
using PrancaBeauty.Application.Contracts.FilePath;
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
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IFileRepository _FileRepository;
        private readonly IFileTypeApplication _FileTypeApplication;
        private readonly IFilePathApplication _FilePathApplication;
        public FileApplication(IFileRepository fileRepository, ILogger logger, IFileTypeApplication fileTypeApplication, IFilePathApplication filePathApplication, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _FileRepository = fileRepository;
            _Logger = logger;
            _FileTypeApplication = fileTypeApplication;
            _FilePathApplication = filePathApplication;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddFileAsync(InpAddFile Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                // برسی تکراری نبود فایل
                if (await CheckExsitAsync(Input.FileServerId, Input.Path, Input.FileName))
                    return new OperationResult().Failed("FileInfoIsDuplicated");

                // اعتبار سنجی نوع فایل
                string FileTypeId = await _FileTypeApplication.GetIdByMimeTypeAsync(new Contracts.FileTypes.InpGetIdByMimeType { MimeType = Input.MimeType.ToLower() });
                if (FileTypeId == null)
                    return new OperationResult().Failed("MimeTypeIsInvalid");

                // اعتبار سنجی پوشه
                string FilePathId = await _FilePathApplication.GetIdByPathAsync(new InpGetIdByPath { FileServerId = Input.FileServerId, Path = Input.Path });
                if (FilePathId == null)
                    return new OperationResult().Failed("FilePathIsInvalid");

                tblFiles tFile = new tblFiles()
                {
                    Id = Guid.Parse(Input.Id),
                    Date = DateTime.Now,
                    FilePathId = Guid.Parse(FilePathId),
                    FileTypeId = Guid.Parse(FileTypeId),
                    UserId = Input.UserId != null ? Guid.Parse(Input.UserId) : null,
                    FileName = Input.FileName,
                    IsPrivate = Input.IsPrivate,
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
                                        .Where(a => FileServerId != null ? a.tblFilePaths.FileServerId == Guid.Parse(FileServerId) : true)
                                        .Where(a => Path != null ? a.tblFilePaths.Path == Path : true)
                                        .Where(a => FileName != null ? a.FileName == FileName : true)
                                        .AnyAsync();
        }

        public async Task<OutGetFileInfo> GetFileInfoAsync(InpGetFileInfo Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _FileRepository.Get
                                                .Where(a => a.Id == Guid.Parse(Input.FileId))
                                                .Where(a => Input.UserId != null ? a.UserId == Guid.Parse(Input.UserId) : true)
                                                .Select(a => new OutGetFileInfo
                                                {
                                                    Title = a.Title,
                                                    UserId = a.UserId.ToString(),
                                                    FileName = a.FileName,
                                                    FileServerId = a.tblFilePaths.FileServerId.ToString(),
                                                    FileServerName = a.tblFilePaths.tblFileServer.Name,
                                                    Date = a.Date,
                                                    IsPrivate = a.IsPrivate,
                                                    MimeType = a.tblFileTypes.MimeType,
                                                    Path = a.tblFilePaths.Path,
                                                    SizeOnDisk = a.SizeOnDisk
                                                })
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

        public async Task<OperationResult> RemoveFileAsync(InpRemoveFile Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion]

                var qData = await _FileRepository.Get
                                                 .Where(a => a.Id == Guid.Parse(Input.FileId))
                                                 .Where(a => Input.UserId != null ? a.UserId == Guid.Parse(Input.UserId) : true)
                                                 .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdNotFound");

                await _FileRepository.DeleteAsync(qData, default, true);

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

        public async Task<List<outGetFileDetailsForFileSelector>> GetFileDetailsForFileSelectorAsync(List<InpGetFileDetailsForFileSelector> Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _SelectedList = Input.Select(a => a.FileId).ToArray();

                var qData = await _FileRepository.Get
                                                 .Where(a => _SelectedList.Contains(a.Id.ToString()))
                                                 .Select(a => new outGetFileDetailsForFileSelector()
                                                 {
                                                     Id = a.Id.ToString(),
                                                     Title = a.Title,
                                                     FileName = a.FileName,
                                                     FileSize = a.SizeOnDisk,
                                                     MimeType = a.tblFileTypes.MimeType,
                                                     FileTypeIconUrl = a.tblFileTypes.IconUrl,
                                                     FileServerName = a.tblFilePaths.tblFileServer.Name,
                                                     DownloadUrl = a.tblFilePaths.tblFileServer.HttpDomin
                                                                   + a.tblFilePaths.tblFileServer.HttpPath
                                                                   + a.tblFilePaths.Path
                                                                   + a.FileName
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

        public async Task<(OutPagingData, List<OutGetFileListForFileManager>)> GetFileListForFileManagerAsync(InpGetFileListForFileManager Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _FileRepository.Get
                                           .Where(a => Input.UploaderUserId != null ? a.UserId == Guid.Parse(Input.UploaderUserId) : true)
                                           .Where(a => Input.FileTypeId != null ? a.FileTypeId == Guid.Parse(Input.FileTypeId) : true)
                                           .Where(a => Input.FileTitle != null ? a.Title.Contains(Input.FileTitle) : true)
                                           .OrderByDescending(a => a.Date)
                                           .Select(a => new OutGetFileListForFileManager
                                           {
                                               Id = a.Id.ToString(),
                                               Title = a.Title,
                                               FileSize = a.SizeOnDisk,
                                               MimeType = a.tblFileTypes.MimeType,
                                               FileTypeIconUrl = a.tblFileTypes.IconUrl,
                                               Date = a.Date,
                                               DownloadLink = a.tblFilePaths.tblFileServer.HttpDomin
                                                                    + a.tblFilePaths.tblFileServer.HttpPath
                                                                    + a.tblFilePaths.Path
                                                                    + a.FileName,
                                           });

                #region Sorting
                {
                    switch (Input.Sort)
                    {
                        case InpGetFileListForFileManagerSort.TitleAes:
                            {
                                qData = qData.OrderBy(a => a.Title);
                                break;
                            }
                        case InpGetFileListForFileManagerSort.TitleDes:
                            {
                                qData = qData.OrderByDescending(a => a.Title);
                                break;
                            }
                        case InpGetFileListForFileManagerSort.FileTypeAes:
                            {
                                qData = qData.OrderBy(a => a.MimeType);
                                break;
                            }
                        case InpGetFileListForFileManagerSort.FileTypeDes:
                            {
                                qData = qData.OrderByDescending(a => a.MimeType);
                                break;
                            }
                        case InpGetFileListForFileManagerSort.DateDes:
                            {
                                qData = qData.OrderByDescending(a => a.Date);
                                break;
                            }
                        case InpGetFileListForFileManagerSort.DateAes:
                            {
                                qData = qData.OrderBy(a => a.Date);
                                break;
                            }
                        default:
                            {
                                qData = qData.OrderByDescending(a => a.Date);
                                break;
                            }
                    }
                }
                #endregion

                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), Input.CurrentPage, Input.Take);
                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(Input.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<string> GetFileUrlAsync(InpGetFileUrl Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _FileRepository.Get
                                                .Where(a => a.Id == Guid.Parse(Input.FileId))
                                                .Where(a => Input.UserId != null ? a.UserId == Guid.Parse(Input.UserId) : true)
                                                .Select(a => new
                                                {
                                                    Url = a.tblFilePaths.tblFileServer.HttpDomin
                                                         + a.tblFilePaths.tblFileServer.HttpPath
                                                         + a.tblFilePaths.Path
                                                         + a.FileName
                                                })
                                                .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData.Url;
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
