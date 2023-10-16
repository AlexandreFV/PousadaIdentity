using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
sealed class DataCheckInMaiorOuIgualDataAtualAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var checkIn = (DateTime)value;
        if (checkIn.Date < DateTime.Now.Date)
        {
            return new ValidationResult("A data de CheckIn deve ser maior ou igual à data atual.");
        }
        return ValidationResult.Success;
    }
}
