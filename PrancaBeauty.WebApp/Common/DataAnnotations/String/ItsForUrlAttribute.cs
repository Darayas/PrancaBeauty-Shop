﻿using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.DataAnnotations.String
{
    public class ItsForUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("Value only can be string.");

            var _Localizer = validationContext.GetService<ILocalizer>();

            if (((string)value).IsMatch(@"^[A-Za-z0-9\-]*$"))
                return ValidationResult.Success;
            else
                return new ValidationResult(_Localizer[ErrorMessage.Replace("{0}", validationContext.DisplayName)]);
        }
    }
}
