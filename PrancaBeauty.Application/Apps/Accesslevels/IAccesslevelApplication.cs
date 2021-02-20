using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Accesslevels
{
    public interface IAccesslevelApplication
    {
        Task<string> GetIdByNameAsync(string Name);
    }
}