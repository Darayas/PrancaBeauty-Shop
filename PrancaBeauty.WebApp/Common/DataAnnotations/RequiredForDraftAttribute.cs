using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.DataAnnotations
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
                    return new ValidationResult(ErrorMessage.Replace("{0}", validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
