using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Keywords
{
    public interface IKeywordApplication
    {
        Task<bool> CheckExistByTitleAsync(string Title);
        Task<string> GetIdByTitleAsync(string Title);
    }
}