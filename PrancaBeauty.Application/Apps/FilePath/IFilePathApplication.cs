using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FilePath
{
    public interface IFilePathApplication
    {
        Task<string> GetIdByPathAsync(string FileServerId, string Path);
    }
}