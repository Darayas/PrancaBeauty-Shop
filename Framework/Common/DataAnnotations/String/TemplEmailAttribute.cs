using Framework.Common.ExMethods;
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
    public class TemplEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage == null)
                ErrorMessage = "TemplEmailMsg";

            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("Value only can be string.");

            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            if (!value.ToString().IsMatch(_Localizer["EmailPattern"]))
                return new ValidationResult(_Localizer[ErrorMessage].Replace("{0}", _Localizer[validationContext.DisplayName]));
            else
                return ValidationResult.Success;
        }
    }
}
