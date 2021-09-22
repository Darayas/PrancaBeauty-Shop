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

        public async Task<string> UplaodCategoryImgAsync(IFormFile _FormFile, string _FileName = null)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException(nameof(_FormFile));

                var qServer = await _FileServerApplication.GetServerDetailsAsync("Public");
                if (qServer == null)
                    return null;

                #region File Name
                string FileName = null;
                if (_FileName == null)
                {
                    string FileEx = (await _AniShell.GetRealExtentionAsync(_FormFile)).Item1;
                    if (FileEx == null)
                        return null;

                    FileName = new Guid().SequentialGuid().ToString() + "." + FileEx;
                }
                else
                {
                    string FileEx = (await _AniShell.GetRealExtentionAsync(_FormFile)).Item1;
                    if (FileEx == null)
                        return null;

                    FileName = _FileName + "-" + new Random().Next(1000, 99999) + "." + FileEx;
                }
                #endregion

                #region Path
                string Path = $"/Img/Category/{DateTime.Now.Month}/{DateTime.Now.Day}";

                // برسی وجود دایرکتوری روی سرور
                if (!await _FtpClient.CheckDirectoryExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, Path, qServer.FtpUserName, qServer.FtpPassword))
                    await MakeDirAsync(qServer.Name, Path);

                // برسی وجود دایرکتوری در دیتابیس
                if (!await _FilePathApplication.CheckDirectoryExistAsync(qServer.Id, Path))
                    await _FilePathApplication.MakePathAsync(qServer.Id, Path);
                #endregion

                var _Result = await _FtpClient.UploadAsync(_FormFile.OpenReadStream(), qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, Path, FileName, qServer.FtpUserName, qServer.FtpPassword);
                if (_Result == true)
                {
                    var _Id = new Guid().SequentialGuid().ToString();
                    await _FileApplication.AddFileAsync(new InpAddFile()
                    {
                        Id = _Id,
                        FileServerId = qServer.Id,
                        Path = $"/{Path.Trim('/')}/",
                        FileName = FileName,
                        UserId = null,
                        IsPrivate = false,
                        Title = $"تصویر دسته - {FileName}",
                        MimeType = (await _AniShell.GetRealExtentionAsync(_FormFile)).Item2,
                        SizeOnDisk = _FormFile.Length
                    });

                    return _Id;
                }
                else
                {
                    throw new Exception("قادر به اپلود فایل دسته نبودیم. لطفا اطلاعات خطای قبلی را برسی نمایید");
                }
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

        public async Task<string> UplaodProfileImgAsync(IFormFile _FormFile, string _FileName = null)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException(nameof(_FormFile));

                var qServer = await _FileServerApplication.GetServerDetailsAsync("Public");
                if (qServer == null)
                    return null;

                #region File Name
                string FileName = null;
                if (_FileName == null)
                {
                    string FileEx = (await _AniShell.GetRealExtentionAsync(_FormFile)).Item1;
                    if (FileEx == null)
                        return null;

                    FileName = new Guid().SequentialGuid().ToString() + "." + FileEx;
                }
                else
                {
                    string FileEx = (await _AniShell.GetRealExtentionAsync(_FormFile)).Item1;
                    if (FileEx == null)
                        return null;

                    FileName = _FileName + "-" + new Random().Next(1000, 99999) + "." + FileEx;
                }
                #endregion

                #region Path
                string Path = $"/Img/Profile/{DateTime.Now.Month}";

                if (!await _FtpClient.CheckDirectoryExistAsync(qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, Path, qServer.FtpUserName, qServer.FtpPassword))
                    await MakeDirAsync(qServer.Name, Path);

                // برسی وجود دایرکتوری در دیتابیس
                if (!await _FilePathApplication.CheckDirectoryExistAsync(qServer.Id, Path))
                    await _FilePathApplication.MakePathAsync(qServer.Id, Path);

                #endregion

                var _Result = await _FtpClient.UploadAsync(_FormFile.OpenReadStream(), qServer.FtpHost, qServer.FtpPort, qServer.FtpPath, Path, FileName, qServer.FtpUserName, qServer.FtpPassword);
                if (_Result == true)
                {
                    var _Id = new Guid().SequentialGuid().ToString();
                    await _FileApplication.AddFileAsync(new InpAddFile()
                    {
                        Id = _Id,
                        FileServerId = qServer.Id,
                        Path = $"/{Path.Trim('/')}/",
                        FileName = FileName,
                        UserId = null,
                        IsPrivate = false,
                        Title = $"تصویر پروفایل - {FileName}",
                        MimeType = (await _AniShell.GetRealExtentionAsync(_FormFile)).Item2,
                        SizeOnDisk = _FormFile.Length
                    });

                    return _Id;
                }
                else
                {
                    throw new Exception("قادر به اپلود پروفایل کاربر نبودیم. لطفا اطلاعات خطای قبلی را برسی نمایید");
                }
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
    }
}
