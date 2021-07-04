using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.FileServer;
using PrancaBeauty.Application.Exceptions;
using PrancaBeauty.Domin.FileServer.ServerAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileServer
{
    public class FileServerApplication : IFileServerApplication
    {
        private readonly ILogger _Logger;
        private readonly IFileServerRepository _FileServerRepository;

        public FileServerApplication(IFileServerRepository fileServerRepository, ILogger logger)
        {
            _FileServerRepository = fileServerRepository;
            _Logger = logger;
        }

        public async Task<OutGetServerDetails> GetServerDetailsAsync(string ServerName)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(ServerName))
                    throw new ArgumentInvalidException($"'{nameof(ServerName)}' cannot be null or whitespace.");

                var qData = await _FileServerRepository.Get
                                                      .Where(a => a.Name == ServerName)
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
