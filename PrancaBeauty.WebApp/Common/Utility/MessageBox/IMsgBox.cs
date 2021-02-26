namespace PrancaBeauty.WebApp.Common.Utility.MessageBox
{
    public interface IMsgBox
    {
        string ModelStateMsg(string ModelErrors, string CallBackFuncs = null);
    }
}