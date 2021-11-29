using Framework.Application.Services.Security.AntiShell;
using Framework.Exceptions;
using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.ExMethods
{
    public static class ObjectEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Input"></param>
        /// <exception cref="ArgumentInvalidException">When modelstate error.</exception>
        public static void CheckModelState<T>(this T Input, IServiceProvider _ServiceProvider = null) where T : class
        {
            if (Input is null)
                throw new ArgumentInvalidException($"{nameof(Input)} cant be null.");

            List<ValidationResult> _ValidationResult = new List<ValidationResult>();
            var _ValidationContext = new ValidationContext(Input);
            _ValidationContext.InitializeServiceProvider(t => _ServiceProvider);

            if (Validator.TryValidateObject(Input, _ValidationContext, _ValidationResult, true) == false)
                if (_ValidationResult != null)
                    throw new ArgumentInvalidException(string.Join(",", _ValidationResult.Select(a => a.ErrorMessage)));

        }
    }
}
