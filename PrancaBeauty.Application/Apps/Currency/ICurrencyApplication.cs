using PrancaBeauty.Application.Contracts.Currency;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Currency
{
    public interface ICurrencyApplication
    {
        Task<OutGetMainByCountryId> GetMainByCountryIdAsync(string LangId, string CountryId);
    }
}