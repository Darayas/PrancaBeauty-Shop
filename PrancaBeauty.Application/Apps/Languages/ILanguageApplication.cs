using PrancaBeauty.Application.Contracts.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Languages
{
    public interface ILanguageApplication
    {
        Task<string> GetAbbrByCodeAsync(string Code);
        Task<List<OutSiteLangCache>> GetAllLanguageForSiteLangAsync();
        Task<string> GetCodeByAbbrAsync(string Abbr);
        Task<string> GetCountryIdByLangIdAsync(string LangId);
        Task<string> GetDirectionByCodeAsync(string Code);
        Task<string> GetFlagUrlByCodeAsync(string Code);
        Task<OutSiteLangCache> GetLangDetailsByIdAsync(string LangId);
        Task<string> GetLangIdByLangCodeAsync(string LangCode);
        Task<string> GetNativeNameByCodeAsync(string Code);
        Task<bool> IsValidAbbrForSiteLangAsync(string Abbr);
    }
}
