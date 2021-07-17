using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.DataAnnotations
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private long _MaxFileSize { get; set; }
        private long _MinFileSize { get; set; }
        public FileSizeAttribute(long MaxFileSize, long MinFileSize = 1)
        {
            _MaxFileSize = MaxFileSize;
            _MinFileSize = MinFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

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
                        if (item.Length < _MinFileSize || item.Length > _MaxFileSize)
                        {
                            _Message += Environment.NewLine + GetMessage(validationContext, "");
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

                if (_FormFile.Length < _MinFileSize || _FormFile.Length > _MaxFileSize)
                {
                    return new ValidationResult(GetMessage(validationContext, ""));
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

            // MinFileSize
            if (ErrorMessage.Contains("{2}"))
                ErrorMessage.Replace("{2}", _MinFileSize.ToString() + "byte");

            // MaxFileSize
            if (ErrorMessage.Contains("{3}"))
                ErrorMessage.Replace("{3}", _MaxFileSize.GetFileSizeName());

            return ErrorMessage;
        }
    }
}
