using Framework.Application.Services.FTP;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Apps.FileServer;
using PrancaBeauty.Application.Exceptions;
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
        public FtpWapper(IFtpClient ftpClient, ILogger logger, IFileServerApplication fileServerApplication, IFileApplication fileApplication)
        {
            _FtpClient = ftpClient;
            _Logger = logger;
            _FileServerApplication = fileServerApplication;
            _FileApplication = fileApplication;
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
                    string FileEx = await GetRealExtentionAsync(_FormFile);
                    if (FileEx == null)
                        return null;

                    FileName = new Guid().SequentialGuid().ToString() + "." + FileEx;
                }
                else
                {
                    string FileEx = await GetRealExtentionAsync(_FormFile);
                    if (FileEx == null)
                        return null;

                    FileName = _FileName + "-" + new Random().Next(1000, 99999) + "." + FileEx;
                }
                #endregion

                #region Path
                string Path = $"/Img/Category/{DateTime.Now.Month}/{DateTime.Now.Day}";

                if (!await _FtpClient.CheckDirectoryExistAsync(qServer.FtpHost, qServer.FtpPort, Path, qServer.FtpUserName, qServer.FtpPassword))
                    await MakeDirAsync(qServer.Name, Path);
                #endregion

                var _Result = await _FtpClient.UploadAsync(_FormFile.OpenReadStream(), qServer.FtpHost, qServer.FtpPort, Path, FileName, qServer.FtpUserName, qServer.FtpPassword);
                if (_Result == true)
                {

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
                        if (!await _FtpClient.CheckDirectoryExistAsync(qServer.FtpHost, qServer.FtpPort, Path, qServer.FtpUserName, qServer.FtpPassword))
                        {
                            await _FtpClient.CreateDirectoryAsync(qServer.FtpHost, qServer.FtpPort, DirCheck, qServer.FtpUserName, qServer.FtpPassword));
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

        private async Task<string> GetRealExtentionAsync(IFormFile _FormFile)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException(nameof(_FormFile));

                byte[] buffer = new byte[50];
                await _FormFile.OpenReadStream().ReadAsync(buffer, 0, 50);

                string hex = "";
                foreach (var item in buffer)
                {
                    hex += string.Format("{0:X}", item) + " ";
                }

                if (hex.StartsWith("89 50 4E 47 0D 0A 1A 0A"))
                {
                    return "png";
                }
                else if (hex.StartsWith("FF D8"))
                {
                    return "jpg";
                }
                else if (hex.StartsWith("47 49 46 38 37 61") || hex.StartsWith("47 49 46 38 39 61"))
                {
                    return "gif";
                }
                else if (hex.StartsWith("42 4D"))
                {
                    return "bmp";
                }
                else
                    throw new FileFormatException("File format not found.");
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

        public async Task<bool> AddFileInfoToDataBaseAsync(string Title, string FileServerId, string Path, string FileName, string UserId, string MimeType, long SizeOnDisk, bool IsPrivate)
        {
            try
            {
                tblFiles tFile = new tblFiles()
                {
                    Id = new Guid().SequentialGuid(),
                    Date = DateTime.Now,
                    FileName = FileName,
                    FileServerId = Guid.Parse(FileServerId),
                    IsPrivate = IsPrivate,
                    MimeType = MimeType,
                    Path = Path,
                    SizeOnDisk = SizeOnDisk,
                    Title = Title,
                    UserId = Guid.Parse(UserId)
                };

                await _FileApplication.
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
