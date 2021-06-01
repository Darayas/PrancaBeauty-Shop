using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.DataAnnotations
{
    public class PhoneNumberAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is not string)
                return ValidationResult.Success;

            var _Localizer = validationContext.GetService<ILocalizer>();

            string PhoneNumberRegex = _Localizer["MobilePattern"];

            if (Regex.IsMatch((string)value, PhoneNumberRegex))
                return ValidationResult.Success;
            else
                return new ValidationResult(_Localizer[ErrorMessage, validationContext.DisplayName]);
        }
    }
}
