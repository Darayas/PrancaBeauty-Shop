﻿using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Common.DataAnnotations.File
{
    public class GUIDAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value is null)
                    return ValidationResult.Success;

                if (value is string)
                {

                    var _Guid = Guid.Parse((string)value);

                    return ValidationResult.Success;
                }
                else
                    return ValidationResult.Success;

            }
            catch (Exception)
            {
                return new ValidationResult(GetMessage(validationContext));
            }
        }

        public string GetMessage(ValidationContext validationContext)
        {
            if (ErrorMessage == null)
                ErrorMessage = "GUIDMsg";

            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();


            var ErrMessage = _Localizer[ErrorMessage];

            // DisplayName
            if (ErrMessage.Contains("{0}"))
                ErrMessage = ErrMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            return ErrMessage;
        }
    }
}
