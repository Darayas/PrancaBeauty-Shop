using Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Common.DataAnnotations.String
{
    public class RequiredForDraftAttribute : ValidationAttribute
    {
        private readonly string _FieldName;
        public RequiredForDraftAttribute(string DraftFieldName)
        {
            _FieldName = DraftFieldName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var IsDraft = (bool?)validationContext.ObjectInstance.GetType().GetProperty(_FieldName).GetValue(validationContext.ObjectInstance);
            if (IsDraft == null)
                IsDraft = false;

            if (IsDraft.Value == false)
                if (value == null)
                    return new ValidationResult(GetMessage(validationContext));

            return ValidationResult.Success;
        }

        public string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider?.GetService<ILocalizer>();

            if (ErrorMessage == null)
                ErrorMessage = "RequiredString";

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

            return ErrorMessage;
        }
    }
}
