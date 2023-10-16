using PousadaIdentity.Entities;
using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
sealed class DataCheckOutMaiorQueCheckInAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var reserva = (Reserva)validationContext.ObjectInstance;
        if (reserva.CheckOut.Date < reserva.CheckIn.Date.AddDays(1))
        {
            return new ValidationResult("A data de CheckOut deve ser pelo menos um dia após a data de CheckIn.");
        }
        return ValidationResult.Success;
    }
}
