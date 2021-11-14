using PrancaBeauty.Application.Contracts.ProductProperties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertis
{
    public interface IProductPropertisApplication
    {
        Task<bool> CheckExistByIdAsync(string Id);
        Task<List<OutGetForManageProduct>> GetForManageProductAsync(string LangId, string TopicId);
    }
}