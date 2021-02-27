using PrancaBeauty.WebApp.Common.Types;

namespace PrancaBeauty.WebApp.Common.Utility.MessageBox
{
    public interface IMsgBox
    {
        JsResult ModelStateMsg(string ModelErrors, string CallBackFuncs = null);
    }
}