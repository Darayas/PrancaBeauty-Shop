using System.IO;
using System.Threading.Tasks;

namespace Framework.Application.Services.FTP
{
    public interface IFtpClient
    {
        Task<bool> CheckDirectoryExistAsync(string FtpHost, int FtpPort, string FtpPath, string Path, string FtpUserName, string FtpPassword);
        Task<bool> CreateDirectoryAsync(string FtpHost, int FtpPort, string FtpPath, string Path, string FtpUserName, string FtpPassword);
        Task<bool> UploadAsync(Stream _File, string FtpHost, int FtpPort, string FtpPath, string Path, string FileName, string FtpUserName, string FtpPassword);
    }
}