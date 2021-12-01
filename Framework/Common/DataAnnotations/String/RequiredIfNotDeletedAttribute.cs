using Framework.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.DataAnnotations.String
{
    public class RequiredIfNotDeletedAttribute : ValidationAttribute
    {
        private readonly string _FieldName;
        public RequiredIfNotDeletedAttribute(string DraftFieldName)
        {
            _FieldName = DraftFieldName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var IsDelete = (bool?)validationContext.ObjectInstance.GetType().GetProperty(_FieldName).GetValue(validationContext.ObjectInstance);
            if (IsDelete == null)
                IsDelete = false;

            if (IsDelete.Value == false)
                if (value == null)
                    return new ValidationResult(GetMessage(validationContext));

            return ValidationResult.Success;
        }

        public string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var _Localizer = _ServiceProvider?.GetService<ILocalizer>();

            if (ErrorMessage == null)
                ErrorMessage = "RequiredStringMsg";

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
