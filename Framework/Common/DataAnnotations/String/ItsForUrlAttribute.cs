using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Common.DataAnnotations.String
{
    public class ItsForUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("Value only can be string.");

            if (ErrorMessage == null)
                ErrorMessage = "ItsForUrlMsg";

            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider?.GetService<ILocalizer>();


            if (((string)value).CheckCharsForUrlName())
                return ValidationResult.Success;
            else
                return new ValidationResult(_Localizer[ErrorMessage].Replace("{0}", validationContext.DisplayName));
        }
    }
}
