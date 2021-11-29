﻿using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Framework.Common.DataAnnotations.File
{
    public class RequiredFileAttribute : ValidationAttribute
    {
        public RequiredFileAttribute()
        {
            if (ErrorMessage == null)
                ErrorMessage = "{0} cant be null.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(GetMessage(validationContext));

            if (value is List<IFormFile> || value is IFormFile[])
            {
                var _FormFile = (IEnumerable<IFormFile>)value;

                if (_FormFile.Count() == 0)
                    return new ValidationResult(GetMessage(validationContext));

                foreach (var item in _FormFile)
                {
                    if (item == null)
                        return new ValidationResult(GetMessage(validationContext));
                }
            }
            else if (value is IFormFile)
            {
                var _FormFile = (IFormFile)value;
                if (_FormFile == null)
                    return new ValidationResult(GetMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        public string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();


            ErrorMessage = _Localizer[ErrorMessage];

            if (ErrorMessage.Contains("{0}"))
                ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            return ErrorMessage;
        }
    }
}
