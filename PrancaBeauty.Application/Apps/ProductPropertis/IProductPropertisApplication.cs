using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductProperties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertis
{
    public interface IProductPropertisApplication
    {
        Task<bool> CheckExistByIdAsync(InpCheckExistById Input);
        Task<List<OutGetForManageProduct>> GetForManageProductAsync(InpGetForManageProduct Input);
    }
}