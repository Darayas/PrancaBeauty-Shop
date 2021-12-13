﻿using Framework.Application.Services.Security.AntiShell;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Framework.Common.DataAnnotations.File
{
    public class AllowExtentionsAttribute : ValidationAttribute
    {
        private string _MimeTypes;
        public AllowExtentionsAttribute(string MimeTypes = null)
        {
            _MimeTypes = MimeTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _AntiShell = _ServiceProvider.GetService<IAniShell>();

            if (value is List<IFormFile> || value is IFormFile[])
            {
                var _FormFile = (IEnumerable<IFormFile>)value;

                if (_FormFile.Count() == 0)
                    return ValidationResult.Success;

                string _Message = "";
                foreach (var item in _FormFile)
                {
                    if (item != null)
                    {
                        if (!_AntiShell.ValidationExtentionAsync(item).Result)
                            _Message += Environment.NewLine + GetMessage(validationContext, item.FileName);
                        else
                        {
                            if (!_MimeTypes.Contains(item.ContentType))
                            {
                                _Message += Environment.NewLine + GetMessage(validationContext, item.FileName);
                            }
                        }
                    }
                }

                if (_Message != "")
                    return new ValidationResult(_Message);
            }
            else if (value is IFormFile)
            {
                var _FormFile = (IFormFile)value;
                if (_FormFile == null)
                    return ValidationResult.Success;

                if (!_AntiShell.ValidationExtentionAsync(_FormFile).Result)
                    return new ValidationResult(GetMessage(validationContext, _FormFile.FileName));
                else
                {
                    if (!_MimeTypes.Contains(_FormFile.ContentType))
                    {
                        return new ValidationResult(GetMessage(validationContext, _FormFile.FileName));
                    }
                }
            }

            return ValidationResult.Success;
        }

        public string GetMessage(ValidationContext validationContext, string FileName)
        {
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            if (ErrorMessage == null)
                ErrorMessage = "AllowExtentionsMsg";

            var ErrMessage = _Localizer[ErrorMessage];

            // DisplayName
            if (ErrMessage.Contains("{0}"))
                ErrorMessage = ErrMessage = ErrMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            // FileName
            if (ErrMessage.Contains("{1}"))
                ErrMessage = ErrMessage.Replace("{1}", FileName);

            // MimeTypes
            if (ErrMessage.Contains("{2}"))
                ErrMessage = ErrMessage.Replace("{2}", _MimeTypes);

            return ErrMessage;
        }
    }
}
