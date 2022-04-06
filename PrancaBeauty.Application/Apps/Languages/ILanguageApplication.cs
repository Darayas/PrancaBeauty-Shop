using PrancaBeauty.Application.Contracts.ApplicationDTO.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Languages
{
    public interface ILanguageApplication
    {
        Task<string> GetAbbrByCodeAsync(InpGetAbbrByCode Input);
        Task<List<OutSiteLangCache>> GetAllLanguageForSiteLangAsync();
        Task<string> GetCodeByAbbrAsync(InpGetCodeByAbbr Input);
        Task<string> GetCountryIdByLangIdAsync(InpGetCountryIdByLangId Input);
        Task<string> GetDirectionByCodeAsync(InpGetDirectionByCode Input);
        Task<string> GetFlagUrlByCodeAsync(InpGetFlagUrlByCode Input);
        Task<OutSiteLangCache> GetLangDetailsByIdAsync(InpGetLangDetailsById Input);
        Task<string> GetLangIdByLangCodeAsync(InpGetLangIdByLangCode Input);
        Task<string> GetNativeNameByCodeAsync(InpGetNativeNameByCode Input);
        Task<bool> IsValidAbbrForSiteLangAsync(InpIsValidAbbrForSiteLang  Input);
    }
}
