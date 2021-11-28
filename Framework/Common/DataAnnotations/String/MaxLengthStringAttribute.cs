using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.DataAnnotations.String
{
    public class MaxLengthStringAttribute : ValidationAttribute
    {
        private readonly int _Max;
        public MaxLengthStringAttribute(int Max)
        {
            _Max = Max;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ErrorMessage == null)
                ErrorMessage = "MaxLengthStringMsg";

            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("Value only can be string.");

            var _Localizer = (ILocalizer)validationContext.GetService(typeof(ILocalizer));

            if (value.ToString().Length > _Max)
                return new ValidationResult(_Localizer[ErrorMessage].Replace("{0}", _Localizer[validationContext.DisplayName])
                                                                    .Replace("{1}", _Max.ToString()));
            else
                return ValidationResult.Success;
        }
    }
}
