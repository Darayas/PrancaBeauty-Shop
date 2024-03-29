﻿using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Framework.Common.DataAnnotations.String
{
    public class DateAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is not string)
                return ValidationResult.Success;

            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();


            string DateRegex = _Localizer["DatePattern"];

            if (Regex.IsMatch((string)value, DateRegex))
                return ValidationResult.Success;
            else
                return new ValidationResult(_Localizer[ErrorMessage, validationContext.DisplayName]);
        }
    }
}
