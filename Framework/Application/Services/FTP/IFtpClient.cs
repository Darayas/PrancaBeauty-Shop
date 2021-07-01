using System.IO;
using System.Threading.Tasks;

namespace Framework.Application.Services.FTP
{
    public interface IFtpClient
    {
        Task<bool> UploadAsync(Stream _File, string FtpHost, int FtpPort, string Path, string FileName, string FtpUserName, string FtpPassword);
    }
}