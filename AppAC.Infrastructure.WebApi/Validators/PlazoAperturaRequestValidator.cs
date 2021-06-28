using System;
using System.Diagnostics.CodeAnalysis;
using AppAC.Application;
using FluentValidation;

namespace AppAC.Infrastructure.WebApi.Validators
{
    public class PlazoAperturaRequestValidator:AbstractValidator<PlazoAperturaRequest>
    {
        public PlazoAperturaRequestValidator()
        {
            RuleFor(x => x.FechaInicio)
                .Must(x => !DateTimeIsEmpty(x)).WithMessage("No debe esta vacio")
                .Must(x=>BeAValidDate(x)).WithMessage("Debe ser valida")
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.FechaFin)
                .Must(x => !DateTimeIsEmpty(x)).WithMessage("No debe esta vacio")
                .Must(x=>BeAValidDate(x)).WithMessage("Debe ser valida")
                .NotEmpty()
                .NotNull();
        }
        //new Date(stringValue).toISOString()
        private bool DateTimeIsEmpty(DateTime dat)
        {
            return dat == DateTime.MinValue;
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}