using Framework.Application.Services.Security.AntiShell;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.DataAnnotations
{
    public class AllowExtentionsAttribute : ValidationAttribute
    {
        private readonly string _MimeTypes;
        public AllowExtentionsAttribute(string MimeTypes)
        {
            _MimeTypes = MimeTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var _AntiShell = (IAniShell)validationContext.GetService(typeof(IAniShell));

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
                    if (_MimeTypes.Contains(_FormFile.ContentType))
                    {
                        return new ValidationResult(GetMessage(validationContext, _FormFile.FileName));
                    }
                }
            }

            return ValidationResult.Success;
        }

        public string GetMessage(ValidationContext validationContext, string FileName)
        {
            var _Localizer = (ILocalizer)validationContext.GetService(typeof(ILocalizer));

            ErrorMessage = _Localizer[ErrorMessage];

            // DisplayName
            if (ErrorMessage.Contains("{0}"))
                ErrorMessage.Replace("{0}", validationContext.DisplayName);

            // FileName
            if (ErrorMessage.Contains("{1}"))
                ErrorMessage.Replace("{1}", FileName);

            // MimeTypes
            if (ErrorMessage.Contains("{2}"))
                ErrorMessage.Replace("{2}", _MimeTypes);

            return ErrorMessage;
        }
    }
}
