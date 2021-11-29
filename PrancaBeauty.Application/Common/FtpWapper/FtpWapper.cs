﻿using Framework.Application.Services.FTP;
using Framework.Application.Services.Security.AntiShell;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Apps.FilePath;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Apps.FileServer;
using PrancaBeauty.Application.Contracts.Common.FtpWapper;
using PrancaBeauty.Application.Contracts.FilePath;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.FileServer;
using PrancaBeauty.Application.Contracts.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Common.FtpWapper
{
    public class FtpWapper : IFtpWapper
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IFtpClient _FtpClient;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IFileServerApplication _FileServerApplication;
        private readonly IFileApplication _FileApplication;
        private readonly IFilePathApplication _FilePathApplication;
        private readonly IAniShell _AniShell;
        public FtpWapper(IFtpClient ftpClient, ILogger logger, IFileServerApplication fileServerApplication, IFileApplication fileApplication, IAniShell aniShell, IFilePathApplication filePathApplication, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _FtpClient = ftpClient;
            _Logger = logger;
            _FileServerApplication = fileServerApplication;
            _FileApplication = fileApplication;
            _AniShell = aniShell;
            _FilePathApplication = filePathApplication;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> UplaodCategoryImgAsync(InpUplaodCategoryImg Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _ValidFileType = (await _AniShell.GetRealExtentionAsync(Input.FormFile));
                if (_ValidFileType == default)
                    return new OperationResult().Failed("FileFormatIsNotAllowed");

                string _Path = $"/{_ValidFileType.Item2}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/";
                string _FileName = new Guid().SequentialGuid().ToString().Replace("-", "") + "." + _ValidFileType.Item1;

                var _Result = await UploadFileAsync(Input.FormFile, Input.UserId, _Path, _FileName, Input.FormFile.FileName.Split('.').First());
                if (_Result.IsSucceeded)
                    return new OperationResult().Succeeded(_Result.Message);
                else
                    return new OperationResult().Failed(_Result.Message);
            }
            catch (FileFormatException ex)
            {
                _Logger.Error(ex);
                return null;
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

        public async Task<OperationResult> UplaodProfileImgAsync(InpUplaodProfileImg Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _ValidFileType = (await _AniShell.GetRealExtentionAsync(Input.FormFile));
                if (_ValidFileType == default)
                    return new OperationResult().Failed("FileFormatIsNotAllowed");

                string _Path = $"/{_ValidFileType.Item2}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/";
                string _FileName = new Guid().SequentialGuid().ToString().Replace("-", "") + "." + _ValidFileType.Item1;

                var _Result = await UploadFileAsync(Input.FormFile, Input.UserId, _Path, _FileName, Input.FormFile.FileName.Split('.').First());
                if (_Result.IsSucceeded)
                    return new OperationResult().Succeeded(_Result.Message);
                else
                    return new OperationResult().Failed(_Result.Message);
            }
            catch (FileFormatException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        private async Task<bool> MakeDirAsync(string ServerName, string Path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ServerName))
                    throw new ArgumentInvalidException($"'{nameof(ServerName)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(Path))
                    throw new ArgumentInvalidException($"'{nameof(Path)}' cannot be null or whitespace.");

                var qServer = await _FileServerApplication.GetServerDetailsAsync(new InpGetServerDetails { ServerName = ServerName });
                if (qServer == null)
                    return false;

                string[] Dirs = $"{qServer.FtpPath.Trim('/')}/{Path.Trim('/')}".Split("/");
                string DirCheck = "";

                foreach (var item in Dirs)
                {
                    if (item != null && item != "")
                    {
                        DirCheck += $"/{item}";
                        if (!await _FtpClient.CheckDirectoryExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, Path, qServer.FtpUserName, qServer.FtpPassword))
                        {
                            await _FtpClient.CreateDirectoryAsync(qServer.FtpHost, qServer.FtpPort, DirCheck, qServer.FtpUserName, qServer.FtpPassword);
                        }
                    }
                }

                return true;
            }
            catch (ArgumentInvalidException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public async Task<bool> RemoveFileAsync(Contracts.Common.FtpWapper.InpRemoveFile Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qFile = await _FileApplication.GetFileInfoAsync(new InpGetFileInfo { FileId = Input.FileId });
                if (qFile == null)
                    return false;

                var qServer = await _FileServerApplication.GetServerDetailsAsync(new InpGetServerDetails { ServerName = qFile.FileServerName });
                if (qServer == null)
                    return false;

                // برسی وجود فایل در ftp سرور
                if (!await _FtpClient.CheckFileExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, qFile.Path, qFile.FileName, qServer.FtpUserName, qServer.FtpPassword))
                    return false;

                // حذف فایل در Ftp سرور
                if (!await _FtpClient.RemoveAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, qFile.Path, qFile.FileName, qServer.FtpUserName, qServer.FtpPassword))
                    return false;

                // حذف فایل از دیتابیس
                var _Result = await _FileApplication.RemoveFileAsync(new Contracts.Files.InpRemoveFile { FileId = Input.FileId, UserId = Input.UserId });
                if (_Result.IsSucceeded)
                    return true;
                else
                {
                    _Logger.Warning($"فایل از روی حافظه با موفقیت پاک شد اما حذف اطلاعات مربوط به فایل در دیتابیس با شکست مواجه شد. شناسه رکورد فایل: {Input.FileId}");
                    return false;
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return false;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public async Task<OperationResult> UploadFromFileManagerAsync(InpUploadFromFileManager Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _ValidFileType = (await _AniShell.GetRealExtentionAsync(Input.FormFile));
                if (_ValidFileType == default)
                    return new OperationResult().Failed("FileFormatIsNotAllowed");

                string _Path = $"/{_ValidFileType.Item2}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/";
                string _FileName = new Guid().SequentialGuid().ToString().Replace("-", "") + "." + _ValidFileType.Item1;

                var _Result = await UploadFileAsync(Input.FormFile, Input.UserId, _Path, _FileName, Input.FormFile.FileName.Split('.').First());
                if (_Result.IsSucceeded)
                    return new OperationResult().Succeeded(_Result.Message);
                else
                    return new OperationResult().Failed(_Result.Message);
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

        private async Task<OperationResult> UploadFileAsync(IFormFile _FormFile, string UserId, string _Path, string _FileName, string _Title)
        {
            try
            {
                #region Validation Parameter
                if (_FormFile is null)
                    throw new ArgumentInvalidException("FormFile cant be null.");

                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException("UserId cant be null.");

                if (string.IsNullOrWhiteSpace(_Path))
                    throw new ArgumentInvalidException("_Path cant be null.");

                if (string.IsNullOrWhiteSpace(_FileName))
                    throw new ArgumentInvalidException("_FileName cant be null.");

                if (string.IsNullOrWhiteSpace(_Title))
                    throw new ArgumentInvalidException("_Title cant be null.");
                #endregion

                var _BestServer = await GetBestServerNameAsync(_FormFile.Length);
                if (_BestServer.IsSucceeded == false)
                    return new OperationResult().Failed(_BestServer.Message);

                var qServer = await _FileServerApplication.GetServerDetailsAsync(new InpGetServerDetails { ServerName = _BestServer.Message });

                #region Path

                if (!await _FtpClient.CheckDirectoryExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, _Path, qServer.FtpUserName, qServer.FtpPassword))
                    await MakeDirAsync(qServer.Name, _Path);

                // برسی وجود دایرکتوری در دیتابیس
                if (!await _FilePathApplication.CheckDirectoryExistAsync(new InpCheckDirectoryExist { FileServerId = qServer.Id, Path = _Path }))
                    await _FilePathApplication.MakePathAsync(new InpMakePath { FileServerId = qServer.Id, Path = _Path });

                #endregion

                var _Result = await _FtpClient.UploadAsync(_FormFile.OpenReadStream(), qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, _Path, _FileName, qServer.FtpUserName, qServer.FtpPassword);
                if (_Result == true)
                {
                    var _Id = new Guid().SequentialGuid().ToString();
                    await _FileApplication.AddFileAsync(new InpAddFile()
                    {
                        Id = _Id,
                        FileServerId = qServer.Id,
                        Path = $"/{_Path.Trim('/')}/",
                        FileName = _FileName,
                        UserId = null,
                        IsPrivate = false,
                        Title = _Title,
                        MimeType = (await _AniShell.GetRealExtentionAsync(_FormFile)).Item2,
                        SizeOnDisk = _FormFile.Length
                    });

                    return new OperationResult().Succeeded(_Id);
                }
                else
                {
                    #region حذف فایل
                    {
                        if (await _FtpClient.CheckFileExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, _Path, _FileName, qServer.FtpUserName, qServer.FtpPassword))
                            if (!await _FtpClient.RemoveAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, _Path, _FileName, qServer.FtpUserName, qServer.FtpPassword))
                                _Logger.Error($"فایل با مشخصات ذیل به صورت ناقص آپلود شد. ServerId:'{qServer.Id}', ServerPath: '{qServer.FtpPath}', Path:'{_Path}', FileName: {_FileName}");
                    }
                    #endregion
                    return new OperationResult().Failed("UploadWithError");
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        private async Task<OperationResult> GetBestServerNameAsync(long _FileSize)
        {
            try
            {
                if (_FileSize <= 0)
                    throw new ArgumentInvalidException("FileSize must be greater than zero bytes");

                var qData = await _FileServerApplication.GetBestServerNameByFileSizeAsync(new InpGetBestServerNameByFileSize { FileSize = _FileSize });
                if (qData == null)
                    return new OperationResult().Failed("ServersAreFull");

                return new OperationResult().Succeeded(qData);
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
