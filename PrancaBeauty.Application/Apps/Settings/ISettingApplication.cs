using PrancaBeauty.Application.Contracts.Settings;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Settings
{
    public interface ISettingApplication
    {
        Task<OutSettings> GetSettingAsync(InpGetSetting Input);
    }
}