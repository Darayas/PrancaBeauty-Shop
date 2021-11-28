using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.DataAnnotations.String
{
    public class RequiredStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage == null)
                ErrorMessage = "RequiredStringMsg";

            var _Localizer = (ILocalizer)validationContext.GetService(typeof(ILocalizer));

            if (value is null)
                return new ValidationResult(_Localizer[ErrorMessage].Replace("{0}", _Localizer[validationContext.DisplayName]));
            else
                return ValidationResult.Success;
        }
    }
}
