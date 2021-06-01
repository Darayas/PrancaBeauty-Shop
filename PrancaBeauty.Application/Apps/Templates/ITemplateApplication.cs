using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Templates
{
    public interface ITemplateApplication
    {
        Task<string> GetEmailChangeTemplateAsync(string LangCode, string Url);
        Task<string> GetEmailConfirmationTemplateAsync(string LangCode, string Url);
        Task<string> GetEmailLoginTemplateAsync(string LangCode, string Url);
        Task<string> GetTemplateAsync(string LangCode, string Name);
    }
}