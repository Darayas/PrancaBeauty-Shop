using Framework.Application.Services.Security.AntiShell;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.ExMethods
{
    public static class ObjectEx
    {
        private static List<ValidationResult> Check<T>(T Input, IServiceProvider _ServiceProvider, string _SectionName = "")
        {
            if (Input is null)
                throw new ArgumentInvalidException($"{nameof(Input)} cant be null.");

            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            var _ValidationResult = new List<ValidationResult>();

            foreach (var item in Input.GetType().GetProperties())
            {
                if (item.PropertyType.GetInterfaces().Contains(typeof(IList)))
                {
                    var LstVals = (IEnumerable)item.GetValue(Input);
                    if (LstVals!=null)
                        foreach (var itemLst in LstVals)
                        {
                            _ValidationResult.AddRange(Check(itemLst, _ServiceProvider, _Localizer[GetName(item)]));
                        }
                }
            }

            var _ValidationContext = new ValidationContext(Input);
            _ValidationContext.InitializeServiceProvider(t => _ServiceProvider);

            Validator.TryValidateObject(Input, _ValidationContext, _ValidationResult, true);

            return _ValidationResult.Select(a => new ValidationResult(_SectionName+ a.ErrorMessage, a.MemberNames)).ToList();
        }

        private static string GetName(PropertyInfo Input)
        {
            object _Attr = Input.GetCustomAttributes(true).Where(a => a.GetType().Name=="DisplayAttribute").FirstOrDefault();
            if (_Attr==null)
                return "";

            return (string)_Attr.GetType().GetProperty("Name").GetValue(_Attr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Input"></param>
        /// <exception cref="ArgumentInvalidException">When modelstate error.</exception>
        public static void CheckModelState<T>(this T Input, IServiceProvider _ServiceProvider) where T : class
        {
            var Errors = Check(Input, _ServiceProvider);
            if (Errors!=null)
                if (Errors.Count()>0)
                    throw new ArgumentInvalidException(string.Join(",", Errors.Select(a => a.ErrorMessage)));
        }
    }
}
