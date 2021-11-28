using PrancaBeauty.Application.Contracts.Currency;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Currency
{
    public interface ICurrencyApplication
    {
        Task<string> GetIdByCountryIdAsync(InpGetIdByCountryId Input);
        Task<OutGetMainByCountryId> GetMainByCountryIdAsync(InpGetMainByCountryId Input);
    }
}