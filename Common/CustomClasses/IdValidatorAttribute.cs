using System.ComponentModel.DataAnnotations;

namespace Common.CustomClasses;

public class IdValidatorAttribute : ValidationAttribute
{
    public IdValidatorAttribute() : base("The field must not be zero.")
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue && intValue == 0)
        {
            return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
    }
}