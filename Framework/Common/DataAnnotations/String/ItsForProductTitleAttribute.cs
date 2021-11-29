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

            if (ErrorMessage == null)
                ErrorMessage = "ItsForProductTitleMsg";

            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();


            if (((string)value).CheckCharsForProductTitle())
                return ValidationResult.Success;
            else
                return new ValidationResult(GetMessage(validationContext));
        }

        public string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            if (_Localizer == null)
            {
                if (ErrorMessage.Contains("{0}"))
                    ErrorMessage = ErrorMessage.Replace("{0}", validationContext.DisplayName);
            }
            else
            {
                ErrorMessage = _Localizer[ErrorMessage];

                if (ErrorMessage.Contains("{0}"))
                    ErrorMessage = ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);
            }

            return ErrorMessage;
        }
    }
}
