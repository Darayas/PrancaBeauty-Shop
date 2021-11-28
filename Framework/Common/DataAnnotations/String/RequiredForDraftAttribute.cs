using Framework.Infrastructure;
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

            var _Localizer = (ILocalizer)validationContext.GetService(typeof(ILocalizer));

            if (IsDraft.Value == false)
                if (value == null)
                    return new ValidationResult(_Localizer[ErrorMessage].Replace("{0}", _Localizer[validationContext.DisplayName]));

            return ValidationResult.Success;
        }
    }
}
