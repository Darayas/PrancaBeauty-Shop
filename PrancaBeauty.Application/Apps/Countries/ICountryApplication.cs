using PrancaBeauty.Application.Contracts.Countries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Countries
{
    public interface ICountryApplication
    {
        Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
    }
}