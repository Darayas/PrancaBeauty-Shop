using Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public static void CheckModelState<T>(this T Input) where T : class
        {
            if (Input is null)
                throw new ArgumentInvalidException($"{nameof(Input)} cant be null.");

            List<ValidationResult> _ValidationResult = null;
            if (Validator.TryValidateObject(Input, new ValidationContext(Input), _ValidationResult))
                throw new ArgumentInvalidException(string.Join(",", _ValidationResult.Select(a => a.ErrorMessage)));

        }
    }
}
