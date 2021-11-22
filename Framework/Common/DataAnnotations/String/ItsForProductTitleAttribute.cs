using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Common.DataAnnotations.String
{
    public class ItsForProductTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("Value only can be string.");

            var _Localizer = validationContext.GetService<ILocalizer>();

            if (((string)value).CheckCharsForProductTitle())
                return ValidationResult.Success;
            else
                return new ValidationResult(_Localizer[ErrorMessage.Replace("{0}", validationContext.DisplayName)]);
        }
    }
}
