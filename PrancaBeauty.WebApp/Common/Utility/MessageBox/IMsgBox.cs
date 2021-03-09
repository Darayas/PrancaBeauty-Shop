using PrancaBeauty.WebApp.Common.Types;

namespace PrancaBeauty.WebApp.Common.Utility.MessageBox
{
    public interface IMsgBox
    {
        JsResult FaildMsg(string Message, string CallBackFuncs = null);
        JsResult InfoMsg(string Message, string CallBackFuncs = null);
        JsResult ModelStateMsg(string ModelErrors, string CallBackFuncs = null);
        JsResult SuccessMsg(string Message, string CallBackFuncs = null);
    }
}