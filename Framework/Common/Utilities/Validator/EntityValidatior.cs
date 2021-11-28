using Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Utilities.Validator
{
    public static class EntityValidatior
    {
        public static void Check(object Input)
        {
            if (Input is null)
                throw new ArgumentInvalidException($"{nameof(Input)} cant be null.");

            //List<ValidationResult> _ValidationResult = null;
            //if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(Input, new ValidationContext(Input), _ValidationResult))
            //    if (_ValidationResult != null)
            //        throw new ArgumentInvalidException(string.Join(",", _ValidationResult.Select(a => a.ErrorMessage)));

            var Validation = Input
                            .GetType()
                            .GetCustomAttributes(true);
                            //.OfType<ValidationAttribute>();
                            //.Where(a => a.ErrorMessage == error.ErrorMessage)
                            //.ToDictionary(a => a.GetType().Name, a => a);
        }
    }
}
