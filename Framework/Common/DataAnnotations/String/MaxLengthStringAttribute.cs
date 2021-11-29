using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Common.DataAnnotations.String
{
    public class MaxLengthStringAttribute : ValidationAttribute
    {
        private readonly int _Max;
        public MaxLengthStringAttribute(int Max)
        {
            _Max = Max;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage == null)
                ErrorMessage = "MaxLengthStringMsg";

            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("Value only can be string.");

            if (value.ToString().Length > _Max)
                return new ValidationResult(GetMessage(validationContext));
            else
                return ValidationResult.Success;
        }

        public string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider?.GetService<ILocalizer>();

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

            if (ErrorMessage.Contains("{1}"))
                ErrorMessage = ErrorMessage.Replace("{1}", _Max.ToString());

            return ErrorMessage;
        }
    }
}
