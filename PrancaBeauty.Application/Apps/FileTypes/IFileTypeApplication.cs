using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileTypes
{
    public interface IFileTypeApplication
    {
        Task<string> GetIdByMimeTypeAsync(string MimeType);
    }
}