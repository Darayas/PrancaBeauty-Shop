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
        Task<string> GetDirectionByCodeAsync(string Code);
        Task<string> GetFlagUrlByCodeAsync(string Code);
        Task<string> GetNativeNameByCodeAsync(string Code);
        Task<bool> IsValidAbbrForSiteLangAsync(string Abbr);
    }
}
