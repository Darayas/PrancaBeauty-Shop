using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application.Services.FTP
{
    public class FtpClient : IFtpClient
    {
        int bufferSize = 2048;
        private readonly ILogger _Logger;

        public FtpClient(ILogger logger)
        {
            _Logger = logger;
        }

        public async Task<bool> UploadAsync(Stream _File, string FtpHost, int FtpPort, string Path, string FileName, string FtpUserName, string FtpPassword)
        {
            try
            {
                _File.Position = 0;
                /* Create an FTP Request */
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create($"{FtpHost}:{FtpPort}/{Path.Trim('/')}/{FileName}");
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(FtpUserName, FtpPassword);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                var ftpStream = await ftpRequest.GetRequestStreamAsync();
                /* Open a File Stream to Read the File for Upload */
                Stream localFileStream = _File;
                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[bufferSize];
                int bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, bufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */

                while (bytesSent != 0)
                {
                    await ftpStream.WriteAsync(byteBuffer, 0, bytesSent);
                    bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, bufferSize);
                }

                localFileStream.Close();
                ftpStream.Close();
                ftpRequest = null;
                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }

        }

        public async Task<bool> CheckDirectoryExistAsync(string FtpHost, int FtpPort, string Path, string FtpUserName, string FtpPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Path))
                    throw new ArgumentException($"'{nameof(Path)}' cannot be null or whitespace.", nameof(Path));

                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create($"{FtpHost}:{FtpPort}/{Path.Trim('/')}");
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(FtpUserName, FtpPassword);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                /* Establish Return Communication with the FTP Server */
                var _FtpWebResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();

                _FtpWebResponse.Close();
                ftpRequest = null;

                return true;
            }
            catch (Exception ex)
            {
                //_Logger.Error(ex);
                return false;
            }
        }

        public async Task<bool> CreateDirectoryAsync(string FtpHost, int FtpPort, string Path, string FtpUserName, string FtpPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Path))
                    throw new ArgumentException($"'{nameof(Path)}' cannot be null or whitespace.", nameof(Path));

                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create($"{FtpHost}:{FtpPort}/{Path.Trim('/')}");
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(FtpUserName, FtpPassword);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                /* Establish Return Communication with the FTP Server */
                var _FtpWebResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();

                _FtpWebResponse.Close();
                ftpRequest = null;

                return true;
            }
            catch (Exception ex)
            {
                //_Logger.Error(ex);
                return false;
            }
        }
    }
}
