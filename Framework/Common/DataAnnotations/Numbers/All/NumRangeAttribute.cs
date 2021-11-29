using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.DataAnnotations.Numbers.All
{
    public class NumRangeAttribute : ValidationAttribute
    {
        private readonly decimal _Min;
        private readonly decimal _Max;
        public NumRangeAttribute(decimal Min, decimal Max)
        {
            _Min = Min;
            _Max = Max;
        }

        public NumRangeAttribute(int Min, int Max)
        {
            _Min = Min;
            _Max = Max;
        }

        public NumRangeAttribute(long Min, long Max)
        {
            _Min = Min;
            _Max = Max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage == null)
                ErrorMessage = "NumRangeMsg";

            if (value is null)
                return ValidationResult.Success;

            if (value is not int
                && value is not byte
                && value is not short
                && value is not long
                && value is not sbyte
                && value is not ushort
                && value is not uint
                && value is not ulong
                && value is not float
                && value is not double
                && value is not decimal)
                throw new Exception("Value only can be Numeric.");

            if (Convert.ToDecimal(value) < _Min && Convert.ToDecimal(value) > _Max)
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

            if (ErrorMessage.Contains("{0}"))
                ErrorMessage = ErrorMessage.Replace("{1}", _Min.ToString())
                            .Replace("{2}", _Max.ToString());

            return ErrorMessage;
        }
    }
}
