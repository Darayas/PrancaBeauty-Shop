using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.Utility.MessageBox
{
    public class MsgBox : IMsgBox
    {
        private readonly ILocalizer _Localizer;
        public MsgBox(ILocalizer localizer)
        {
            _Localizer = localizer;
        }
        private string Show(string Title, string Message, MsgBoxType Type, string OkBtnText = "OK", string CallBackFunction = null)
        {

            CallBackFunction = CallBackFunction ?? "return '';";
            string Js = $@"swal.fire({{
                                        title: '{Title.Replace("'", "`")}',
                                        html: '{Message.Replace("'", "`")}',
                                        icon: '{Type.ToString()}',
                                        confirmButtonText: '{OkBtnText}',
                                    }}).then((result) => {{
                                        if (result.isConfirmed) {{
                                          {CallBackFunction};
                                        }}
                                    }});";

            return Js;
        }

        public JsResult ModelStateMsg(string ModelErrors, string CallBackFuncs = null)
        {
            return new JsResult(Show("", ModelErrors, MsgBoxType.error, _Localizer["OK"], CallBackFuncs));
        }
    }

    public enum MsgBoxType
    {
        success,
        error,
        warning,
        info,
        //question
    }
}
