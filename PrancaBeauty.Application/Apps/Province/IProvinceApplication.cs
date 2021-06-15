using PrancaBeauty.Application.Contracts.Province;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Province
{
    public interface IProvinceApplication
    {
        Task<List<OutGetListForCombo>> GetListForComboAsync(string LangId, string CountryId, string Search);
    }
}