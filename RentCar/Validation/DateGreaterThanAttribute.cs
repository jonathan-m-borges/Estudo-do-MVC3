using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RentCar.Validation
{
    public sealed class DateGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string basePropertyName;

        public DateGreaterThanAttribute(string basePropertyName)
        {
            this.basePropertyName = basePropertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, basePropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(basePropertyName);
            var startDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);

            var thisDate = (DateTime)value;

            if (thisDate <= startDate)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());

            //This string identifies which Javascript function to be executed to validate this   
            rule.ValidationType = "greaterthan";
            rule.ValidationParameters.Add("otherfield", basePropertyName);
            yield return rule;
        }
    }
}