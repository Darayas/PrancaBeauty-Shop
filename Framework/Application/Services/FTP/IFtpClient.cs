using System.IO;
using System.Threading.Tasks;

namespace Framework.Application.Services.FTP
{
    public interface IFtpClient
    {
        Task<bool> CheckDirectoryExistAsync(string FtpHost, string FtpPort, string FtpPath, string Path, string FtpUserName, string FtpPassword);
        Task<bool> CheckFileExistAsync(string FtpHost, string FtpPort, string FtpPath, string Path, string FileName, string FtpUserName, string FtpPassword);
        Task<bool> CreateDirectoryAsync(string FtpHost, string FtpPort, string Path, string FtpUserName, string FtpPassword);
        Task<bool> RemoveAsync(string FtpHost, string FtpPort, string FtpPath, string Path, string FileName, string FtpUserName, string FtpPassword);
        Task<bool> UploadAsync(Stream _File, string FtpHost, string FtpPort, string FtpPath, string Path, string FileName, string FtpUserName, string FtpPassword);
    }
}