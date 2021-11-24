using Framework.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Common.DataAnnotations
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

            var _Localizer = (ILocalizer)validationContext.GetService(typeof(ILocalizer));

            var ErrMessage = _Localizer[ErrorMessage];

            // DisplayName
            if (ErrMessage.Contains("{0}"))
                ErrMessage = ErrMessage.Replace("{0}", validationContext.DisplayName);

            return ErrMessage;
        }
    }
}
