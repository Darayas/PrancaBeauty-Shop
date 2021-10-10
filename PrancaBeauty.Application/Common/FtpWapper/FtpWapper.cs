using Framework.Application.Services.FTP;
using Framework.Application.Services.Security.AntiShell;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Apps.FilePath;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Apps.FileServer;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Common.FtpWapper
{
    public class FtpWapper : IFtpWapper
    {
        private readonly ILogger _Logger;
        private readonly IFtpClient _FtpClient;
        private readonly IFileServerApplication _FileServerApplication;
        private readonly IFileApplication _FileApplication;
        private readonly IFilePathApplication _FilePathApplication;
        private readonly IAniShell _AniShell;
        public FtpWapper(IFtpClient ftpClient, ILogger logger, IFileServerApplication fileServerApplication, IFileApplication fileApplication, IAniShell aniShell, IFilePathApplication filePathApplication)
        {
            _FtpClient = ftpClient;
            _Logger = logger;
            _FileServerApplication = fileServerApplication;
            _FileApplication = fileApplication;
            _AniShell = aniShell;
            _FilePathApplication = filePathApplication;
        }

        public async Task<OperationResult> UplaodCategoryImgAsync(IFormFile _FormFile, string _UserId)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException(nameof(_FormFile));

                if (string.IsNullOrWhiteSpace(_UserId))
                    throw new ArgumentInvalidException(nameof(_UserId));

                var _ValidFileType = (await _AniShell.GetRealExtentionAsync(_FormFile));
                if (_ValidFileType == default)
                    return new OperationResult().Failed("FileFormatIsNotAllowed");

                string _Path = $"/{_ValidFileType.Item2}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/";
                string _FileName = new Guid().SequentialGuid().ToString().Replace("-", "") + "." + _ValidFileType.Item1;

                var _Result = await UploadFileAsync(_FormFile, _UserId, _Path, _FileName, _FormFile.FileName.Split('.').First());
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

        public async Task<OperationResult> UplaodProfileImgAsync(IFormFile _FormFile, string _UserId)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException(nameof(_FormFile));

                if (string.IsNullOrWhiteSpace(_UserId))
                    throw new ArgumentInvalidException(nameof(_UserId));

                var _ValidFileType = (await _AniShell.GetRealExtentionAsync(_FormFile));
                if (_ValidFileType == default)
                    return new OperationResult().Failed("FileFormatIsNotAllowed");

                string _Path = $"/{_ValidFileType.Item2}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/";
                string _FileName = new Guid().SequentialGuid().ToString().Replace("-", "") + "." + _ValidFileType.Item1;

                var _Result = await UploadFileAsync(_FormFile, _UserId, _Path, _FileName, _FormFile.FileName.Split('.').First());
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

        private async Task<bool> MakeDirAsync(string ServerName, string Path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ServerName))
                    throw new ArgumentInvalidException($"'{nameof(ServerName)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(Path))
                    throw new ArgumentInvalidException($"'{nameof(Path)}' cannot be null or whitespace.");

                var qServer = await _FileServerApplication.GetServerDetailsAsync(ServerName);
                if (qServer == null)
                    return false;

                string[] Dirs = Path.Split("/");
                string DirCheck = "";

                foreach (var item in Dirs)
                {
                    if (item != null && item != "")
                    {
                        DirCheck += $"/{item}";
                        if (!await _FtpClient.CheckDirectoryExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, Path, qServer.FtpUserName, qServer.FtpPassword))
                        {
                            await _FtpClient.CreateDirectoryAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, DirCheck, qServer.FtpUserName, qServer.FtpPassword);
                        }
                    }
                }

                return true;
            }
            catch (ArgumentInvalidException ex)
            {
                return false;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public async Task<bool> RemoveFileAsync(string FileId, string UserId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FileId))
                    throw new ArgumentInvalidException($"'{nameof(FileId)}' cannot be null or whitespace.");

                var qFile = await _FileApplication.GetFileInfoAsync(FileId);
                if (qFile == null)
                    return false;

                var qServer = await _FileServerApplication.GetServerDetailsAsync(qFile.FileServerName);
                if (qServer == null)
                    return false;

                // برسی وجود فایل در ftp سرور
                if (!await _FtpClient.CheckFileExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, qFile.Path, qFile.FileName, qServer.FtpUserName, qServer.FtpPassword))
                    return false;

                // حذف فایل در Ftp سرور
                if (!await _FtpClient.RemoveAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, qFile.Path, qFile.FileName, qServer.FtpUserName, qServer.FtpPassword))
                    return false;

                // حذف فایل از دیتابیس
                var _Result = await _FileApplication.RemoveFileAsync(FileId, UserId);
                if (_Result.IsSucceeded)
                    return true;
                else
                {
                    _Logger.Warning($"فایل از روی حافظه با موفقیت پاک شد اما حذف اطلاعات مربوط به فایل در دیتابیس با شکست مواجه شد. شناسه رکورد فایل: {FileId}");
                    return false;
                }
            }
            catch (ArgumentInvalidException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public async Task<OperationResult> UploadFromFileManagerAsync(IFormFile _FormFile, string _UserId)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException("FormFile cant be null.");

                if (string.IsNullOrWhiteSpace(_UserId))
                    throw new ArgumentInvalidException("UserId cant be null.");

                var _ValidFileType = (await _AniShell.GetRealExtentionAsync(_FormFile));
                if (_ValidFileType == default)
                    return new OperationResult().Failed("FileFormatIsNotAllowed");

                string _Path = $"/{_ValidFileType.Item2}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/";
                string _FileName = new Guid().SequentialGuid().ToString().Replace("-", "") + "." + _ValidFileType.Item1;

                var _Result = await UploadFileAsync(_FormFile, _UserId, _Path, _FileName, _FormFile.FileName.Split('.').First());
                if (_Result.IsSucceeded)
                    return new OperationResult().Succeeded(_Result.Message);
                else
                    return new OperationResult().Failed(_Result.Message);
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

                var qServer = await _FileServerApplication.GetServerDetailsAsync(_BestServer.Message);

                #region Path

                if (!await _FtpClient.CheckDirectoryExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, _Path, qServer.FtpUserName, qServer.FtpPassword))
                    await MakeDirAsync(qServer.Name, _Path);

                // برسی وجود دایرکتوری در دیتابیس
                if (!await _FilePathApplication.CheckDirectoryExistAsync(qServer.Id, _Path))
                    await _FilePathApplication.MakePathAsync(qServer.Id, _Path);

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

                var qData = await _FileServerApplication.GetBestServerNameByFileSizeAsync(_FileSize);
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
