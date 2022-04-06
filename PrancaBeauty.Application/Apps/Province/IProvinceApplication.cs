using PrancaBeauty.Application.Contracts.ApplicationDTO.Province;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Province
{
    public interface IProvinceApplication
    {
        Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
    }
}