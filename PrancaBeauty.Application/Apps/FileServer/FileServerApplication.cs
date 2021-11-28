using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.FileServer;
using PrancaBeauty.Domin.FileServer.ServerAgg.Contracts;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileServer
{
    public class FileServerApplication : IFileServerApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IFileServerRepository _FileServerRepository;

        public FileServerApplication(IFileServerRepository fileServerRepository, ILogger logger, ILocalizer localizer)
        {
            _FileServerRepository = fileServerRepository;
            _Logger = logger;
            _Localizer = localizer;
        }

        public async Task<OutGetServerDetails> GetServerDetailsAsync(InpGetServerDetails Input)
        {

            try
            {
                #region Validations
                Input.CheckModelState(_Localizer);
                #endregion

                var qData = await _FileServerRepository.Get
                                                      .Where(a => a.Name == Input.ServerName)
                                                      .Where(a => a.IsActive)
                                                      .Select(a => new OutGetServerDetails
                                                      {
                                                          Id = a.Id.ToString(),
                                                          Name = a.Name,
                                                          HttpDomin = a.HttpDomin,
                                                          HttpPath = a.HttpPath,
                                                          Description = a.Description,
                                                          Capacity = a.Capacity,
                                                          FtpData = a.FtpData
                                                      })
                                                      .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                #region Decrypted FtpData
                string DecryptedFtpData = qData.FtpData.AesDecrypt(AuthConst.SecretKey);

                var _FtpData = JsonSerializer.Deserialize<OutGetServerDetails>(DecryptedFtpData);

                qData.FtpHost = _FtpData.FtpHost;
                qData.FtpPath = _FtpData.FtpPath;
                qData.FtpPort = _FtpData.FtpPort;
                qData.FtpUserName = _FtpData.FtpUserName;
                qData.FtpPassword = _FtpData.FtpPassword;
                qData.FtpPassword = _FtpData.FtpPassword;
                #endregion

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

        public async Task<string> GetBestServerNameByFileSizeAsync(InpGetBestServerNameByFileSize Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_Localizer);
                #endregion

                var qData = await _FileServerRepository.Get
                                                       .Where(a => a.IsActive)
                                                       .Select(a => new
                                                       {
                                                           ServerName = a.Name,
                                                           FreeSpace = a.Capacity - (a.tblFilePaths.SelectMany(a => a.tblFiles.Select(a => a.SizeOnDisk)).Sum())
                                                       })
                                                       .Where(a => a.FreeSpace > Input.FileSize)
                                                       .FirstOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData.ServerName;
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
