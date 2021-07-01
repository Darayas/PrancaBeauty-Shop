using Framework.Application.Services.FTP;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Apps.FileServer;
using PrancaBeauty.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Common.FtpWapper
{
    public class FtpWapper: IFtpWapper
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

        public async Task<string> UplaodCategoryImgAsync(IFormFile _FormFile)
        {
            try
            {
                if (_FormFile is null)
                    throw new ArgumentInvalidException(nameof(_FormFile));

                var qServer= await _FileServerApplication.
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
