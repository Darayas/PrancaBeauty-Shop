using PrancaBeauty.Application.Contracts.ApplicationDTO.Countries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Countries
{
    public interface ICountryApplication
    {
        Task<List<OutGetCourtriesListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
    }
}