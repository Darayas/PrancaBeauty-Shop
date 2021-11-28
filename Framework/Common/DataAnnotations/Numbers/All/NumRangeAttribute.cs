using Framework.Infrastructure;
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

            var _Localizer = (ILocalizer)validationContext.GetService(typeof(ILocalizer));

            if ((decimal)value < _Min && (decimal)value > _Max)
                return new ValidationResult(_Localizer[ErrorMessage].Replace("{0}", _Localizer[validationContext.DisplayName])
                                                                    .Replace("{1}", _Min.ToString())
                                                                    .Replace("{2}", _Max.ToString()));
            else
                return ValidationResult.Success;
        }
    }
}
