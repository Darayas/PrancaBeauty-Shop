using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Framework.Common.DataAnnotations.File
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
                            _Message += Environment.NewLine + GetMessage(validationContext, item.FileName);
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
                    return new ValidationResult(GetMessage(validationContext, _FormFile.FileName));
                }
            }

            return ValidationResult.Success;
        }

        public string GetMessage(ValidationContext validationContext, string FileName)
        {
            var _Localizer = (ILocalizer)validationContext.GetService(typeof(ILocalizer));

            var ErrMessage = _Localizer[ErrorMessage];

            // DisplayName
            if (ErrMessage.Contains("{0}"))
                ErrMessage = ErrMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            // FileName
            if (ErrMessage.Contains("{1}"))
                ErrMessage = ErrMessage.Replace("{1}", FileName);

            // MinFileSize
            if (ErrMessage.Contains("{2}"))
                ErrMessage = ErrMessage.Replace("{2}", _MinFileSize.ToString() + "byte");

            // MaxFileSize
            if (ErrMessage.Contains("{3}"))
                ErrMessage = ErrMessage.Replace("{3}", _MaxFileSize.GetFileSizeName());

            return ErrMessage;
        }
    }
}
